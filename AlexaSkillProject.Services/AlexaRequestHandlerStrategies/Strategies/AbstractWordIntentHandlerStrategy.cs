using AlexaSkillProject.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Caching;
using System.Text;

namespace AlexaSkillProject.Services
{
    public abstract class AbstractWordIntentHandlerStrategy : IAlexaRequestHandlerStrategy
    {
        // gets a word from our database
        protected readonly IWordService _wordService;
        
        // maps a word to its values (could be from db or third party api)
        protected readonly IDictionaryService _dictionaryService;

        protected readonly ICacheService _cacheService;

        public AbstractWordIntentHandlerStrategy(
            IWordService wordService, 
            IDictionaryService dictionaryService,
            ICacheService cacheService)
        {
            // TODO:  DI from the top level ??  Adding into parameterless contructor / strategy pattern
            // http://stackoverflow.com/questions/1945611/setting-the-parameterless-constructor-as-the-injection-constructor-in-container

            _wordService = wordService;
            _dictionaryService = dictionaryService;
            _cacheService = cacheService;
           
        }

        protected abstract Word GetWord();
        
        /// <summary>
        /// This method implements retry logic to get a word (specific method implemented in base class)
        /// This method builds the response dictionary for the word and alexa response
        /// This method ensures caching for between session logic
        /// The WordOfTheDay and AnotherWord strategies implement this abstract class with different responses and word retrival logic
        /// </summary>
        /// <param name="alexaRequest"></param>
        /// <returns></returns>
        public AlexaResponse HandleAlexaRequest(AlexaRequestPayload alexaRequest)
        {
            // initialize variables used in case of retry
            Word word = null; // what word will we get - should never be null
            int retryCounter = 0;
            Dictionary<WordEnum, string> wordResponseDictionary = null; // this method must return populated dictionary
            string wordResponse = null;

            // get the word
            // get the response dictionary from the word
            // handle retry errors - get another word from the word service if invalid response dictionary

            try
            {
                // repeat api call and parse
                while (wordResponse == null)
                {
                    // Get the word - implemented in base class
                    if (retryCounter == 0)
                        word = GetWord();
                    // Get random word - there was an issue with the first word
                    else
                        word = _wordService.GetRandomWord();

                    try
                    {
                        wordResponseDictionary = _dictionaryService.GetWordDictionaryFromString(word.WordName);
                    }
                    catch (Exception exception)
                    {
                        throw new Exception("Unable to build response dictionary for the word: " + word + " " + exception.Message);
                    }

                    wordResponse = wordResponseDictionary[WordEnum.Word];
                    retryCounter += 1;
                }

                // build the alexa response - implemented in base class
                AlexaResponse alexaResponse = BuildAlexaResponse(alexaRequest, wordResponseDictionary);

                // assign word to request for caching the request between intents
                alexaRequest.Session.Attributes.LastWord = word.WordName; // use the word from the db as it is saved in intent slots
                alexaRequest.Session.Attributes.LastWordDefinition = wordResponseDictionary[WordEnum.WordDefinition];

                _cacheService.CacheAlexaRequest(alexaRequest);

                //// use set to add sessionid/request to memory cache
                //// http://stackoverflow.com/questions/8868486/whats-the-difference-between-memorycache-add-and-memorycache-set

                //MemoryCache.Default.Set(alexaRequest.Session.SessionId,
                //    alexaRequest,
                //    new CacheItemPolicy()
                //    );


                return alexaResponse;
            }

            catch (Exception exception)
            {
                // todo - log the exception
                return new AlexaWordErrorResponse().GenerateCustomError();
            }
        }

        /// <summary>
        /// Builds the response from the request and retrieved word properties
        /// </summary>
        /// <param name="alexaRequest"></param>
        /// <param name="wordResponseDictionary"></param>
        /// <returns></returns>
        protected AlexaResponse BuildAlexaResponse(AlexaRequestPayload alexaRequest, Dictionary<WordEnum, string> wordResponseDictionary)
        {
            AlexaResponse alexaResponse = new AlexaResponse();

            alexaResponse.Response.OutputSpeech.Ssml = string.Format("<speak>{0}</speak>", BuildOutputSpeech(wordResponseDictionary));

            alexaResponse.Response.Card.Title = ConfigurationSettings.AppSettings["AppTitle"];
            alexaResponse.Response.Card.Content = BuildOutputCardContent(wordResponseDictionary);

            alexaResponse.Response.Reprompt.OutputSpeech.Ssml =
                string.Format("<speak><p>You can say</p><p>The word is {0}</p><p>Or you can say</p><p>Get another word</p></speak>",
                wordResponseDictionary[WordEnum.Word]);

            alexaResponse.Response.ShouldEndSession = false;

            alexaResponse.Response.OutputSpeech.Type = "SSML";
            alexaResponse.Response.Reprompt.OutputSpeech.Type = "SSML";

            return alexaResponse;
        }


        /// <summary>
        /// Generic word response with p tags for SSML markup
        /// Todo: some sort of builder pattern for making responses
        /// </summary>
        /// <param name="wordResponseDictionary"></param>
        /// <returns></returns>
        protected string BuildOutputSpeech(Dictionary<WordEnum, string> wordResponseDictionary)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(string.Format("<p>The Word is {0}</p>",
                wordResponseDictionary[WordEnum.Word]));

            stringBuilder.Append(string.Format("<p>{0} is a {1}</p>",
                wordResponseDictionary[WordEnum.Word],
                wordResponseDictionary[WordEnum.WordPartOfSpeech]));

            stringBuilder.Append(string.Format("<p>The Definition of {0} is</p><p>{1}</p>",
                wordResponseDictionary[WordEnum.Word],
                wordResponseDictionary[WordEnum.WordDefinition]));

            if (wordResponseDictionary[WordEnum.WordExample] != null)
            {
                stringBuilder.Append(string.Format("<p>{0} can be used in an example like </p><p>{1}</p>",
                    wordResponseDictionary[WordEnum.Word],
                    wordResponseDictionary[WordEnum.WordExample]));
            }

            stringBuilder.Append(string.Format("<p>Okay your turn now</p><p>Say</p><p>The word is {0}</p>",
                wordResponseDictionary[WordEnum.Word]));

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Generic alexa card response
        /// </summary>
        /// <param name="wordResponseDictionary"></param>
        /// <returns></returns>
        protected string BuildOutputCardContent(Dictionary<WordEnum, string> wordResponseDictionary)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(string.Format("The Word is: {0}.",
                wordResponseDictionary[WordEnum.Word]));

            stringBuilder.Append(Environment.NewLine);

            stringBuilder.Append(string.Format("The Definition of {0} is: {1}.",
                wordResponseDictionary[WordEnum.Word],
                wordResponseDictionary[WordEnum.WordDefinition]));

            return stringBuilder.ToString();
        }

    }
}
