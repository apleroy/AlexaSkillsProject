using System;
using AlexaSkillProject.Domain;

namespace AlexaSkillProject.Services
{
    public class SessionEndedRequestHandlerStrategy : IAlexaRequestHandlerStrategy
    {
        public string SupportedRequestIntentName
        {
            get
            {
                return "SessionEndedIntent";
            }
        }

        public string SupportedRequestType
        {
            get
            {
                return StrategyHandlerTypes.SessionEndedRequest.ToString();
            }
        }

        public AlexaResponse HandleAlexaRequest(AlexaRequestPayload alexaRequest)
        {
            return null;
        }
    }
}
