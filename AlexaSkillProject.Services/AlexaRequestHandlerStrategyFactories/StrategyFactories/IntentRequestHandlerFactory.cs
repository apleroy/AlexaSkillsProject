using AlexaSkillProject.Domain;
using System;

namespace AlexaSkillProject.Services
{
    public class IntentRequestHandlerFactory
    {
        private readonly IWordService _wordService;
        private readonly IDictionaryService _dictionaryService;
        private readonly ICacheService _cacheService;

        public IntentRequestHandlerFactory(
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
            switch (alexaRequest.Request.Intent.Name)
            {
                case "AMAZON.CancelIntent":
                case "AMAZON.StopIntent":
                    return new CancelOrStopIntentHandlerStrategy();

                case "AMAZON.HelpIntent":
                    return new HelpIntentHandlerStrategy();

                case "HelloWorldIntent":
                    return new HelloWorldIntentHandlerStrategy();

                case "WordOfTheDayIntent":
                    return new WordOfTheDayIntentHandlerStrategy(_wordService, _dictionaryService, _cacheService);

                case "AnotherWordIntent":
                    return new AnotherWordIntentHandlerStrategy(_wordService, _dictionaryService, _cacheService);

                case "SayWordIntent":
                    return new SayWordIntentHandlerStrategy(_cacheService);

                default:
                    throw new NotImplementedException();
            }
            
        }
    }
}
