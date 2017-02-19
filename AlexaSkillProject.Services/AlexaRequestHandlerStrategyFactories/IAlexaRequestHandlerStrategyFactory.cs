using AlexaSkillProject.Domain;

namespace AlexaSkillProject.Services
{
    public interface IAlexaRequestHandlerStrategyFactory
    {
        IAlexaRequestHandlerStrategy CreateAlexaRequestHandlerStrategy(AlexaRequestPayload alexaRequest);
    }
}
