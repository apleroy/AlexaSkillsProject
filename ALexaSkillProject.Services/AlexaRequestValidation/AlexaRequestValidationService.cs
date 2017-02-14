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
        
        public SpeechletRequestValidationResult ValidateAlexaRequest(AlexaRequestInputModel alexaRequestInputModel)
        {
            SpeechletRequestValidationResult validationResult = SpeechletRequestValidationResult.OK;
            
            // check timestamp
            if (!VerifyRequestTimestamp(alexaRequestInputModel, DateTime.UtcNow))
            {
                validationResult = SpeechletRequestValidationResult.InvalidTimestamp;
                throw new Exception(validationResult.ToString());
            }

            // check app id
            if (!VerifyApplicationIdHeader(alexaRequestInputModel))
            {
                validationResult = SpeechletRequestValidationResult.InvalidAppId;
                throw new Exception(validationResult.ToString());
            }

            
            return validationResult;
            
        }


        private bool VerifyRequestTimestamp(AlexaRequestInputModel alexaRequestInputModel, DateTime referenceTimeUtc)
        {
            // verify timestamp is within tolerance
            var diff = referenceTimeUtc - alexaRequestInputModel.Request.Timestamp;
            return (Math.Abs((decimal)diff.TotalSeconds) <= AlexaSdk.TIMESTAMP_TOLERANCE_SEC);
        }

        private bool VerifyApplicationIdHeader(AlexaRequestInputModel alexaRequestInputModel)
        {
            string alexaApplicationId = ConfigurationSettings.AppSettings["AlexaApplicationId"];

            return alexaRequestInputModel.Session.Application.ApplicationId.Equals(alexaApplicationId);   
        }
    }
}
