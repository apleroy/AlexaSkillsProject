using System;
using AlexaSkillProject.Domain;
using AlexaSkillProject.Core;

namespace AlexaSkillProject.Services
{
    public class HelloWorldIntentHandlerStrategy : IAlexaRequestHandlerStrategy
    {
        public string SupportedRequestIntentName
        {
            get
            {
                return "HelloWorldIntent";
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
            return new AlexaResponse("Hello Andy");
        }
    }
}