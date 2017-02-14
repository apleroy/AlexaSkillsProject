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
        protected readonly IPearsonsDictionaryApiService _pearsonsDictionaryApiService;
        protected readonly IWordService _wordService;

        public AbstractWordIntentHandlerStrategy()
        {
            _wordService = new WordService();
            _pearsonsDictionaryApiService = new PearsonsDictionaryApiService();
        }

        internal abstract Word GetWord();

        internal abstract string BuildOutputSpeech(Dictionary<string, string> pearsonResponseDictionary);

        internal abstract AlexaResponse BuildAlexaResponse(AlexaRequestPayload alexaRequest, Dictionary<string, string> pearsonResponseDictionary);

        public AlexaResponse HandleAlexaRequest(AlexaRequestPayload alexaRequest)
        {
            try
            {
                // build and send the api call
                Word word = null;
                int count = 0;
                PearsonsDictionaryApiResponse pearsonsDictionaryApiResponse = null;
                Dictionary<string, string> pearsonResponseDictionary = null;
                string wordApiResponse = null;

                // repeat api call and parse
                while (wordApiResponse == null)
                {
                    // Get the word - implemented in base class
                    if (count == 0)
                    {
                        word = GetWord();
                    }
                    // Get random word - there was an issue with the first word
                    else
                    {
                        word = _wordService.GetRandomWord();
                    }

                    try
                    {
                        // create and parse the web request to pearsons dictionary api  
                        HttpWebRequest webRequest = _pearsonsDictionaryApiService.CreateDictionaryApiRequest(word.WordName);
                        pearsonsDictionaryApiResponse = _pearsonsDictionaryApiService.ParseDictionaryApiRequest(webRequest);
                    }
                    catch (Exception exception)
                    {
                        var e = exception.Message;
                    }

                    // parse the api response
                    pearsonResponseDictionary = ConvertPearsonResponseToDictionary(pearsonsDictionaryApiResponse);
                    wordApiResponse = pearsonResponseDictionary[Utility.GetDescriptionFromEnumValue(WordEnum.Word)];
                    count += 1;
                }

                // build the alexa response - implemented in base class
                AlexaResponse alexaResponse = BuildAlexaResponse(alexaRequest, pearsonResponseDictionary);

                // assign word to request for caching the request between intents
                alexaRequest.Session.Attributes.LastWord = word.WordName;

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

       
        private Dictionary<string, string> ConvertPearsonResponseToDictionary(PearsonsDictionaryApiResponse pearsonsDictionaryApiResponse)
        {
            Dictionary<string, string> wordOfTheDayDictionary = new Dictionary<string, string>
            {
                {Utility.GetDescriptionFromEnumValue(WordEnum.Word), null },
                {Utility.GetDescriptionFromEnumValue(WordEnum.WordPartOfSpeech), null },
                {Utility.GetDescriptionFromEnumValue(WordEnum.WordDefinition), null },
                {Utility.GetDescriptionFromEnumValue(WordEnum.WordExample), null },
            };


            foreach (PearsonsDictionaryApiResponse.ResultSet resultSet in pearsonsDictionaryApiResponse.Results)
            {
                if (resultSet.Datasets.Contains("laad3"))
                {
                    try { wordOfTheDayDictionary[Utility.GetDescriptionFromEnumValue(WordEnum.Word)] = resultSet.Headword; } catch { }
                    try { wordOfTheDayDictionary[Utility.GetDescriptionFromEnumValue(WordEnum.WordPartOfSpeech)] = resultSet.PartOfSpeech; } catch { }
                    try { wordOfTheDayDictionary[Utility.GetDescriptionFromEnumValue(WordEnum.WordDefinition)] = resultSet.Senses[0].Definition; } catch { }
                    try { wordOfTheDayDictionary[Utility.GetDescriptionFromEnumValue(WordEnum.WordExample)] = resultSet.Senses[0].Examples[0].Text; } catch { }
                }
            }

            return wordOfTheDayDictionary;

        }

        

        
    }
}
