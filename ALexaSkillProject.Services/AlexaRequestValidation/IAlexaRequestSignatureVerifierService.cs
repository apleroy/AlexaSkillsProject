using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaSkillProject.Services
{
    public interface IAlexaRequestSignatureVerifierService
    {
        bool VerifyCertificateUrl(string certChainUrl);

        bool VerifyRequestSignature(byte[] serializedSpeechletRequest, string expectedSignature, string certChainUrl);

        Task<bool> VerifyRequestSignatureAsync(byte[] serializedSpeechletRequest, string expectedSignature, string certChainUrl);

        X509Certificate RetrieveAndVerifyCertificate(string certChainUrl);

        Task<X509Certificate> RetrieveAndVerifyCertificateAsync(string certChainUrl);

        bool CheckRequestSignature(byte[] serializedSpeechletRequest, string expectedSignature, X509Certificate cert);
    }
}
