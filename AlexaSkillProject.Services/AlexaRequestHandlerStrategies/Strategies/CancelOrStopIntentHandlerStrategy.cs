using AlexaSkillProject.Domain;

namespace AlexaSkillProject.Services
{
    public class CancelOrStopIntentHandlerStrategy : IAlexaRequestHandlerStrategy
    {
        public AlexaResponse HandleAlexaRequest(AlexaRequestPayload alexaRequest)
        {
            var response = new AlexaResponse("Thanks for using the Eloquency Skill.  You can also visit Eloquency App dot com for more information.  I hope to see you again soon.");

            response.Response.ShouldEndSession = true;

            return response;
        }
    }
}
