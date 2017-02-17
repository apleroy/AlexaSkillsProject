using System.Data.Entity;
using AlexaSkillProject.Domain;

namespace AlexaSkillProject.Repository
{
    /// <summary>
    /// DataContext shared across the application
    /// </summary>
    public class AlexaSkillProjectDataContext : DbContext
    {
        public AlexaSkillProjectDataContext() :
            base("AlexaSkillProjectDataContext")
        {
        }

        public DbSet<AlexaRequest> AlexaRequests { get; set; }
        public DbSet<AlexaMember> AlexaMembers { get; set; }
        public DbSet<Word> Words { get; set; }


    }
}
