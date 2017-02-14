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
        private readonly IAlexaRequestValidationService _alexaRequestValidationService;


        public AlexaRequestService(
            IAlexaRequestMapper alexaRequestMapper, 
            IAlexaRequestPersistenceService alexaRequestPersistenceService,
            IAlexaRequestHandlerStrategyFactory alexaRequestHandlerStrategyFactory,
            IAlexaRequestValidationService alexaRequestValidationService
        )
        {
            _alexaRequestMapper = alexaRequestMapper;
            _alexaRequestPersistenceService = alexaRequestPersistenceService;
            _alexaRequestHandlerStrategyFactory = alexaRequestHandlerStrategyFactory;
            _alexaRequestValidationService = alexaRequestValidationService;
        }

        public AlexaResponse ProcessAlexaRequest(AlexaRequestInputModel alexaRequestInputModel)
        {
            // validate request time stamp and app id
            SpeechletRequestValidationResult validationResult = _alexaRequestValidationService.ValidateAlexaRequest(alexaRequestInputModel);

            if (validationResult == SpeechletRequestValidationResult.OK)
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

            return null;
        }

    }
}
