using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AlexaSkillProject.Domain;

namespace AlexaSkillProject.Services
{
    public interface IPearsonsDictionaryApiService
    {
        HttpWebRequest CreateDictionaryApiRequest(string word);
        PearsonsDictionaryApiResponse ParseDictionaryApiRequest(HttpWebRequest webRequest);
        
    }
}
