using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlexaSkillProject.Domain;

namespace AlexaSkillProject.Services
{
    public class AlexaRequestHandlerStrategyFactory : IAlexaRequestHandlerStrategyFactory
    {
        public IAlexaRequestHandlerStrategy CreateAlexaRequestHandlerStrategy(AlexaRequestPayload alexaRequest)
        {
            switch (alexaRequest.Request.Type)
            {
                case "LaunchRequest":
                    return new LaunchRequestHandlerFactory().CreateAlexaRequestHandlerStrategy(alexaRequest);
 
                case "SessionEndedRequest":
                    return new SessionEndedRequestHandlerFactory().CreateAlexaRequestHandlerStrategy(alexaRequest);

                case "IntentRequest":
                    return new IntentRequestHandlerFactory().CreateAlexaRequestHandlerStrategy(alexaRequest);

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
