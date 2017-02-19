using AlexaSkillProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AlexaSkillProject.Services
{
    // IDictionaryService is responsible for transforming an input Word into a dictionary
    // This service could be implemented with the assistance of a third party API
    // or could be implemented with a db call for a word object
    public interface IDictionaryService
    {
        // Dictionary service should be able to accept a word and pass back dictionary
        // with word, part of speech, definition, example

        Dictionary<WordEnum, string> GetWordDictionaryFromString(string word);
    }
}
