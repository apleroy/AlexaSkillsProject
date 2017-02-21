using AlexaSkillProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaSkillProject.Services
{
    public interface ICacheService
    {
        void CacheAlexaRequest(AlexaRequestPayload alexaRequest);
        AlexaRequestPayload RetrieveAlexaRequest(string alexaRequestSessionId);
    }
}
