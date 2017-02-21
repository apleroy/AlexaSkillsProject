using AlexaSkillProject.Domain;

namespace AlexaSkillProject.Services
{
    public interface IAlexaRequestValidationService
    {
        SpeechletRequestValidationResult ValidateAlexaRequest(AlexaRequestPayload alexaRequest);
        
    }
}
