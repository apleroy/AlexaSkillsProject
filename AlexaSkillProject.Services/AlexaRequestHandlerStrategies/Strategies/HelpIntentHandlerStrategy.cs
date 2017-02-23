using System;
using AlexaSkillProject.Domain;

namespace AlexaSkillProject.Services
{
    public class HelpIntentHandlerStrategy : IAlexaRequestHandlerStrategy
    {
        public string SupportedRequestIntentName
        {
            get
            {
                return "AMAZON.HelpIntent";
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
            var response = new AlexaResponse("You can ask What is the Word of the Day.  You can also visit Eloquency App dot com for more information.");

            response.Response.ShouldEndSession = false;

            return response;
        }
    }
}
