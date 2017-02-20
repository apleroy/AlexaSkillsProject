using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlexaSkillProject.Domain;
using AlexaSkillProject.Core;
using System.Configuration;

namespace AlexaSkillProject.Services
{
    
    public class WordOfTheDayIntentHandlerStrategy : AbstractWordIntentHandlerStrategy
    {
        public WordOfTheDayIntentHandlerStrategy(IWordService wordService, IDictionaryService dictionaryService) : base(wordService, dictionaryService) { }

        protected override Word GetWord()
        {
            Word word = null;
            word = _wordService.GetWordOfTheDay();
            while (word == null)
            {
               word = _wordService.GetRandomWord();
            }
            return word;
        }

        protected override AlexaResponse BuildAlexaResponse(AlexaRequestPayload alexaRequest, Dictionary<WordEnum, string> wordResponseDictionary)
        {
            AlexaResponse alexaResponse = new AlexaResponse();

            alexaResponse.Response.OutputSpeech.Ssml = string.Format("<speak>{0}</speak>", BuildOutputSpeech(wordResponseDictionary));
            
            alexaResponse.Response.Card.Title = ConfigurationSettings.AppSettings["AppTitle"];
            alexaResponse.Response.Card.Content = string.Format("The Word of the Day is {0}.",
                wordResponseDictionary[WordEnum.Word]);

            alexaResponse.Response.Reprompt.OutputSpeech.Ssml = 
                string.Format("<speak><p>You can say</p><p>The word is {0}</p><p>Or you can say</p><p>Get another word</p></speak>",
                wordResponseDictionary[WordEnum.Word]);

            alexaResponse.Response.ShouldEndSession = false;

            alexaResponse.Response.OutputSpeech.Type = "SSML";
            alexaResponse.Response.Reprompt.OutputSpeech.Type = "SSML";

            return alexaResponse;
        }


    }

}