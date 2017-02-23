using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlexaSkillProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using AlexaSkillProject.Domain;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AlexaSkillProject.TestHelpers;

namespace AlexaSkillProject.Services.Tests
{
    [TestClass()]
    public class AlexaRequestHandlerStrategyFactoryTests
    {
        [TestMethod()]
        public void CreateAlexaRequestHandlerStrategyTest_LaunchRequestHandlerStrategy()
        {
            // arrange
            AlexaRequestHandlerStrategyFactory alexaRequestHandlerStrategyFactory = BuildTestAlexaRequestHandlerStrategyFactory();

            var alexaRequestPayload = AlexaSkillProjectTestHelpers.GetAlexaRequestPayload("AlexaRequestPayloadTest.json");
            alexaRequestPayload.Request.Type = "LaunchRequest";

            // act
            var strategyHandler = alexaRequestHandlerStrategyFactory.CreateAlexaRequestHandlerStrategy(alexaRequestPayload);

            // assert
            Assert.AreEqual(strategyHandler.GetType(), typeof(LaunchRequestHandlerStrategy));
        }

        [TestMethod()]
        public void CreateAlexaRequestHandlerStrategyTest_SessionEndedRequestHandlerStrategy()
        {
            // arrange
            AlexaRequestHandlerStrategyFactory alexaRequestHandlerStrategyFactory = BuildTestAlexaRequestHandlerStrategyFactory();

            var alexaRequestPayload = AlexaSkillProjectTestHelpers.GetAlexaRequestPayload("AlexaRequestPayloadTest.json");
            alexaRequestPayload.Request.Type = "SessionEndedRequest";

            // act
            var strategyHandler = alexaRequestHandlerStrategyFactory.CreateAlexaRequestHandlerStrategy(alexaRequestPayload);

            // assert
            Assert.AreEqual(strategyHandler.GetType(), typeof(SessionEndedRequestHandlerStrategy));
        }

        [TestMethod()]
        public void CreateAlexaRequestHandlerStrategyTest_IntentRequestHandlerStrategy_AnotherWordIntentHandlerStrategy()
        {
            // arrange
            AlexaRequestHandlerStrategyFactory alexaRequestHandlerStrategyFactory = BuildTestAlexaRequestHandlerStrategyFactory();

            var alexaRequestPayload = AlexaSkillProjectTestHelpers.GetAlexaRequestPayload("AlexaRequestPayloadTest.json");
            alexaRequestPayload.Request.Type = "IntentRequest";
            alexaRequestPayload.Request.Intent.Name = "AnotherWordIntent";

            // act
            var strategyHandler = alexaRequestHandlerStrategyFactory.CreateAlexaRequestHandlerStrategy(alexaRequestPayload);

            // assert
            Assert.AreEqual(strategyHandler.GetType(), typeof(AnotherWordIntentHandlerStrategy));
        }

        [TestMethod()]
        public void CreateAlexaRequestHandlerStrategyTest_IntentRequestHandlerStrategy_WordOfTheDayIntentHandlerStrategy()
        {
            // arrange
            AlexaRequestHandlerStrategyFactory alexaRequestHandlerStrategyFactory = BuildTestAlexaRequestHandlerStrategyFactory();

            var alexaRequestPayload = AlexaSkillProjectTestHelpers.GetAlexaRequestPayload("AlexaRequestPayloadTest.json");
            alexaRequestPayload.Request.Type = "IntentRequest";
            alexaRequestPayload.Request.Intent.Name = "WordOfTheDayIntent";

            // act
            var strategyHandler = alexaRequestHandlerStrategyFactory.CreateAlexaRequestHandlerStrategy(alexaRequestPayload);

            // assert
            Assert.AreEqual(strategyHandler.GetType(), typeof(WordOfTheDayIntentHandlerStrategy));
        }

        [TestMethod()]
        public void CreateAlexaRequestHandlerStrategyTest_IntentRequestHandlerStrategy_SayWordIntentHandlerStrategy()
        {
            // arrange
            AlexaRequestHandlerStrategyFactory alexaRequestHandlerStrategyFactory = BuildTestAlexaRequestHandlerStrategyFactory();

            var alexaRequestPayload = AlexaSkillProjectTestHelpers.GetAlexaRequestPayload("AlexaRequestPayloadTest.json");
            alexaRequestPayload.Request.Type = "IntentRequest";
            alexaRequestPayload.Request.Intent.Name = "SayWordIntent";

            // act
            var strategyHandler = alexaRequestHandlerStrategyFactory.CreateAlexaRequestHandlerStrategy(alexaRequestPayload);

            // assert
            Assert.AreEqual(strategyHandler.GetType(), typeof(SayWordIntentHandlerStrategy));
        }

        [TestMethod()]
        [ExpectedException(typeof(NotImplementedException))]
        public void CreateAlexaRequestHandlerStrategyTest_IntentRequestHandlerStrategy_NullIntentHandlerStrategy()
        {
            // arrange
            AlexaRequestHandlerStrategyFactory alexaRequestHandlerStrategyFactory = BuildTestAlexaRequestHandlerStrategyFactory();

            var alexaRequestPayload = AlexaSkillProjectTestHelpers.GetAlexaRequestPayload("AlexaRequestPayloadTest.json");
            alexaRequestPayload.Request.Type = "IntentRequest";
            alexaRequestPayload.Request.Intent.Name = "DoesNotExist";

            // act
            var strategyHandler = alexaRequestHandlerStrategyFactory.CreateAlexaRequestHandlerStrategy(alexaRequestPayload);

            // assert
            Assert.AreEqual(strategyHandler.GetType(), typeof(Exception));
        }

        [TestMethod()]
        [ExpectedException(typeof(NotImplementedException))]
        public void CreateAlexaRequestHandlerStrategyTest_NullRequestHandlerStrategy()
        {
            // arrange
            AlexaRequestHandlerStrategyFactory alexaRequestHandlerStrategyFactory = BuildTestAlexaRequestHandlerStrategyFactory();

            var alexaRequestPayload = AlexaSkillProjectTestHelpers.GetAlexaRequestPayload("AlexaRequestPayloadTest.json");
            alexaRequestPayload.Request.Type = "NullRequest";

            // act
            var strategyHandler = alexaRequestHandlerStrategyFactory.CreateAlexaRequestHandlerStrategy(alexaRequestPayload);

            // assert
            Assert.AreEqual(strategyHandler.GetType(), typeof(Exception));
        }



        private AlexaRequestHandlerStrategyFactory BuildTestAlexaRequestHandlerStrategyFactory()
        {
            var wordService = new Mock<IWordService>();
            var dictionaryService = new Mock<IDictionaryService>();
            var cacheService = new Mock<ICacheService>();
            var availableServices = new Mock<IEnumerable<IAlexaRequestHandlerStrategy>> ();

            var alexaRequestHandlerStrategyFactory = new AlexaRequestHandlerStrategyFactory(availableServices.Object);

            return alexaRequestHandlerStrategyFactory;
        }

    }
}