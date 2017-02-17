using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlexaSkillProject.Domain
{
    /// <summary>
    /// A Word is a domain object mapped to EF
    /// All fields are required to preserve quality for the user 
    /// </summary>
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

        /// <summary>
        /// Set to true of this word is the 'word of the day'
        /// </summary>
        [Required]
        public bool WordOfTheDay { get; set; }
    }
}
