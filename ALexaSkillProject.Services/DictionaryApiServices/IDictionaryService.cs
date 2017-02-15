using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AlexaSkillProject.Services
{
    public interface IDictionaryService
    {
        // Dictionary service should be able to accept a word and pass back dictionary
        // with word, part of speech, definition, example

        Dictionary<WordEnum, string> GetWordDictionaryFromString(string word);
    }
}
