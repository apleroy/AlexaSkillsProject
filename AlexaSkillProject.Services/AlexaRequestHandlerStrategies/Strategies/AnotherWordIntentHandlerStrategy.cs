using AlexaSkillProject.Domain;
using System.Collections.Generic;
using System.Configuration;

namespace AlexaSkillProject.Services
{

    /// <summary>
    /// This method is called for users requesting 'another word'
    /// Implements a random word override method
    /// </summary>
    public class AnotherWordIntentHandlerStrategy : AbstractWordIntentHandlerStrategy
    {

        public AnotherWordIntentHandlerStrategy(
            IWordService wordService, 
            IDictionaryService dictionaryService,
            ICacheService cacheService
            ) : base(wordService, dictionaryService, cacheService) { }


        protected override Word GetWord()
        {
            Word word = null;
            while (word == null)
            {
                word = _wordService.GetRandomWord();
            }
            return word;
        } 

    }

        
}
