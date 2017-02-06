using AlexaSkillProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace AlexaSkillProject.Repository
{
    public class AlexaRequestRepository : Repository<AlexaRequest>, IAlexaRequestRepository
    {
        public AlexaRequestRepository(AlexaSkillProjectDataContext context) : base(context)
        {
        }

        public IEnumerable<AlexaRequest> GetRequestsWithMembers(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public AlexaSkillProjectDataContext AlexaSkillProjectDataContext
        {
            get { return Context as AlexaSkillProjectDataContext; }
        }
    }
}
