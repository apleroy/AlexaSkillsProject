using AlexaSkillProject.Domain;

namespace AlexaSkillProject.Repository
{
    /// <summary>
    /// Base WordRepository 
    /// </summary>
    public class WordRepository : AbstractGenericRepository<Word>, IWordRepository
    {
        public WordRepository(AlexaSkillProjectDataContext context) : base(context)
        {
        }

        public AlexaSkillProjectDataContext AlexaSkillProjectDataContext
        {
            get { return Context as AlexaSkillProjectDataContext; }
        }
    }
}
