using AlexaSkillProject.Domain;
using AlexaSkillProject.Services;
using log4net;
using System.Web.Http;

namespace AlexaSkillProject.Controllers
{
    /// <summary>
    /// WebAPI controller to accept AlexaRequests
    /// </summary>
    public class AlexaController : ApiController
    {
        private readonly IAlexaRequestService _alexaRequestService;
        
        public AlexaController(IAlexaRequestService alexaRequestService)
        {
            _alexaRequestService = alexaRequestService;
        }

        [HttpPost, Route("api/v1/alexa/test")]
        public dynamic Test(AlexaRequestPayload alexaRequestInput)
        {
            return new AlexaResponse("Non db call working");
        }


        [HttpPost, Route("api/v1/alexa/grammartool")]
        public dynamic WordOfTheDay(AlexaRequestPayload alexaRequestInput)
        {
            return _alexaRequestService.ProcessAlexaRequest(alexaRequestInput); 
        }


    }
}
