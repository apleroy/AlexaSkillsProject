﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlexaSkillProject.Domain;
using AlexaSkillProject.Repository;
using System.Linq.Expressions;

namespace AlexaSkillProject.Services
{
    public class WordService : IWordService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDictionaryService _dictionaryService;

        public WordService(IUnitOfWork unitOfWork, IDictionaryService dictionaryService)
        {
            _unitOfWork = unitOfWork; //new UnitOfWork(new AlexaSkillProjectDataContext());
            _dictionaryService = dictionaryService;

        }

        // CRUD methods - pulled out of controller from Entity Framework

        // Index
        public List<Word> Words()
        {
            return _unitOfWork.Words.GetAll().ToList();
        }

        // Post
        public void AddWord(Word word)
        {
            _unitOfWork.Words.Add(word);
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
            _unitOfWork.Words.Update(word);
        }

        // Delete
        public void Remove(Word word)
        {
            throw new NotImplementedException();
        }

        
        public Word FindWordByName(string word)
        {
            return _unitOfWork.Words.Find(w => w.WordName == word).FirstOrDefault();
        }

        public Word GetWordOfTheDay()
        {
            Word word = new Word();

            int dayNumber = DateTime.Today.DayOfYear;
            word = _unitOfWork.Words.Get(dayNumber);

            // if there is no word for today, get a random one
            if (word == null)
            {
                word = GetRandomWord();
            }

            return word;
        }

        public Word GetRandomWord()
        {

            int dayNumber = DateTime.Today.DayOfYear;
            IEnumerable<Word> words = _unitOfWork.Words.Find(w => w.Id == dayNumber);

            // determine total number and generate the random number
            int count = _unitOfWork.Words.Count();
            int index = new Random().Next(count);

            // second rountrip to get the random word
            Word word = words.Skip(index).FirstOrDefault();

            return word;
        }


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
                _unitOfWork.Complete();
            }
        }

        
    }
}
