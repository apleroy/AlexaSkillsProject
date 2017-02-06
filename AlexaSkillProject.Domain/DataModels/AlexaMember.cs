using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaSkillProject.Domain
{
    public class AlexaMember
    {
        public AlexaMember()
        {
            this.AlexaRequests = new HashSet<AlexaRequest>();
        }

        public int Id { get; set; }
        public string AlexaUserId { get; set; }
        public int RequestCount { get; set; }
        public DateTime LastRequestDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<AlexaRequest> AlexaRequests { get; set; }
    }
}
