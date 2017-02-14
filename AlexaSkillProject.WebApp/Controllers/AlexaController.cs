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
        //private readonly IAlexaRequestValidationService _alexaRequestValidationService;

        public AlexaController(IAlexaRequestService alexaRequestService)
        {
            _alexaRequestService = alexaRequestService;
            //_alexaRequestValidationService = alexaRequestValidationService;
        }

        [HttpPost, Route("api/v1/alexa/test")]
        public dynamic Test(AlexaRequestInputModel alexaRequestInput)
        {
            return new AlexaResponse("Working");
        }

        [HttpPost, Route("api/v1/alexa/demo")]
        public dynamic Yoda(AlexaRequestInputModel alexaRequestInput)
        {
            return _alexaRequestService.ProcessAlexaRequest(alexaRequestInput);          
        }

        [HttpPost, Route("api/v1/alexa/wod")]
        public dynamic WordOfTheDay(AlexaRequestInputModel alexaRequestInput)
        {
            //HttpContent requestContent = Request.Content;
            //string jsonContent = requestContent.ReadAsStringAsync().Result;

            //HttpRequestMessage httpRequest = Request;
            //AlexaRequestInputModel alexaRequestInputModel = _alexaRequestValidationService.ValidateAlexaHttpRequest(httpRequest);

            // var result = _alexaRequestValidationService.ValidateAlexaHttpRequest(httpRequest);
            // validate request and pass in message
            // parse per json contract
            // pass to service
            return _alexaRequestService.ProcessAlexaRequest(alexaRequestInput);
            
        }


    }
}
