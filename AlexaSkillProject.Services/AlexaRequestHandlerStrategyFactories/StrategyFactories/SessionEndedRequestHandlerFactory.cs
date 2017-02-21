using AlexaSkillProject.Domain;

namespace AlexaSkillProject.Services
{
    public class SessionEndedRequestHandlerFactory
    {
        
        public IAlexaRequestHandlerStrategy CreateAlexaRequestHandlerStrategy(AlexaRequestPayload alexaRequest)
        {
            return new SessionEndedRequestHandlerStrategy();
        }
    }
}
