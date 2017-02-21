using AlexaSkillProject.Domain;

namespace AlexaSkillProject.Services
{
    public class LaunchRequestHandlerFactory
    {
        
        public IAlexaRequestHandlerStrategy CreateAlexaRequestHandlerStrategy(AlexaRequestPayload alexaRequest)
        {
            return new LaunchRequestHandlerStrategy();
        }
    }
}
