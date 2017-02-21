using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlexaSkillProject.Domain;

namespace AlexaSkillProject.Services
{
    /// <summary>
    /// This class serves as an abstract factory - based on the request type, it returns a handler strategy
    /// </summary>
    public class AlexaRequestHandlerStrategyFactory : IAlexaRequestHandlerStrategyFactory
    {

        private readonly IWordService _wordService;
        private readonly IDictionaryService _dictionaryService;
        private readonly ICacheService _cacheService;

        public AlexaRequestHandlerStrategyFactory(
            IWordService wordService, 
            IDictionaryService dictionaryService,
            ICacheService cacheService)
        {
            _wordService = wordService;
            _dictionaryService = dictionaryService;
            _cacheService = cacheService;
        }

        public IAlexaRequestHandlerStrategy CreateAlexaRequestHandlerStrategy(AlexaRequestPayload alexaRequest)
        {
            switch (alexaRequest.Request.Type)
            {
                case "LaunchRequest":
                    return new LaunchRequestHandlerFactory().CreateAlexaRequestHandlerStrategy(alexaRequest);
 
                case "SessionEndedRequest":
                    return new SessionEndedRequestHandlerFactory().CreateAlexaRequestHandlerStrategy(alexaRequest);

                case "IntentRequest":
                    return new IntentRequestHandlerFactory(_wordService, _dictionaryService, _cacheService).CreateAlexaRequestHandlerStrategy(alexaRequest);

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
