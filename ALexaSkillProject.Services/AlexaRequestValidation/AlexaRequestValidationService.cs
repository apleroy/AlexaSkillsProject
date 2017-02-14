using AlexaSkillProject.Core;
using AlexaSkillProject.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AlexaSkillProject.Services
{
    

    public class AlexaRequestValidationService : IAlexaRequestValidationService
    {
        
        public SpeechletRequestValidationResult ValidateAlexaRequest(AlexaRequestPayload alexaRequest)
        {
            SpeechletRequestValidationResult validationResult = SpeechletRequestValidationResult.OK;

            if (!ConfigurationSettings.AppSettings["Mode"].Equals("Debug"))
            {
                // check timestamp
                if (!VerifyRequestTimestamp(alexaRequest, DateTime.UtcNow))
                {
                    validationResult = SpeechletRequestValidationResult.InvalidTimestamp;
                    throw new Exception(validationResult.ToString());
                }

                // check app id
                if (!VerifyApplicationIdHeader(alexaRequest))
                {
                    validationResult = SpeechletRequestValidationResult.InvalidAppId;
                    throw new Exception(validationResult.ToString());
                }

            }

            return validationResult;
            
        }


        private bool VerifyRequestTimestamp(AlexaRequestPayload alexaRequest, DateTime referenceTimeUtc)
        {
            // verify timestamp is within tolerance
            var diff = referenceTimeUtc - alexaRequest.Request.Timestamp;
            return (Math.Abs((decimal)diff.TotalSeconds) <= AlexaSdk.TIMESTAMP_TOLERANCE_SEC);
        }

        private bool VerifyApplicationIdHeader(AlexaRequestPayload alexaRequest)
        {
            string alexaApplicationId = ConfigurationSettings.AppSettings["AlexaApplicationId"];

            return alexaRequest.Session.Application.ApplicationId.Equals(alexaApplicationId);   
        }
    }
}
