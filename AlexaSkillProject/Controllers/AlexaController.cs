using AlexaSkillProject.Domain;
using AlexaSkillProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AlexaSkillProject.Controllers
{
    public class AlexaController : ApiController
    {

        private readonly IAlexaRequestService _alexaRequestService;
        private readonly IDummyService _dummyService;

        public AlexaController(IAlexaRequestService alexaRequestService)
        {
            _alexaRequestService = alexaRequestService;
        }

        //public AlexaController(IDummyService dummyService)
        //{
        //    _dummyService = dummyService;
        //}


        [HttpPost, Route("api/alexa/demo")]
        public dynamic Yoda(AlexaRequestInputModel alexaRequestInput)
        {
            return _alexaRequestService.ProcessAlexaRequest(alexaRequestInput);
            //var a = _dummyService.ReturnString();
            //return a;
        }

        
    }
}
