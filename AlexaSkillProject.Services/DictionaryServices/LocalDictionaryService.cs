using AlexaSkillProject.Domain;
using AlexaSkillProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaSkillProject.Services
{
    public class LocalDictionaryService : IDictionaryService
    {

        private readonly IUnitOfWork _unitOfWork;

        public LocalDictionaryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Dictionary<WordEnum, string> GetWordDictionaryFromString(string word)
        {
            // pull the Word object out of the repo
            Word wordObject = _unitOfWork.Words.Find(w => w.WordName == word).FirstOrDefault();

            // transform it into dictionary
            Dictionary<WordEnum, string> responseDictionary = new Dictionary<WordEnum, string>();
            responseDictionary[WordEnum.Word] = wordObject.WordName;
            responseDictionary[WordEnum.WordPartOfSpeech] = wordObject.PartOfSpeech;
            responseDictionary[WordEnum.WordDefinition] = wordObject.Definition;
            responseDictionary[WordEnum.WordExample] = wordObject.Example;

            // return the dictionary
            return responseDictionary;
        }
    }
}
