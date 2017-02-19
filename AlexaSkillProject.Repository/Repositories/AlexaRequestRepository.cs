using AlexaSkillProject.Domain;

namespace AlexaSkillProject.Repository
{
    public class AlexaRequestRepository : AbstractGenericRepository<AlexaRequest>, IAlexaRequestRepository
    {
        public AlexaRequestRepository(AlexaSkillProjectDataContext context) : base(context)
        {
        }

        public AlexaSkillProjectDataContext AlexaSkillProjectDataContext
        {
            get { return Context as AlexaSkillProjectDataContext; }
        }
    }
}
