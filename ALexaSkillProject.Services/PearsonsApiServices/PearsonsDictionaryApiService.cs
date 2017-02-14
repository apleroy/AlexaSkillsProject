using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AlexaSkillProject.Domain;
using Newtonsoft.Json;
using System.IO;
using AlexaSkillProject.Core;
using System.Configuration;

namespace AlexaSkillProject.Services
{
    public class PearsonsDictionaryApiService : IPearsonsDictionaryApiService
    {

        const string URLREQUEST = "http://api.pearson.com/v2/dictionaries/entries?headword=";
        

        public HttpWebRequest CreateDictionaryApiRequest(string word)
        {
            string requestUrl = string.Format("{0}{1}{2}{3}", 
                URLREQUEST, 
                word, 
                "&apikey=", 
                ConfigurationSettings.AppSettings["PearsonsApiKey"]);
            return (HttpWebRequest)WebRequest.Create(requestUrl);
        }

        public PearsonsDictionaryApiResponse ParseDictionaryApiRequest(HttpWebRequest webRequest)
        {
            try
            {     
                using (HttpWebResponse webresponse = webRequest.GetResponse() as HttpWebResponse)
                {
                    if (webresponse.StatusCode != HttpStatusCode.OK)
                        throw new Exception(String.Format(
                        "Server error (HTTP {0}: {1}).",
                        webresponse.StatusCode,
                        webresponse.StatusDescription)
                        );

                    var reader = new StreamReader(webresponse.GetResponseStream());
                    string webResponseBody = reader.ReadToEnd();
                    reader.Close();

                    PearsonsDictionaryApiResponse response = JsonConvert.DeserializeObject<PearsonsDictionaryApiResponse>(webResponseBody);
                    return response;
                }

            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}
