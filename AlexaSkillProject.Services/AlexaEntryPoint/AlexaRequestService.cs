using AlexaSkillProject.Domain;
using System;

namespace AlexaSkillProject.Services
{
    /// <summary>
    /// This class is the main entry point and serves as the wrapper service for each AlexaRequest
    /// AlexaRequests come through the Web API AlexaController (in the WebApp project)
    /// </summary>
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

        public AlexaResponse ProcessAlexaRequest(AlexaRequestPayload alexaRequestPayload)
        {
            // validate request time stamp and app id
            // note that there is custom validation in the AlexaRequestValidationHandler
            SpeechletRequestValidationResult validationResult = _alexaRequestValidationService.ValidateAlexaRequest(alexaRequestPayload);

            if (validationResult == SpeechletRequestValidationResult.OK)
            {
                try
                {
                    // transform request
                    AlexaRequest alexaRequest = _alexaRequestMapper.MapAlexaRequest(alexaRequestPayload);

                    // persist request and member
                    _alexaRequestPersistenceService.PersistAlexaRequestAndMember(alexaRequest);

                    // create a request handler strategy from the alexarequest
                    IAlexaRequestHandlerStrategy alexaRequestHandlerStrategy = _alexaRequestHandlerStrategyFactory.CreateAlexaRequestHandlerStrategy(alexaRequestPayload);

                    // use the handlerstrategy to process the request and generate a response
                    AlexaResponse alexaResponse = alexaRequestHandlerStrategy.HandleAlexaRequest(alexaRequestPayload);

                    // return response
                    return alexaResponse;
                }
                catch (Exception exception)
                {
                    // todo: log the error
                    return new AlexaWordErrorResponse().GenerateCustomError();
                }
            }

            return null;
        }

    }
}
