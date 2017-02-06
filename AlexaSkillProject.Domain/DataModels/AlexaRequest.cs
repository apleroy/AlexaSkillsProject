using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaSkillProject.Domain
{
    public class AlexaRequest
    {
        public int Id { get; set; }
        public int AlexaMemberId { get; set; }
        public string SessionId { get; set; }
        public string AppId { get; set; }
        public string RequestId { get; set; }
        public string UserId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Intent { get; set; }
        public string Slots { get; set; }
        public bool IsNew { get; set; }
        public string Version { get; set; }
        public string Type { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual AlexaMember AlexaMember { get; set; }

        private List<KeyValuePair<string, string>> slotsList = new List<KeyValuePair<string, string>>();

        public List<KeyValuePair<string, string>> SlotsList
        {
            get
            {
                return slotsList;
            }
            set
            {
                slotsList = value;

                var slots = new StringBuilder();

                slotsList.ForEach(s => slots.AppendFormat("{0}|{1},", s.Key, s.Value));

                Slots = slots.ToString().TrimEnd(',');
            }
        }
    }
}
