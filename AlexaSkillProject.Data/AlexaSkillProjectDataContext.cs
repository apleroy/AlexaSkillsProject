using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using AlexaSkillProject.Domain;

namespace AlexaSkillProject.Repository
{
    public class AlexaSkillProjectDataContext : DbContext
    {
        public AlexaSkillProjectDataContext() : base("AlexaSkillProjectDataContext")
        {
        }

        public DbSet<AlexaRequest> AlexaRequests { get; set; }
        public DbSet<AlexaMember> AlexaMembers { get; set; }

    }
}
