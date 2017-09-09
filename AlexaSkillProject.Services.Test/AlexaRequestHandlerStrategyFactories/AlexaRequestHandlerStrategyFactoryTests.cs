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
            IList<IAlexaRequestHandlerStrategy> availableStrategies = new List<IAlexaRequestHandlerStrategy>();
            AlexaRequestHandlerStrategyFactory alexaRequestHandlerStrategyFactory = BuildTestAlexaRequestHandlerStrategyFactory();

            var alexaRequestPayload = AlexaSkillProjectTestHelpers.GetAlexaRequestPayload("AlexaRequestPayloadTest.json");
            alexaRequestPayload.Request.Type = "LaunchRequest";

            // act
            var strategyHandler = alexaRequestHandlerStrategyFactory.CreateAlexaRequestHandlerStrategy(alexaRequestPayload);

            // assert
            Assert.AreEqual(typeof(LaunchRequestHandlerStrategy), strategyHandler.GetType());
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
            Assert.AreEqual(typeof(SessionEndedRequestHandlerStrategy), strategyHandler.GetType());
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
            Assert.AreEqual(typeof(AnotherWordIntentHandlerStrategy), strategyHandler.GetType());
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
            Assert.AreEqual(typeof(WordOfTheDayIntentHandlerStrategy), strategyHandler.GetType());
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
            Assert.AreEqual(typeof(SayWordIntentHandlerStrategy), strategyHandler.GetType());
        }

        [TestMethod()]
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
            Assert.IsNull(strategyHandler);
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
            Assert.AreEqual(typeof(Exception), strategyHandler.GetType());
        }



        private AlexaRequestHandlerStrategyFactory BuildTestAlexaRequestHandlerStrategyFactory()
        {
            var wordService = new Mock<IWordService>().Object;
            var dictionaryService = new Mock<IDictionaryService>().Object;
            var cacheService = new Mock<ICacheService>().Object;
            var availableServices = new Mock<IEnumerable<IAlexaRequestHandlerStrategy>> ();

            List<IAlexaRequestHandlerStrategy> availableStrategies = new List<IAlexaRequestHandlerStrategy>
            {
                new LaunchRequestHandlerStrategy(),
                new SayWordIntentHandlerStrategy(cacheService),
                new AnotherWordIntentHandlerStrategy(wordService, dictionaryService, cacheService),
                new CancelIntentHandlerStrategy(),
                new HelpIntentHandlerStrategy(),
                new SessionEndedRequestHandlerStrategy(),
                new StopIntentHandlerStrategy(),
                new WordOfTheDayIntentHandlerStrategy(wordService, dictionaryService, cacheService)
            };

            var alexaRequestHandlerStrategyFactory = new AlexaRequestHandlerStrategyFactory(availableStrategies);

            return alexaRequestHandlerStrategyFactory;
        }

    }
}