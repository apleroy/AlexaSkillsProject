using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlexaSkillProject.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlexaSkillProject.Services;
using Moq;
using AlexaSkillProject.Domain;
using AlexaSkillProject.TestHelpers;

namespace AlexaSkillProject.Controllers.Tests
{
    [TestClass()]
    public class AlexaControllerTests
    {


        [TestMethod()]
        public void WordOfTheDayTest()
        {
            // arrange
            AlexaRequestPayload alexaRequestPayload = new AlexaRequestPayload();
            
            var alexaRequestService = new Mock<IAlexaRequestService>();
            alexaRequestService.Setup(s => s.ProcessAlexaRequest(alexaRequestPayload)).Returns(new AlexaResponse("hello"));

            AlexaController controller = new AlexaController(alexaRequestService.Object);
            
            // act
            AlexaResponse result = controller.WordOfTheDay(alexaRequestPayload);

            // assert
            Assert.AreEqual(result.Response.OutputSpeech.Text, "hello");
        }

    }
}