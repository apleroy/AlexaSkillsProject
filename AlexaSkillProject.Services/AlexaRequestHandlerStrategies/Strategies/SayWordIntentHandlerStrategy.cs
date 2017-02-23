using AlexaSkillProject.Core;
using AlexaSkillProject.Domain;
using System;
using System.Runtime.Caching;
using System.Text;

namespace AlexaSkillProject.Services
{

    /// <summary>
    /// This class handles the user saying 'The word is <WORD>'
    /// A comparison using the session id is used to compare the user's spoken word with the last word given to them by alexa
    /// </summary>
    public class SayWordIntentHandlerStrategy : IAlexaRequestHandlerStrategy
    {
        private readonly ICacheService _cacheService;

        public string SupportedRequestType
        {
            get
            {
                return "SayWordIntent";
            }
        }

        public string SupportedRequestIntentName
        {
            get
            {
                return StrategyHandlerTypes.IntentRequest.ToString();
            }
        }

        public SayWordIntentHandlerStrategy(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }



        /// <summary>
        /// This method gets the word spoken by the user (using the slots and intents)
        /// See the LIST_OF_WORDS.txt in speech assets
        /// The word spoken is compared to the latest word given (and cached using session id)
        /// </summary>
        /// <param name="alexaRequest"></param>
        /// <returns></returns>
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
            AlexaRequestPayload lastRequest = _cacheService.RetrieveAlexaRequest(alexaRequest.Session.SessionId);

            try
            {
                //AlexaRequestPayload lastRequest = (AlexaRequestPayload)MemoryCache.Default.Get(alexaRequest.Session.SessionId);
                lastWordGiven = lastRequest.Session.Attributes.LastWord;
                lastWordGivenDefinition = lastRequest.Session.Attributes.LastWordDefinition;
            }
            catch (Exception exception)
            {
                // log exception in the future - for now the words will remain null
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
            alexaResponse.Response.Reprompt.OutputSpeech.Ssml = string.Format("<speak><p>You can say</p><p>The word of the day is {0} </p></speak>", lastWordGiven);
            alexaResponse.Response.ShouldEndSession = false;
            alexaResponse.Response.OutputSpeech.Type = "SSML";

            return alexaResponse;
        }

        /// <summary>
        /// This method builds the response when the user response is equal to the last word gievn to them (and cached for their session)
        /// </summary>
        /// <param name="wordSaid"></param>
        /// <param name="lastWordGiven"></param>
        /// <param name="lastWordGivenDefinition"></param>
        /// <returns></returns>
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

        /// <summary>
        /// This method is returned when the user says a word that does not equal the last word given to them
        /// </summary>
        /// <param name="wordSaid"></param>
        /// <param name="lastWordGiven"></param>
        /// <returns></returns>
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
