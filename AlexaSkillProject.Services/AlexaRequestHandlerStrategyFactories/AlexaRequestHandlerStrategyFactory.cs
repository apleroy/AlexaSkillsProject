using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlexaSkillProject.Domain;

namespace AlexaSkillProject.Services
{
    /// <summary>
    /// This class returns the correct handler strategy to process the request
    /// Available strategies are initialized in the UnityConfig
    /// </summary>
    public class AlexaRequestHandlerStrategyFactory : IAlexaRequestHandlerStrategyFactory
    {
        private readonly IEnumerable<IAlexaRequestHandlerStrategy> _availableStrategies;

        public AlexaRequestHandlerStrategyFactory(IEnumerable<IAlexaRequestHandlerStrategy> availableStrategies)
        {
            _availableStrategies = availableStrategies;
        }

        public IAlexaRequestHandlerStrategy CreateAlexaRequestHandlerStrategy(AlexaRequestPayload alexaRequest)
        {
            
            switch (alexaRequest.Request.Type)
            {
                case "LaunchRequest":
                case "SessionEndedRequest":
                    IAlexaRequestHandlerStrategy strategy = _availableStrategies
                        .FirstOrDefault(s => s.SupportedRequestType == alexaRequest.Request.Type);
                    return strategy;
                case "IntentRequest":
                    IAlexaRequestHandlerStrategy intentStrategy = _availableStrategies
                        .FirstOrDefault(s => s.SupportedRequestIntentName == alexaRequest.Request.Intent.Name);
                    return intentStrategy;

                default:
                    throw new NotImplementedException();
            }

        }
    }
}
