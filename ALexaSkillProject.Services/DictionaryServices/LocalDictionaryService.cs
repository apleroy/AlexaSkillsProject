using AlexaSkillProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaSkillProject.Services.DictionaryServices
{
    public class LocalDictionaryService : IDictionaryService
    {

        private readonly IWordService _wordService;

        public LocalDictionaryService(IWordService wordService)
        {
            _wordService = wordService;
        }

        public Dictionary<WordEnum, string> GetWordDictionaryFromString(string word)
        {
            // pull the Word object out of the repo
            Word w = _wordService.FindWordByName(word);

            // transform it into dictionary
            Dictionary<WordEnum, string> responseDictionary = new Dictionary<WordEnum, string>();
            responseDictionary[WordEnum.Word] = w.WordName;
            responseDictionary[WordEnum.WordPartOfSpeech] = w.PartOfSpeech;
            responseDictionary[WordEnum.WordDefinition] = w.Definition;
            responseDictionary[WordEnum.WordExample] = w.Example;

            // return the dictionary
            return responseDictionary;
        }
    }
}
