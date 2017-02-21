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
using AlexaSkillProject.Core;

namespace AlexaSkillProject.Services.Tests
{
    [TestClass()]
    public class SayWordIntentHandlerStrategyTests
    {
        [TestMethod()]
        public void HandleAlexaRequestTest_HandlesSuccessCorrectly()
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

            AlexaRequestPayload alexaRequestPayload = AlexaSkillProjectTestHelpers.GetAlexaRequestPayload("AlexaRequestPayloadSayWordIntent.json");

            alexaRequestPayload.Session.Attributes.LastWord = word.WordName;
            alexaRequestPayload.Session.Attributes.LastWordDefinition = word.Definition;

            var cacheService = new Mock<ICacheService>();
            cacheService.Setup(c => c.RetrieveAlexaRequest(alexaRequestPayload.Session.SessionId)).Returns(alexaRequestPayload);


            var wordOfTheDayIntentHandlerStrategy = new SayWordIntentHandlerStrategy(cacheService.Object);

            // act
            AlexaResponse alexaResponse = wordOfTheDayIntentHandlerStrategy.HandleAlexaRequest(alexaRequestPayload);

           
            // assert
            Assert.IsTrue(alexaResponse.Response.OutputSpeech.Ssml.Contains(
                Utility.GetDescriptionFromEnumValue(SuccessPhrases.Great)));

            //Assert.AreEqual(alexaRequestPayload.Session.Attributes.LastWord, word.WordName);
            //Assert.AreEqual(alexaRequestPayload.Session.Attributes.LastWordDefinition, word.Definition);
        }

        [TestMethod()]
        public void HandleAlexaRequestTest_HandlesFailureCorrectly()
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

            // get the example payload and set the session attributes
            AlexaRequestPayload alexaRequestPayload = AlexaSkillProjectTestHelpers.GetAlexaRequestPayload("AlexaRequestPayloadSayWordIntent.json");
            alexaRequestPayload.Session.Attributes.LastWord = "Incorrect";
            alexaRequestPayload.Session.Attributes.LastWordDefinition = "Incorrect definition";

            // these will be the attributes returned by the mock cache service
            var cacheService = new Mock<ICacheService>();
            cacheService.Setup(c => c.RetrieveAlexaRequest(alexaRequestPayload.Session.SessionId)).Returns(alexaRequestPayload);

            var wordOfTheDayIntentHandlerStrategy = new SayWordIntentHandlerStrategy(cacheService.Object);

            // reset the session attributes to not match the cached session
            alexaRequestPayload.Session.Attributes.LastWord = word.WordName;
            alexaRequestPayload.Session.Attributes.LastWord = word.Definition;

            // act
            AlexaResponse alexaResponse = wordOfTheDayIntentHandlerStrategy.HandleAlexaRequest(alexaRequestPayload);


            // assert
            Assert.IsTrue(alexaResponse.Response.OutputSpeech.Ssml.Contains(
                Utility.GetDescriptionFromEnumValue(ErrorPhrases.Incorrect)));

        }
    }
}