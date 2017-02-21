using AlexaSkillProject.Domain;

namespace AlexaSkillProject.Services
{
    public interface IAlexaRequestMapper
    {
        AlexaRequest MapAlexaRequest(AlexaRequestPayload alexaRequest);
    }
}
