using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlexaSkillProject.Domain;
using AlexaSkillProject.Core;
using System.Configuration;

namespace AlexaSkillProject.Services
{
    
    /// <summary>
    /// This method handles the 'what is the word of the day' request
    /// </summary>
    public class WordOfTheDayIntentHandlerStrategy : AbstractWordIntentHandlerStrategy
    {
        
        public override string SupportedRequestIntentName
        {
            get
            {
                return "WordOfTheDayIntent";
            }
        }

        public override string SupportedRequestType
        {
            get
            {

                return StrategyHandlerTypes.IntentRequest.ToString();
            }
        }

        public WordOfTheDayIntentHandlerStrategy(
            IWordService wordService, 
            IDictionaryService dictionaryService,
            ICacheService cacheService
            ) : base(wordService, dictionaryService, cacheService) { }

        

        /// <summary>
        /// One record in the Words table is marked as 'WordOfTheDay' == true
        /// </summary>
        /// <returns></returns>
        protected override Word GetWord()
        {
            Word word = null;
            word = _wordService.GetWordOfTheDay();
            while (word == null)
            {
               word = _wordService.GetRandomWord();
            }
            return word;
        }

        
    }

}