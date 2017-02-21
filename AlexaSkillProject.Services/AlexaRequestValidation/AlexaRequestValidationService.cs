using AlexaSkillProject.Domain;
using System;
using System.Configuration;

namespace AlexaSkillProject.Services
{

    /// <summary>
    /// This class verifies that each call to the alexa app has valida data for the timestamp and alexa app id
    /// This class ensures no posts carry on for > 150 seconds and that requests are for this application specifically
    /// </summary>
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
