using System;
using AlexaSkillProject.Domain;

namespace AlexaSkillProject.Services
{
    public class CancelOrStopIntentHandlerStrategy : IAlexaRequestHandlerStrategy
    {
        public string SupportedRequestIntentName
        {
            get
            {
                return "AMAZON.CancelOrStopIntent";
            }
        }

        public string SupportedRequestType
        {
            get
            {
                return StrategyHandlerTypes.IntentRequest.ToString();
            }
        }

        public AlexaResponse HandleAlexaRequest(AlexaRequestPayload alexaRequest)
        {
            var response = new AlexaResponse("Thanks for using the Eloquency Skill.  You can also visit Eloquency App dot com for more information.  I hope to see you again soon.");

            response.Response.ShouldEndSession = true;

            return response;
        }
    }
}
