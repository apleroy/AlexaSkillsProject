using AlexaSkillProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace AlexaSkillProject.Repository
{
    public class AlexaMemberRepository : Repository<AlexaMember>, IAlexaMemberRepository
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
