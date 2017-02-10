using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlexaSkillProject.Domain;
using System.Runtime.Serialization.Json;
using System.Net;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.IO;
using AlexaSkillProject.Services;
using AlexaSkillProject.Repository;

namespace AlexaSkillProject.Services
{
    public class WordOfTheDayIntentHandlerStrategy : IAlexaRequestHandlerStrategy
    {
        private readonly IPearsonsDictionaryApiService _pearsonsDictionaryApiService;
        private readonly IWordOfTheDayService _wordOfTheDayService;

        public WordOfTheDayIntentHandlerStrategy()
        {
            _pearsonsDictionaryApiService = new PearsonsDictionaryApiService();
            _wordOfTheDayService = new WordOfTheDayService();
        }

        public AlexaResponse HandleAlexaRequest(AlexaRequest alexaRequest)
        {
            // get a random word from a list - random word service - optional db call
            var word = "Catastrophe";

            Word wordOfTheDay = _wordOfTheDayService.GetWordOfTheDay();

            PearsonsDictionaryApiResponse pearsonsDictionaryApiResponse = null;
            // create and parse the web request to pearsons dictionary api
            try
            {
                HttpWebRequest webRequest = _pearsonsDictionaryApiService.CreateDictionaryApiRequest(wordOfTheDay.WordName);
                pearsonsDictionaryApiResponse = _pearsonsDictionaryApiService.ParseDictionaryApiRequest(webRequest);
            }
            catch (Exception e)
            {
                throw e;
            }
            // put the response into sayings

            // parse the api response
            Dictionary<string, string> pearsonResponseDictionary = ConvertPearsonResponseToDictionary(pearsonsDictionaryApiResponse);

            // build the response
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(String.Format("The Word of the day is {0}.", pearsonResponseDictionary["wordOfTheDay"]));
            stringBuilder.Append(String.Format("{0} is a {1}.", pearsonResponseDictionary["wordOfTheDay"], pearsonResponseDictionary["wordOfTheDayPartOfSpeech"]));
            stringBuilder.Append(String.Format("The Definition of {0} is {1}.", pearsonResponseDictionary["wordOfTheDay"], pearsonResponseDictionary["wordOfTheDayDefinition"]));
            stringBuilder.Append(String.Format("{0} can be used in an example like {1}.", pearsonResponseDictionary["wordOfTheDay"], pearsonResponseDictionary["wordOfTheDayExample"]));

            AlexaResponse alexaResponse = new AlexaResponse(stringBuilder.ToString());
            alexaResponse.Session.MemberId = alexaRequest.AlexaMemberId;
            alexaResponse.Response.Card.Title = "Vocabulary App";
            alexaResponse.Response.Card.Content = String.Format("Word of the Day is {0}.", pearsonResponseDictionary["wordOfTheDay"]);
            alexaResponse.Response.Reprompt.OutputSpeech.Text = "What Would You like to do next?";
            alexaResponse.Response.ShouldEndSession = false;
            
            // put all of this into enums
            // the word of the day is: <WOD>
            // the definition of <WOD> is: <WOD DEFINITION>
            // <WOD> can be used in a sentence like: <WOD EXAMPLE>
            // Your turn.  Say <WOD>

            return alexaResponse;
        }

        private Dictionary<string, string> ConvertPearsonResponseToDictionary(PearsonsDictionaryApiResponse pearsonsDictionaryApiResponse)
        {
            Dictionary<string, string> wordOfTheDayDictionary = new Dictionary<string, string>
            {
                {"wordOfTheDay", null },
                {"wordOfTheDayPartOfSpeech", null },
                {"wordOfTheDayDefinition", null },
                {"wordOfTheDayExample", null },
            };

            
            foreach (PearsonsDictionaryApiResponse.ResultSet resultSet in pearsonsDictionaryApiResponse.Results)
            {
                if (resultSet.Datasets.Contains("laad3"))
                {
                    try { wordOfTheDayDictionary["wordOfTheDay"] = resultSet.Headword; } catch { }
                    try { wordOfTheDayDictionary["wordOfTheDayPartOfSpeech"] = resultSet.PartOfSpeech;} catch { }
                    try { wordOfTheDayDictionary["wordOfTheDayDefinition"] = resultSet.Senses[0].Definition; } catch { }
                    try { wordOfTheDayDictionary["wordOfTheDayExample"] = resultSet.Senses[0].Examples[0].Text; } catch { }
                }
            }

            
            return wordOfTheDayDictionary;

        }

    }











}