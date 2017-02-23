using AlexaSkillProject.Domain;
using System.Collections.Generic;
using System.Configuration;
using System;

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

        public override string SupportedRequestIntentName
        {
            get
            {
                return "AnotherWordIntent";
            }
        }

        public override string SupportedRequestType
        {
            get
            { 
                return StrategyHandlerTypes.IntentRequest.ToString();
            }
        }

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
