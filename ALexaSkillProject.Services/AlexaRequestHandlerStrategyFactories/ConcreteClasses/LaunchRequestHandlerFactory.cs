using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlexaSkillProject.Domain;

namespace AlexaSkillProject.Services
{
    public class LaunchRequestHandlerFactory : IAlexaRequestHandlerStrategyFactory
    {
        
        public IAlexaRequestHandlerStrategy CreateAlexaRequestHandlerStrategy(AlexaRequest alexaRequest)
        {
            return new LaunchRequestHandlerStrategy(alexaRequest);
        }
    }
}
