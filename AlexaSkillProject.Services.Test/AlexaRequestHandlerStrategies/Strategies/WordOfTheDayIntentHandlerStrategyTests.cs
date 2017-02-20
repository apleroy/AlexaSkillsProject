using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlexaSkillProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using AlexaSkillProject.TestHelpers;
using AlexaSkillProject.Domain;

namespace AlexaSkillProject.Services.Tests
{
    [TestClass()]
    public class WordOfTheDayIntentHandlerStrategyTests
    {
        [TestMethod()]
        public void HandleAlexaRequestTest()
        {
            // arrange
            Word word = new Word
            {
                Id = 1,
                WordName = "test",
                PartOfSpeech = "noun",
                Definition = "a test",
                Example = " a moq uses a test"
            };

            var wordService = new Mock<IWordService>();
            wordService.Setup(w => w.GetRandomWord()).Returns(word);


            Dictionary<WordEnum, string> mockDictionary = new Dictionary<WordEnum, string>();
            mockDictionary[WordEnum.Word] = word.WordName;
            mockDictionary[WordEnum.WordPartOfSpeech] = word.PartOfSpeech;
            mockDictionary[WordEnum.WordDefinition] = word.Definition;
            mockDictionary[WordEnum.WordExample] = word.Example;


            var dictionaryService = new Mock<IDictionaryService>();
            dictionaryService.Setup(w => w.GetWordDictionaryFromString(word.WordName)).Returns(mockDictionary);

            var wordOfTheDayIntentHandlerStrategy = new WordOfTheDayIntentHandlerStrategy(wordService.Object, dictionaryService.Object);

            AlexaRequestPayload alexaRequestPayload = AlexaSkillProjectTestHelpers.GetAlexaRequestPayload();
            alexaRequestPayload.Request.Type = "IntentRequest";
            alexaRequestPayload.Request.Intent.Name = "WordOfTheDayIntent";

            // act
            AlexaResponse alexaResponse = wordOfTheDayIntentHandlerStrategy.HandleAlexaRequest(alexaRequestPayload);

            // assert
            Assert.IsTrue(alexaResponse.Response.OutputSpeech.Ssml.Contains("<speak><p>The Word is test</p>"));
            Assert.AreEqual(alexaRequestPayload.Session.Attributes.LastWord, word.WordName);
            Assert.AreEqual(alexaRequestPayload.Session.Attributes.LastWordDefinition, word.Definition);
        }




        //private AbstractWordIntentHandlerStrategy BuildAbstractWordIntentHandlerStrategy()
        //{
        //    //
        //}
    }
}