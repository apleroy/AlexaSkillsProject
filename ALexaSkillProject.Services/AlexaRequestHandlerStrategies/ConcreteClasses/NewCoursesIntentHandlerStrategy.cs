using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlexaSkillProject.Domain;

namespace AlexaSkillProject.Services
{
    public class NewCoursesIntentHandlerStrategy : IAlexaRequestHandlerStrategy
    {
        

        public AlexaResponse HandleAlexaRequest(AlexaRequestPayload alexaRequest)
        {
            var output = new StringBuilder("Here are the latest courses. ");

            

            return new AlexaResponse(output.ToString());
            
        }
    }
}
