using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using AlexaSkillProject.Domain;
using System.Configuration;

namespace AlexaSkillProject.Repository
{
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
