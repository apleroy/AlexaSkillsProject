using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlexaSkillProject.Domain;
using AlexaSkillProject.Repository;

namespace AlexaSkillProject.Services
{
    class WordOfTheDayService : IWordOfTheDayService
    {
        private readonly IUnitOfWork _unitOfWork;

        public WordOfTheDayService()
        {
            _unitOfWork = new UnitOfWork(new AlexaSkillProjectDataContext());
        }

        public Word GetRandomWord()
        {
            // get list of all words that are not today's word
            IEnumerable<Word> words = _unitOfWork.Words.Find(w => w.WordOfTheDayDate != DateTime.Now);

            // determine total number and generate the random number
            int count = _unitOfWork.Words.Count();
            int index = new Random().Next(count);

            // second rountrip to get the random word
            Word word = words.Skip(index).FirstOrDefault();

            return word;
        }

        public Word GetWordOfTheDay()
        {
            Word word = new Word();
            try
            {
                word = _unitOfWork.Words.Find(w => w.WordOfTheDayDate == DateTime.Now).FirstOrDefault();
            }
            catch (Exception e)
            {
                // if there is no word for today, get a random one
                word = GetRandomWord();                
            }
            
            return word;
        }
    }
}
