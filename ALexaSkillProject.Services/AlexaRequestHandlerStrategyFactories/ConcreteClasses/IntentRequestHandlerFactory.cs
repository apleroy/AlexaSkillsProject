using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlexaSkillProject.Domain;

namespace AlexaSkillProject.Services
{
    public class IntentRequestHandlerFactory
    {
        
        public IAlexaRequestHandlerStrategy CreateAlexaRequestHandlerStrategy(AlexaRequest alexaRequest)
        {
            switch (alexaRequest.Intent)
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


                case "AMAZON.HelpIntent":
                    return new HelpIntentHandlerStrategy();

                default:
                    throw new NotImplementedException();
            }
            
        }
    }
}
