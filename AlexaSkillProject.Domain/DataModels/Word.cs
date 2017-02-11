using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaSkillProject.Domain
{
    public class Word
    {
        public int Id { get; set; }
        public string WordName { get; set; }
        public DateTime WordOfTheDayDate { get; set; }
    }
}
