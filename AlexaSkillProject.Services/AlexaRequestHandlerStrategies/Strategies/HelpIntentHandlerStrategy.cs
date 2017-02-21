using AlexaSkillProject.Domain;

namespace AlexaSkillProject.Services
{
    public class HelpIntentHandlerStrategy : IAlexaRequestHandlerStrategy
    {
        public AlexaResponse HandleAlexaRequest(AlexaRequestPayload alexaRequest)
        {
            var response = new AlexaResponse("You can ask What is the Word of the Day.  You can also visit Eloquency App dot com for more information.");

            response.Response.ShouldEndSession = false;

            return response;
        }
    }
}
