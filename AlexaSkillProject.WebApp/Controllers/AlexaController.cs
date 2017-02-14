using AlexaSkillProject.Domain;
using AlexaSkillProject.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AlexaSkillProject.Controllers
{
    public class AlexaController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IAlexaRequestService _alexaRequestService;
        
        public AlexaController(IAlexaRequestService alexaRequestService)
        {
            _alexaRequestService = alexaRequestService;
        }

        [HttpPost, Route("api/v1/alexa/test")]
        public dynamic Test(AlexaRequestPayload alexaRequestInput)
        {
            return new AlexaResponse("Working");
        }

        [HttpPost, Route("api/v1/alexa/demo")]
        public dynamic Yoda(AlexaRequestPayload alexaRequestInput)
        {
            return _alexaRequestService.ProcessAlexaRequest(alexaRequestInput);          
        }

        [HttpPost, Route("api/v1/alexa/wod")]
        public dynamic WordOfTheDay(AlexaRequestPayload alexaRequestInput)
        {
            return _alexaRequestService.ProcessAlexaRequest(alexaRequestInput); 
        }


    }
}
