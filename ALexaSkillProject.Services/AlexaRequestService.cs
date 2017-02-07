using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlexaSkillProject.Domain;
using AlexaSkillProject.Repository;


namespace AlexaSkillProject.Services
{
    public class AlexaRequestService : IAlexaRequestService
    {

        private readonly IAlexaRequestMapper _alexaRequestMapper;
        private readonly IAlexaRequestPersistenceService _alexaRequestPersistenceService;
        private readonly IAlexaRequestHandlerStrategyFactory _alexaRequestHandlerStrategyFactory;

        public AlexaRequestService(
            IAlexaRequestMapper alexaRequestMapper, 
            IAlexaRequestPersistenceService alexaRequestPersistenceService,
            IAlexaRequestHandlerStrategyFactory alexaRequestHandlerStrategyFactory)
        {
            _alexaRequestMapper = alexaRequestMapper;
            _alexaRequestPersistenceService = alexaRequestPersistenceService;
            _alexaRequestHandlerStrategyFactory = alexaRequestHandlerStrategyFactory;
        }

        public AlexaResponse ProcessAlexaRequest(AlexaRequestInputModel alexaRequestInputModel)
        {
            // transform request
            AlexaRequest alexaRequest = _alexaRequestMapper.MapAlexaRequest(alexaRequestInputModel);

            // persist request and member
            _alexaRequestPersistenceService.PersistAlexaRequestAndMember(alexaRequest);

            // create a request handler strategy from the alexarequest
            IAlexaRequestHandlerStrategy alexaRequestHandlerStrategy = _alexaRequestHandlerStrategyFactory.CreateAlexaRequestHandlerStrategy(alexaRequest);

            // use the handlerstrategy to process the request and generate a response
            AlexaResponse alexaResponse = alexaRequestHandlerStrategy.HandleAlexaRequest(alexaRequest);

            // return response
            return alexaResponse;
        }

        


    }
}
