using AlexaSkillProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaSkillProject.Services
{
    public abstract class AbstractAlexaErrorResponse
    {
        public virtual AlexaResponse GenerateCustomError()
        {
            AlexaResponse alexaResponse = new AlexaResponse("Sorry there was an error");
            return alexaResponse;
        }
    }
}
