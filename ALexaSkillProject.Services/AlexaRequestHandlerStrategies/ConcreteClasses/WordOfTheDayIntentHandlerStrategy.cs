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
using System.ComponentModel;
using System.Reflection;
using AlexaSkillProject.Core;

namespace AlexaSkillProject.Services
{
    public enum WOD
    {  
        [Description("WordOfTheDay")]
        WordOfTheDay,

        [Description("WordOfTheDayPartOfSpeech")]
        WordOfTheDayPartOfSpeech,

        [Description("WordOfTheDayDefinition")]
        WordOfTheDayDefinition,

        [Description("WordOfTheDayExample")]
        WordOfTheDayExample,
    }

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
            try
            {
                // Get the word of the day
                Word wordOfTheDay = _wordOfTheDayService.GetWordOfTheDay();

                // create and parse the web request to pearsons dictionary api  
                HttpWebRequest webRequest = _pearsonsDictionaryApiService.CreateDictionaryApiRequest(wordOfTheDay.WordName);
                PearsonsDictionaryApiResponse pearsonsDictionaryApiResponse = _pearsonsDictionaryApiService.ParseDictionaryApiRequest(webRequest);
                
                // parse the api response
                Dictionary<string, string> pearsonResponseDictionary = ConvertPearsonResponseToDictionary(pearsonsDictionaryApiResponse);

                // build the response
                string response = BuildResponse(pearsonResponseDictionary);
                
                AlexaResponse alexaResponse = new AlexaResponse(response);
                alexaResponse.Session.MemberId = alexaRequest.AlexaMemberId;
                alexaResponse.Response.Card.Title = "Vocabulary App";
                alexaResponse.Response.Card.Content = String.Format("The Word of the Day is {0}.", 
                    pearsonResponseDictionary[Utility.GetDescriptionFromEnumValue(WOD.WordOfTheDay)]);
                alexaResponse.Response.Reprompt.OutputSpeech.Text = "What Would You like to do next?";
                alexaResponse.Response.ShouldEndSession = false;

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
                {Utility.GetDescriptionFromEnumValue(WOD.WordOfTheDay), null },
                {Utility.GetDescriptionFromEnumValue(WOD.WordOfTheDayPartOfSpeech), null },
                {Utility.GetDescriptionFromEnumValue(WOD.WordOfTheDayDefinition), null },
                {Utility.GetDescriptionFromEnumValue(WOD.WordOfTheDayExample), null },
            };

            
            foreach (PearsonsDictionaryApiResponse.ResultSet resultSet in pearsonsDictionaryApiResponse.Results)
            {
                if (resultSet.Datasets.Contains("laad3"))
                {
                    try { wordOfTheDayDictionary[Utility.GetDescriptionFromEnumValue(WOD.WordOfTheDay)] = resultSet.Headword; } catch { }
                    try { wordOfTheDayDictionary[Utility.GetDescriptionFromEnumValue(WOD.WordOfTheDayPartOfSpeech)] = resultSet.PartOfSpeech;} catch { }
                    try { wordOfTheDayDictionary[Utility.GetDescriptionFromEnumValue(WOD.WordOfTheDayDefinition)] = resultSet.Senses[0].Definition; } catch { }
                    try { wordOfTheDayDictionary[Utility.GetDescriptionFromEnumValue(WOD.WordOfTheDayExample)] = resultSet.Senses[0].Examples[0].Text; } catch { }
                }
            }

            return wordOfTheDayDictionary;

        }

        private string BuildResponse(Dictionary<string, string> pearsonResponseDictionary)
        {
            
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(string.Format("The Word of the day is {0}.", 
                pearsonResponseDictionary[Utility.GetDescriptionFromEnumValue(WOD.WordOfTheDay)]));

            stringBuilder.Append(string.Format("{0} is a {1}.", 
                pearsonResponseDictionary[Utility.GetDescriptionFromEnumValue(WOD.WordOfTheDay)], 
                pearsonResponseDictionary[Utility.GetDescriptionFromEnumValue(WOD.WordOfTheDayPartOfSpeech)]));

            stringBuilder.Append(string.Format("The Definition of {0} is {1}.", 
                pearsonResponseDictionary[Utility.GetDescriptionFromEnumValue(WOD.WordOfTheDay)], 
                pearsonResponseDictionary[Utility.GetDescriptionFromEnumValue(WOD.WordOfTheDayDefinition)]));

            if (pearsonResponseDictionary["wordOfTheDayExample"] != null)
            {
                stringBuilder.Append(string.Format("{0} can be used in an example like {1}.", 
                    pearsonResponseDictionary[Utility.GetDescriptionFromEnumValue(WOD.WordOfTheDay)], 
                    pearsonResponseDictionary[Utility.GetDescriptionFromEnumValue(WOD.WordOfTheDayExample)]));
            }

            return stringBuilder.ToString();
        }



    }











}