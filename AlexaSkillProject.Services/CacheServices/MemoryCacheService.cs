using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlexaSkillProject.Domain;
using System.Runtime.Caching;

namespace AlexaSkillProject.Services
{
    public class MemoryCacheService : ICacheService
    {
        public void CacheAlexaRequest(AlexaRequestPayload alexaRequest)
        {
            // use set to add sessionid/request to memory cache
            // http://stackoverflow.com/questions/8868486/whats-the-difference-between-memorycache-add-and-memorycache-set

            MemoryCache.Default.Set(alexaRequest.Session.SessionId,
                alexaRequest,
                new CacheItemPolicy()
                );
        }

        public AlexaRequestPayload RetrieveAlexaRequest(string alexaRequestSessionId)
        {
            AlexaRequestPayload lastRequestPayload = null;
            try
            {
                lastRequestPayload = (AlexaRequestPayload)MemoryCache.Default.Get(alexaRequestSessionId);
                
            }
            catch (Exception exception)
            {
                return null; 
            }

            return lastRequestPayload;
        }
    }
}
