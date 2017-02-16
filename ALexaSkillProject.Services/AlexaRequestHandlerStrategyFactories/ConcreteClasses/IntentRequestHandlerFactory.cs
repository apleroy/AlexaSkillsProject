using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlexaSkillProject.Domain;
using AlexaSkillProject.Repository;

namespace AlexaSkillProject.Services
{
    public class IntentRequestHandlerFactory
    {
        private readonly IWordService _wordService;
        private readonly IDictionaryService _dictionaryService;

        public IntentRequestHandlerFactory(IWordService wordService, IDictionaryService dictionaryService)
        {
            _wordService = wordService;
            _dictionaryService = dictionaryService;
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
                    return new WordOfTheDayIntentHandlerStrategy(_wordService, _dictionaryService);

                case "AnotherWordIntent":
                    return new AnotherWordIntentHandlerStrategy(_wordService, _dictionaryService);

                case "SayWordIntent":
                    return new SayWordIntentHandlerStrategy();

                default:
                    throw new NotImplementedException();
            }
            
        }
    }
}
