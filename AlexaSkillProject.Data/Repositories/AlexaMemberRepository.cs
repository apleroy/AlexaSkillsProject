using AlexaSkillProject.Domain;

namespace AlexaSkillProject.Repository
{
    public class AlexaMemberRepository : AbstractGenericRepository<AlexaMember>, IAlexaMemberRepository
    {
        public AlexaMemberRepository(AlexaSkillProjectDataContext context) : base(context)
        {
        }

        public AlexaSkillProjectDataContext AlexaSkillProjectDataContext
        {
            get { return Context as AlexaSkillProjectDataContext; }
        }
    }
}
