using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaSkillProject.Domain
{
    public class Word
    {
        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(450)]
        public string WordName { get; set; }

        [Required]
        public string PartOfSpeech { get; set; }

        [Required]
        public string Definition { get; set; }

        [Required]
        public string Example { get; set; }

        [Required]
        public bool WordOfTheDay { get; set; }
    }
}
