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
        //private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IAlexaRequestService _alexaRequestService;

        public AlexaController(IAlexaRequestService alexaRequestService)
        {
            _alexaRequestService = alexaRequestService;
        }

        [HttpPost, Route("api/alexa/test")]
        public dynamic Test(AlexaRequestInputModel alexaRequestInput)
        {
            return new AlexaResponse("Working");
        }

        [HttpPost, Route("api/alexa/demo")]
        public dynamic Yoda(AlexaRequestInputModel alexaRequestInput)
        {
            try
            {
                return _alexaRequestService.ProcessAlexaRequest(alexaRequestInput);
            }
            catch (Exception exception)
            {
                //Log.Error("Error: " + exception.Message);
            }
            return null;
        }

        [HttpPost, Route("api/alexa/wod")]
        public dynamic WordOfTheDay(AlexaRequestInputModel alexaRequestInput)
        {
            try
            { 
                return _alexaRequestService.ProcessAlexaRequest(alexaRequestInput);
            }
            catch (Exception exception)
            {
                //Log.Error("Error: " + exception.Message);
            }
            return null;

        }


    }
}
