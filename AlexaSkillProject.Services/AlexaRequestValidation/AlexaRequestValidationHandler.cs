using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AlexaSkillProject.Domain;
using AlexaSkillProject.Core;
using System.Configuration;

namespace AlexaSkillProject.Services
{
    /// <summary>
    /// This handler is called before the controller model binding occurs
    /// The below code is a combination of :
    /// https://github.com/AreYouFreeBusy/AlexaSkillsKit.NET & pluralsight C# Alexa course code
    /// Note that the build mode environment variable is set to bypass validation checks when in debug mode
    /// </summary>
    public class AlexaRequestValidationHandler : DelegatingHandler
    {

        /// <summary>
        /// Check to verify request headers and certificates per Amazon's validation requirements
        /// Note this method does not apply in debug mode
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequest, CancellationToken cancellationToken)
        {
            string buildMode = ConfigurationSettings.AppSettings["Mode"];

            if (!buildMode.Equals("Debug")) 
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
                    if (!AlexaRequestSignatureVerifierService.VerifyRequestSignature(alexaBytes, signature, chainUrl))
                    {
                        validationResult = validationResult | SpeechletRequestValidationResult.InvalidSignature;
                    }
                }

                if (validationResult != SpeechletRequestValidationResult.OK)
                {
                    throw new Exception("VALIDATION");
                }
            }

            return await base.SendAsync(httpRequest, cancellationToken);
        }

    }
}
