using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AlexaSkillProject.Core
{
    //public class AlexaRequestValidationHandler //: DelegatingHandler
    //{
    //    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    //    {
    //        if (!request.Headers.Contains("Signature") || !request.Headers.Contains("SignatureCertChainUrl"))
    //        {
    //            throw new Exception(System.Net.HttpStatusCode.BadRequest.ToString());
    //        }

    //        var signatureCertChainUrl = request.Headers.GetValues("SignatureCertChainUrl").First().Replace("/../", "/");

    //        if (string.IsNullOrWhiteSpace(signatureCertChainUrl))
    //        {
    //            throw new Exception(System.Net.HttpStatusCode.BadRequest.ToString());
    //        }

    //        var certUrl = new Uri(signatureCertChainUrl);

    //        if (!(
    //            (certUrl.Port == 443 || certUrl.IsDefaultPort)
    //            && certUrl.Scheme.Equals("s3.amazonaws.com", StringComparison.OrdinalIgnoreCase)
    //            && certUrl.AbsolutePath.StartsWith("/echo.api/")
    //            ))
    //        {
    //            throw new Exception(System.Net.HttpStatusCode.BadRequest.ToString());
    //        }

    //        using (var web = new System.Net.WebClient())
    //        {
    //            var certificate = web.DownloadData(certUrl);
    //            var cert = new X509Certificate2(certificate);

    //            var effectiveDate = DateTime.MinValue;
    //            var expiryDate = DateTime.MinValue;

    //            if (!((DateTime.TryParse(cert.GetExpirationDateString(), out expiryDate)
    //                && expiryDate > DateTime.UtcNow)
    //                && (DateTime.TryParse(cert.GetExpirationDateString(), out effectiveDate)
    //                && effectiveDate < DateTime.UtcNow)
    //                ))
    //            {
    //                throw new Exception(System.Net.HttpStatusCode.BadRequest.ToString());
    //            }

    //            if (!cert.Subject.Contains("CN=echo-api.amazon.com") || !cert.Issuer.Contains("CN=VeriSign Class 3 Secure Server CA"))
    //            {
    //                throw new Exception(System.Net.HttpStatusCode.BadRequest.ToString());
    //            }

    //            var signatureString = request.Headers.GetValues("Signature").First();

    //            byte[] signature = Convert.FromBase64String(signatureString);

    //            using (var sha1 = new System.Security.Cryptography.SHA1Managed())
    //            {
    //                var body = await request.Content.ReadAsStringAsync();

    //                var data = sha1.ComputeHash(Encoding.UTF8.GetBytes(body));
    //                var rsa = (RSACryptoServiceProvider)cert.PublicKey.Key;

    //                if (rsa == null || rsa.VerifyHash(data, CryptoConfig.MapNameToOID("SHA1"), signature))
    //                {
    //                    throw new Exception(System.Net.HttpStatusCode.BadRequest.ToString());
    //                }
    //            }
    //        }

    //        return await base.SendAsync(request, cancellationToken);
    //    }
    //}
}
