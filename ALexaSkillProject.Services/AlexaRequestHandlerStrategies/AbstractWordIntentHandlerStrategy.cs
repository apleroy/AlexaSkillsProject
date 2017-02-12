using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlexaSkillProject.Domain;
using System.Net;
using AlexaSkillProject.Core;

namespace AlexaSkillProject.Services
{
    public abstract class AbstractWordIntentHandlerStrategy : IAlexaRequestHandlerStrategy
    {
        protected readonly IPearsonsDictionaryApiService _pearsonsDictionaryApiService;
        protected readonly IWordOfTheDayService _wordOfTheDayService;

        public AbstractWordIntentHandlerStrategy()
        {
            _pearsonsDictionaryApiService = new PearsonsDictionaryApiService();
            _wordOfTheDayService = new WordOfTheDayService();
        }

        internal abstract Word GetWord();

        internal abstract string BuildOutputSpeech(Dictionary<string, string> pearsonResponseDictionary);

        internal abstract AlexaResponse BuildAlexaResponse(AlexaRequest alexaRequest, Dictionary<string, string> pearsonResponseDictionary);

        public AlexaResponse HandleAlexaRequest(AlexaRequest alexaRequest)
        {
            try
            {
                // Get the word of the day - implemented in base class
                Word wordOfTheDay = GetWord();

                // build and send the api call
                PearsonsDictionaryApiResponse pearsonsDictionaryApiResponse = null;
                try
                {
                    // create and parse the web request to pearsons dictionary api  
                    HttpWebRequest webRequest = _pearsonsDictionaryApiService.CreateDictionaryApiRequest(wordOfTheDay.WordName);
                    pearsonsDictionaryApiResponse = _pearsonsDictionaryApiService.ParseDictionaryApiRequest(webRequest);
                }
                catch (Exception exception)
                {
                    var e = exception.Message;
                }

                // parse the api response
                Dictionary<string, string> pearsonResponseDictionary = ConvertPearsonResponseToDictionary(pearsonsDictionaryApiResponse);

                // build the alexa response - implemented in base class
                AlexaResponse alexaResponse = BuildAlexaResponse(alexaRequest, pearsonResponseDictionary);

                return alexaResponse;
            }
            catch (Exception exception)
            {
                return new AlexaWordErrorResponse().GenerateCustomError();
            }
        }


        protected Dictionary<string, string> ConvertPearsonResponseToDictionary(PearsonsDictionaryApiResponse pearsonsDictionaryApiResponse)
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
