using AlexaSkillProject.Core;
using AlexaSkillProject.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AlexaSkillProject.Services
{
    [Flags]
    public enum SpeechletRequestValidationResult
    {
        OK = 0,
        NoSignatureHeader = 1,
        NoCertHeader = 2,
        InvalidSignature = 4,
        InvalidTimestamp = 8,
        InvalidJson = 16
    }

    public class AlexaRequestValidationService : IAlexaRequestValidationService
    {
        private readonly IAlexaRequestSignatureVerifierService _alexaRequestSignatureVerifierService;

        public AlexaRequestValidationService(
            IAlexaRequestSignatureVerifierService alexaRequestSignatureVerifierService)
        {
            _alexaRequestSignatureVerifierService = alexaRequestSignatureVerifierService;
        }

        public AlexaRequestInputModel ValidateAlexaHttpRequest(HttpRequestMessage httpRequest)
        {
            AlexaRequestInputModel alexaRequestInputModel = null;
            
            // validate header per amazons sdk and method
            SpeechletRequestValidationResult validationResult = ValidateAlexaRequestHeader(httpRequest);

            // serialize into alexarequest per json specs and contract
            if (validationResult == SpeechletRequestValidationResult.OK)
            {
                alexaRequestInputModel = DeserializeHttpRequest(httpRequest);

                // check timestamp
                DateTime now = DateTime.UtcNow; // reference time for this request

                if (!VerifyRequestTimestamp(alexaRequestInputModel, now))
                {
                    return null;
                }
            }

            // check appid


            // return validated request
            return alexaRequestInputModel;
        }

        private SpeechletRequestValidationResult ValidateAlexaRequestHeader(HttpRequestMessage httpRequest)
        {
            SpeechletRequestValidationResult validationResult = SpeechletRequestValidationResult.OK;
            
            string chainUrl = null;
            if (!httpRequest.Headers.Contains(AlexaSdk.SIGNATURE_CERT_URL_REQUEST_HEADER) ||
                String.IsNullOrEmpty(chainUrl = httpRequest.Headers.GetValues(AlexaSdk.SIGNATURE_CERT_URL_REQUEST_HEADER).First()))
            {
                validationResult = validationResult | SpeechletRequestValidationResult.NoCertHeader;
            }

            string signature = null;
            if (!httpRequest.Headers.Contains(AlexaSdk.SIGNATURE_REQUEST_HEADER) ||
                String.IsNullOrEmpty(signature = httpRequest.Headers.GetValues(AlexaSdk.SIGNATURE_REQUEST_HEADER).First()))
            {
                validationResult = validationResult | SpeechletRequestValidationResult.NoSignatureHeader;
            }

            var alexaBytes = AsyncHelpers.RunSync<byte[]>(() => httpRequest.Content.ReadAsByteArrayAsync());
            
            // attempt to verify signature only if we were able to locate certificate and signature headers
            if (validationResult == SpeechletRequestValidationResult.OK)
            {
                if (!_alexaRequestSignatureVerifierService.VerifyRequestSignature(alexaBytes, signature, chainUrl))
                {
                    validationResult = validationResult | SpeechletRequestValidationResult.InvalidSignature;
                }
            }

            return validationResult;

        }

        private AlexaRequestInputModel DeserializeHttpRequest(HttpRequestMessage requestMessage)
        {
            AlexaRequestInputModel alexaRequestInputModel = null;

            try
            {
                alexaRequestInputModel = JsonConvert.DeserializeObject<AlexaRequestInputModel>(requestMessage.Content.ReadAsStringAsync().Result);

            }
            catch (Exception e)
            {
                throw;
            }

            return alexaRequestInputModel;
            
        }

        private bool VerifyRequestTimestamp(AlexaRequestInputModel alexaRequestInputModel, DateTime referenceTimeUtc)
        {
            // verify timestamp is within tolerance
            var diff = referenceTimeUtc - alexaRequestInputModel.Request.Timestamp;
            return (Math.Abs((decimal)diff.TotalSeconds) <= AlexaSdk.TIMESTAMP_TOLERANCE_SEC);
        }

        private bool VerifyApplicationIdHeader(AlexaRequestInputModel alexaRequestInputModel)
        {
            //if id == this id
            return true;
        }
    }
}
