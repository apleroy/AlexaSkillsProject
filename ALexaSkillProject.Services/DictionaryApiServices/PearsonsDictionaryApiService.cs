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
    public class PearsonsDictionaryApiService : IDictionaryService
    {
        private const string URLREQUEST = "http://api.pearson.com/v2/dictionaries/entries?headword=";
        

        public Dictionary<WordEnum, string> GetWordDictionaryFromString(string word)
        {
            // create dictionary api request
            HttpWebRequest httpWebRequest = CreateDictionaryApiRequest(word);

            // parse the api request into response
            PearsonsDictionaryApiResponse dictionaryApiResponse = ParseDictionaryApiRequest(httpWebRequest);

            // read the response into consumable dictionary - the standard response for this interface
            Dictionary<WordEnum, string> responseDictionary = ConvertPearsonResponseToDictionary(dictionaryApiResponse);

            return responseDictionary;
        }



        private HttpWebRequest CreateDictionaryApiRequest(string word)
        {
            string requestUrl = string.Format("{0}{1}{2}{3}",
                URLREQUEST,
                word,
                "&apikey=",
                ConfigurationSettings.AppSettings["PearsonsApiKey"]);
            return (HttpWebRequest)WebRequest.Create(requestUrl);
        }

        private PearsonsDictionaryApiResponse ParseDictionaryApiRequest(HttpWebRequest webRequest)
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

        private Dictionary<WordEnum, string> ConvertPearsonResponseToDictionary(PearsonsDictionaryApiResponse pearsonsDictionaryApiResponse)
        {
            Dictionary<WordEnum, string> wordDictionary = new Dictionary<WordEnum, string>
            {
                {WordEnum.Word, null },
                {WordEnum.WordPartOfSpeech, null },
                {WordEnum.WordDefinition, null },
                {WordEnum.WordExample, null },
            };

            foreach (PearsonsDictionaryApiResponse.ResultSet resultSet in pearsonsDictionaryApiResponse.Results)
            {
                if (resultSet.Datasets.Contains("laad3"))
                {
                    try { wordDictionary[WordEnum.Word] = resultSet.Headword; } catch { }
                    try { wordDictionary[WordEnum.WordPartOfSpeech] = resultSet.PartOfSpeech; } catch { }
                    try { wordDictionary[WordEnum.WordDefinition] = resultSet.Senses[0].Definition; } catch { }
                    try { wordDictionary[WordEnum.WordExample] = resultSet.Senses[0].Examples[0].Text; } catch { }
                }
            }

            return wordDictionary;

        }

    }
}
