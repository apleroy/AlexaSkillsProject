using AlexaSkillProject.Domain;
using AlexaSkillProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;


namespace AlexaSkillProject.Services
{
    public class WordService : IWordService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDictionaryService _dictionaryService;

        public WordService(IUnitOfWork unitOfWork, IDictionaryService dictionaryService)
        {
            _unitOfWork = unitOfWork; 
            _dictionaryService = dictionaryService;
        }

        // CRUD methods 

        // Index
        public List<Word> Words()
        {
            return _unitOfWork.Words.GetAll().ToList();
            
        }

        // Post
        public void AddWord(Word word)
        {
            _unitOfWork.Words.Add(word);
            _unitOfWork.Complete();
        }

        // Get
        public Word Find(int? id)
        {
            if (id == null)
                return null;
            Word word = _unitOfWork.Words.Get(id.Value);
            return word;
        }

        // Put
        public void UpdateWord(Word word)
        {
            _unitOfWork.Words.UpdateEntity(word);
            _unitOfWork.Complete();

        }

        // Delete
        public void Remove(Word word)
        {
            _unitOfWork.Words.Remove(word);
            _unitOfWork.Complete();
            
        }

        
        public Word FindWordByName(string word)
        {
            return _unitOfWork.Words.Find(w => w.WordName == word).FirstOrDefault();
        }


        public Word GetWordOfTheDay()
        {
            Word word = new Word();
            word = _unitOfWork.Words.Find(w => w.WordOfTheDay == true).FirstOrDefault();

            // if there is no word for today, get a random one
            if (word == null)
            {
                word = GetRandomWord();
            }

            return word;
        }

        public Word GetRandomWord()
        {
            IEnumerable<Word> words = _unitOfWork.Words.Find(w => !w.WordOfTheDay);

            // determine total number and generate the random number
            int count = _unitOfWork.Words.Count();
            int index = new Random().Next(count);

            // second rountrip to get the random word
            Word word = words.Skip(index).FirstOrDefault();

            return word;
        }


        /// <summary>
        /// Can be used to hit the pearsons api to preprocess words and save them locally
        /// </summary>
        /// <param name="word"></param>
        public void GetAndSaveWordInformation(string word)
        {
            
                if (word == null)
                {
                    throw new Exception("Word is null");
                }
                Dictionary<WordEnum, string> responseDictionary = _dictionaryService.GetWordDictionaryFromString(word);

                if (responseDictionary[WordEnum.Word] != null
                    && responseDictionary[WordEnum.Word].Equals(word)
                    && responseDictionary[WordEnum.WordPartOfSpeech] != null
                    && responseDictionary[WordEnum.WordDefinition] != null
                    && responseDictionary[WordEnum.WordExample] != null)
                {
                    Word w = new Word
                    {
                        WordName = responseDictionary[WordEnum.Word],
                        PartOfSpeech = responseDictionary[WordEnum.WordPartOfSpeech],
                        Definition = responseDictionary[WordEnum.WordDefinition],
                        Example = responseDictionary[WordEnum.WordExample]
                    };

                    _unitOfWork.Words.Add(w);
                }
            
        }

        /// <summary>
        /// Make sure the unit of work is disposed between calls, otherwise concurrent access exceptions can occur
        /// See an Entity Framework crud controller for examples at the controller (not the service) level
        /// </summary>
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
