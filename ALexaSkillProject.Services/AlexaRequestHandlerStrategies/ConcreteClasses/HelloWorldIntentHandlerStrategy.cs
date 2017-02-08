using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlexaSkillProject.Domain;

namespace AlexaSkillProject.Services
{
    public class HelloWorldIntentHandlerStrategy : IAlexaRequestHandlerStrategy
    {
        public AlexaResponse HandleAlexaRequest(AlexaRequest alexaRequest)
        {
            return new AlexaResponse("Hello Andy");
        }
    }
}