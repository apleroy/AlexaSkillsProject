using System;
using AlexaSkillProject.Domain;

namespace AlexaSkillProject.Services
{
    public class CancelIntentHandlerStrategy : IAlexaRequestHandlerStrategy
    {
        public string SupportedRequestIntentName
        {
            get
            {
                return "AMAZON.CancelIntent";
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
            var response = new AlexaResponse("Thanks for using Grammar Tool.  You can also visit Grammar Tool App dot com for more information.  I hope to see you again soon.");

            response.Response.ShouldEndSession = true;

            return response;
        }
    }
}
