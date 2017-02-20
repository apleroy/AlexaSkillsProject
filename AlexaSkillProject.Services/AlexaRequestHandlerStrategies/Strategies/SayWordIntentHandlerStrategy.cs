using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlexaSkillProject.Domain;
using System.Runtime.Caching;
using AlexaSkillProject.Core;

namespace AlexaSkillProject.Services
{

    public class SayWordIntentHandlerStrategy : IAlexaRequestHandlerStrategy
    {
        public AlexaResponse HandleAlexaRequest(AlexaRequestPayload alexaRequest)
        {
            // get the word said back to alexa
            string wordSaid = null;
            var slots = alexaRequest.Request.Intent.GetSlots();
            foreach (var slot in slots)
            {
                if (slot.Key.Equals("Word"))
                {
                    wordSaid = slot.Value;
                }
            }

            // compare with the last word given to user
            string lastWordGiven = null;
            string lastWordGivenDefinition = null;
            try
            {
                AlexaRequestPayload lastRequest = (AlexaRequestPayload)MemoryCache.Default.Get(alexaRequest.Session.SessionId);
                lastWordGiven = lastRequest.Session.Attributes.LastWord;
                lastWordGivenDefinition = lastRequest.Session.Attributes.LastWordDefinition;
            }
            catch
            {
                // error getting last word
            }

            // build the response speech
            string outputSpeech = null;
            if (wordSaid.Equals(lastWordGiven))
            {
                outputSpeech = BuildSuccessResponse(wordSaid, lastWordGiven, lastWordGivenDefinition);
            }
            else
            {
                outputSpeech = BuildErrorResponse(wordSaid, lastWordGiven);
            }

            // build the alexaresponse
            AlexaResponse alexaResponse = new AlexaResponse();
            alexaResponse.Response.OutputSpeech.Ssml = string.Format("<speak>{0}</speak>", outputSpeech);

            alexaResponse.Response.Card.Title = "Vocabulary App";
            alexaResponse.Response.Card.Content = String.Format("The word is {0}. You said {1}", lastWordGiven, wordSaid);
            alexaResponse.Response.Reprompt.OutputSpeech.Ssml = string.Format("<speak><p>You can say</p><p>The word of the day is {0} </p></speak>", lastWordGiven);
            alexaResponse.Response.ShouldEndSession = false;
            alexaResponse.Response.OutputSpeech.Type = "SSML";

            return alexaResponse;
        }

        private string BuildSuccessResponse(string wordSaid, string lastWordGiven, string lastWordGivenDefinition)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(string.Format("<p>I heard {0}</p><p>{1}</p>", 
                wordSaid, Utility.GetDescriptionFromEnumValue(SuccessPhrases.Great)));

            stringBuilder.Append(string.Format("<p>To recap</p><p>The definition of {0} is {1}</p>", lastWordGiven, lastWordGivenDefinition));

            stringBuilder.Append(string.Format("<p>Good job.  Let's keep it up!</p>"));

            stringBuilder.Append("<p>You can continue by saying</p>");
            stringBuilder.Append("<p>Get Another Word</p>");

            return stringBuilder.ToString();
        }

        private string BuildErrorResponse(string wordSaid, string lastWordGiven)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(string.Format("<p>{0}</p>",
                Utility.GetDescriptionFromEnumValue(ErrorPhrases.Incorrect)));

            stringBuilder.Append(string.Format("<p>I heard {0}</p>", wordSaid));
            stringBuilder.Append(string.Format("<p>The last word is {0}</p>", lastWordGiven));
            stringBuilder.Append("<p>Could you please try again?</p>");
            stringBuilder.Append("<p>Please say</p>");
            stringBuilder.Append(string.Format("<p>The word is {0}</p>", lastWordGiven));

            return stringBuilder.ToString();
        }

    }

}
