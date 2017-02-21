using AlexaSkillProject.Domain;

namespace AlexaSkillProject.Services
{
    public class AlexaWordErrorResponse : AbstractAlexaErrorResponse
    {
        public override AlexaResponse GenerateCustomError()
        {
            AlexaResponse alexaResponse = new AlexaResponse("I'm Sorry, I couldn't understand your request.  Please ask What is the Word of the Day?");
            alexaResponse.Response.Reprompt.OutputSpeech.Text = "Please ask What is the Word of the Day?";
            alexaResponse.Response.ShouldEndSession = false;

            return alexaResponse;
        }

    }
}
