using AlexaSkillProject.Domain;

namespace AlexaSkillProject.Services
{
    public class HelloWorldIntentHandlerStrategy : IAlexaRequestHandlerStrategy
    {
        public AlexaResponse HandleAlexaRequest(AlexaRequestPayload alexaRequest)
        {
            return new AlexaResponse("Hello Andy");
        }
    }
}