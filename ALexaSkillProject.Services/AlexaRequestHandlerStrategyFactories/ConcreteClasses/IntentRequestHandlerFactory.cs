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

        public IAlexaRequestHandlerStrategy CreateAlexaRequestHandlerStrategy(AlexaRequestPayload alexaRequest)
        {
            switch (alexaRequest.Request.Intent.Name)
            {
                case "NewCoursesIntent":
                    return new NewCoursesIntentHandlerStrategy();

                case "AMAZON.CancelIntent":
                case "AMAZON.StopIntent":
                    return new CancelOrStopIntentHandlerStrategy();

                case "HelloWorldIntent":
                    return new HelloWorldIntentHandlerStrategy();

                case "WordOfTheDayIntent":
                    return new WordOfTheDayIntentHandlerStrategy();

                case "AnotherWordIntent":
                    return new AnotherWordIntentHandlerStrategy();

                case "SayWordIntent":
                    return new SayWordIntentHandlerStrategy();

                case "AMAZON.HelpIntent":
                    return new HelpIntentHandlerStrategy();

                default:
                    throw new NotImplementedException();
            }
            
        }
    }
}
