using AlexaSkillProject.Domain;

namespace AlexaSkillProject.Services
{
    public interface IAlexaRequestPersistenceService
    {
        void PersistAlexaRequestAndMember(AlexaRequest alexaRequest);
    }
}
