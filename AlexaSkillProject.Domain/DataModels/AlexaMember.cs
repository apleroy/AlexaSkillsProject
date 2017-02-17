using System;
using System.Collections.Generic;

namespace AlexaSkillProject.Domain
{
    /// <summary>
    /// AlexaMember refers to a 'user' saved from an alexa request
    /// Defined by a unique GUID like string from amazon and stored in the AlexaUserID
    /// </summary>
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
