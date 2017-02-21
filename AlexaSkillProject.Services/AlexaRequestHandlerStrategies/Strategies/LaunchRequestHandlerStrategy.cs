using AlexaSkillProject.Domain;
using System.Configuration;

namespace AlexaSkillProject.Services
{
    public class LaunchRequestHandlerStrategy : IAlexaRequestHandlerStrategy
    {
        
        public AlexaResponse HandleAlexaRequest(AlexaRequestPayload alexaRequest)
        {
 
            var response = new AlexaResponse("Welcome to Eloquency.  You can start by asking What is the word of the day?");
            
            response.Response.Card.Title = ConfigurationSettings.AppSettings["AppTitle"];
            response.Response.Card.Content = "Welcome to Eloquency";
            response.Response.Reprompt.OutputSpeech.Text = "Please ask What is the Word of The Day?";
            response.Response.ShouldEndSession = false;

            return response;
        }
    }
}
