using AlexaSkillProject.Domain;

namespace AlexaSkillProject.Services
{
    public interface IAlexaRequestService
    {
        AlexaResponse ProcessAlexaRequest(AlexaRequestPayload alexaRequestInput);
    }
}
