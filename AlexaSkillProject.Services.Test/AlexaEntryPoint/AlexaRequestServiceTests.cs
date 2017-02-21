using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlexaSkillProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using AlexaSkillProject.Domain;
using AlexaSkillProject.TestHelpers;

namespace AlexaSkillProject.Services.Tests
{
    [TestClass()]
    public class AlexaRequestServiceTests
    {
        [TestMethod()]
        public void ProcessAlexaRequestTest()
        {
            // arrange
            AlexaRequestPayload alexaRequestPayload = AlexaSkillProjectTestHelpers.GetAlexaRequestPayload("AlexaRequestPayloadTest.json");

            var alexaRequestMapper = new Mock<IAlexaRequestMapper>();

            var alexaRequestPersistenceService = new Mock<IAlexaRequestPersistenceService>();

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

            var cacheService = new Mock<ICacheService>();
            //cacheService.Setup(c => c.CacheAlexaRequest(alexaRequestPayload))

            Dictionary<WordEnum, string> mockDictionary = new Dictionary<WordEnum, string>();
            mockDictionary[WordEnum.Word] = word.WordName;
            mockDictionary[WordEnum.WordPartOfSpeech] = word.PartOfSpeech;
            mockDictionary[WordEnum.WordDefinition] = word.Definition;
            mockDictionary[WordEnum.WordExample] = word.Example;


            var dictionaryService = new Mock<IDictionaryService>();
            dictionaryService.Setup(w => w.GetWordDictionaryFromString(word.WordName)).Returns(mockDictionary);

            var wordOfTheDayIntentHandlerStrategy = new WordOfTheDayIntentHandlerStrategy(wordService.Object, dictionaryService.Object, cacheService.Object);

            var alexaRequestHandlerStrategyFactory = new Mock<IAlexaRequestHandlerStrategyFactory>();
            alexaRequestHandlerStrategyFactory.Setup(x => x.CreateAlexaRequestHandlerStrategy(alexaRequestPayload)).Returns(wordOfTheDayIntentHandlerStrategy);


            var alexaRequestValidationService = new Mock<IAlexaRequestValidationService>();
            alexaRequestValidationService.Setup(x => x.ValidateAlexaRequest(alexaRequestPayload)).Returns(SpeechletRequestValidationResult.OK);


            var alexaRequestService = new AlexaRequestService(
                alexaRequestMapper.Object,
                alexaRequestPersistenceService.Object,
                alexaRequestHandlerStrategyFactory.Object,
                alexaRequestValidationService.Object
                );

            
            alexaRequestPayload.Request.Type = "IntentRequest";
            alexaRequestPayload.Request.Intent.Name = "WordOfTheDayIntent";

            // act
            AlexaResponse alexaResponse = alexaRequestService.ProcessAlexaRequest(alexaRequestPayload);

            // assert
            Assert.IsTrue(alexaResponse.Response.OutputSpeech.Ssml.Contains("<speak><p>The Word is test</p>"));
            
        }
    }
}