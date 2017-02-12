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

    public class AnotherWordIntentHandlerStrategy : AbstractWordIntentHandlerStrategy
    {

        //public AnotherWordIntentHandlerStrategy() : base() { }

        internal override Word GetWord()
        {
            return _wordOfTheDayService.GetRandomWord();
        }

        internal override AlexaResponse BuildAlexaResponse(AlexaRequest alexaRequest, Dictionary<string, string> pearsonResponseDictionary)
        {
            AlexaResponse alexaResponse = new AlexaResponse();

            alexaResponse.Response.OutputSpeech.Ssml = string.Format("<speak>{0}</speak>", BuildOutputSpeech(pearsonResponseDictionary));
            alexaResponse.Session.MemberId = alexaRequest.AlexaMemberId;
            alexaResponse.Response.Card.Title = "Vocabulary App";
            alexaResponse.Response.Card.Content = String.Format("The Word is {0}.",
                pearsonResponseDictionary[Utility.GetDescriptionFromEnumValue(WordEnum.Word)]);
            alexaResponse.Response.Reprompt.OutputSpeech.Ssml = "What Would You like to do next?";
            alexaResponse.Response.ShouldEndSession = false;
            alexaResponse.Response.OutputSpeech.Type = "SSML";

            return alexaResponse;
        }

        internal override string BuildOutputSpeech(Dictionary<string, string> pearsonResponseDictionary)
        {

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(string.Format("<p>The Next Word is {0}</p>",
                pearsonResponseDictionary[Utility.GetDescriptionFromEnumValue(WordEnum.Word)]));

            stringBuilder.Append(string.Format("<p>{0} is a {1}</p>",
                pearsonResponseDictionary[Utility.GetDescriptionFromEnumValue(WordEnum.Word)],
                pearsonResponseDictionary[Utility.GetDescriptionFromEnumValue(WordEnum.WordPartOfSpeech)]));

            stringBuilder.Append(string.Format("<p>The Definition of {0} is</p><p>{1}</p>",
                pearsonResponseDictionary[Utility.GetDescriptionFromEnumValue(WordEnum.Word)],
                pearsonResponseDictionary[Utility.GetDescriptionFromEnumValue(WordEnum.WordDefinition)]));

            if (pearsonResponseDictionary[Utility.GetDescriptionFromEnumValue(WordEnum.WordExample)] != null)
            {
                stringBuilder.Append(string.Format("<p>{0} can be used in an example like </p><p>{1}</p>",
                    pearsonResponseDictionary[Utility.GetDescriptionFromEnumValue(WordEnum.Word)],
                    pearsonResponseDictionary[Utility.GetDescriptionFromEnumValue(WordEnum.WordExample)]));
            }

            // Okay your turn...

            return stringBuilder.ToString();
        }
    }

        
}
