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

        internal override Word GetWord()
        {
            Word word = null;
            while (word == null)
            {
                word = _wordService.GetRandomWord();
            }
            return word;
        }

        internal override AlexaResponse BuildAlexaResponse(AlexaRequestPayload alexaRequest, Dictionary<string, string> pearsonResponseDictionary)
        {
            AlexaResponse alexaResponse = new AlexaResponse();

            alexaResponse.Response.OutputSpeech.Ssml = string.Format("<speak>{0}</speak>", BuildOutputSpeech(pearsonResponseDictionary));

            alexaResponse.Response.Card.Title = "Vocabulary App";
            alexaResponse.Response.Card.Content = string.Format("The Word is {0}.",
                pearsonResponseDictionary[Utility.GetDescriptionFromEnumValue(WordEnum.Word)]);

            alexaResponse.Response.Reprompt.OutputSpeech.Ssml = string.Format("<speak><p>You can say</p><p>The word is {0}</p><p>Or you can say</p><p>Get another word</p></speak>",
                pearsonResponseDictionary[Utility.GetDescriptionFromEnumValue(WordEnum.Word)]);

            alexaResponse.Response.ShouldEndSession = false;

            alexaResponse.Response.OutputSpeech.Type = "SSML";
            alexaResponse.Response.Reprompt.OutputSpeech.Type = "SSML";

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

            stringBuilder.Append(string.Format("<p>Okay your turn now</p><p>Say</p><p>The word is {0}</p>",
                pearsonResponseDictionary[Utility.GetDescriptionFromEnumValue(WordEnum.Word)]));

            return stringBuilder.ToString();
        }
    }

        
}
