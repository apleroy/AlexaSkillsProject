using AlexaSkillProject.Core;
using AlexaSkillProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AlexaSkillProject.Services
{
    public interface IAlexaRequestValidationService
    {
        SpeechletRequestValidationResult ValidateAlexaRequest(AlexaRequestPayload alexaRequest);
        
    }
}
