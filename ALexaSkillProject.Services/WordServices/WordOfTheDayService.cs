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

        public Word GetWordOfTheDay()
        {


            Word w = new Word();
            try
            {
                w = _unitOfWork.Words.Get(2);
            }
            catch (Exception e)
            {
                var m = e.Message;
                return null;
            }
            
            return w;
        }
    }
}
