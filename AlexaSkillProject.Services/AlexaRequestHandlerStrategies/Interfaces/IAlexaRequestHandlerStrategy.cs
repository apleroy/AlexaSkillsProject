using AlexaSkillProject.Domain;

namespace AlexaSkillProject.Services
{
    /// <summary>
    /// This is the top level handler strategy for all incoming alexa requests
    /// </summary>
    public interface IAlexaRequestHandlerStrategy
    {
        AlexaResponse HandleAlexaRequest(AlexaRequestPayload alexaRequest);
    }
}
