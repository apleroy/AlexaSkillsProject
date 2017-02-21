using AlexaSkillProject.Domain;
using AlexaSkillProject.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaSkillProject.Services.Test
{
    [TestClass()]
    public class AnotherWordIntentHandlerStrategyTests
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

            var cacheService = new Mock<ICacheService>();

            var wordOfTheDayIntentHandlerStrategy = new AnotherWordIntentHandlerStrategy(wordService.Object, dictionaryService.Object, cacheService.Object);

            AlexaRequestPayload alexaRequestPayload = AlexaSkillProjectTestHelpers.GetAlexaRequestPayload("AlexaRequestPayloadTest.json");
            alexaRequestPayload.Request.Type = "IntentRequest";
            alexaRequestPayload.Request.Intent.Name = "AnotherWordIntent";

            // act
            AlexaResponse alexaResponse = wordOfTheDayIntentHandlerStrategy.HandleAlexaRequest(alexaRequestPayload);

            // assert
            Assert.IsTrue(alexaResponse.Response.OutputSpeech.Ssml.Contains("<speak><p>The Word is test</p>"));
            Assert.AreEqual(alexaRequestPayload.Session.Attributes.LastWord, word.WordName);
            Assert.AreEqual(alexaRequestPayload.Session.Attributes.LastWordDefinition, word.Definition);
        }
    }
}
