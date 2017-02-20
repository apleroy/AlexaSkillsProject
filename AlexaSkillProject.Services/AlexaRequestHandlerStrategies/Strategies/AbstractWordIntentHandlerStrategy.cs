using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlexaSkillProject.Domain;
using System.Net;
using AlexaSkillProject.Core;
using System.Runtime.Caching;

namespace AlexaSkillProject.Services
{
    public abstract class AbstractWordIntentHandlerStrategy : IAlexaRequestHandlerStrategy
    {
        // gets a word from our database
        protected readonly IWordService _wordService;
        
        // maps a word to its values (could be from db or third party api)
        protected readonly IDictionaryService _dictionaryService;
        

        public AbstractWordIntentHandlerStrategy(IWordService wordService, IDictionaryService dictionaryService)
        {
            // TODO:  DI from the top level ??  Adding into parameterless contructor / strategy pattern
            // http://stackoverflow.com/questions/1945611/setting-the-parameterless-constructor-as-the-injection-constructor-in-container

            _wordService = wordService;
            _dictionaryService = dictionaryService;
           
        }

        protected abstract Word GetWord();

        protected abstract AlexaResponse BuildAlexaResponse(AlexaRequestPayload alexaRequest, Dictionary<WordEnum, string> responseDictionary);

        public AlexaResponse HandleAlexaRequest(AlexaRequestPayload alexaRequest)
        {
            // initialize variables used in case of retry
            Word word = null; // what word will we get - should never be null
            int retryCounter = 0;
            Dictionary<WordEnum, string> wordResponseDictionary = null; // this method must return populated dictionary
            string wordResponse = null;

            // get the word
            // get the response dictionary from the wrod
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
                        var e = exception.Message;
                    }

                    wordResponse = wordResponseDictionary[WordEnum.Word];
                    retryCounter += 1;
                }

                // build the alexa response - implemented in base class
                AlexaResponse alexaResponse = BuildAlexaResponse(alexaRequest, wordResponseDictionary);

                // assign word to request for caching the request between intents
                alexaRequest.Session.Attributes.LastWord = word.WordName; // use the word from the db as it is saved in intent slots
                alexaRequest.Session.Attributes.LastWordDefinition = wordResponseDictionary[WordEnum.WordDefinition];

                // use set to add sessionid/request to memory cache
                // http://stackoverflow.com/questions/8868486/whats-the-difference-between-memorycache-add-and-memorycache-set

                MemoryCache.Default.Set(alexaRequest.Session.SessionId,
                    alexaRequest,
                    new CacheItemPolicy()
                    );


                return alexaResponse;
            }

            catch (Exception exception)
            {
                return new AlexaWordErrorResponse().GenerateCustomError();
            }
        }

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

    }
}
