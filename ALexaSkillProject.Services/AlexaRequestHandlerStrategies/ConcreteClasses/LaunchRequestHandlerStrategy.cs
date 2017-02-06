using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlexaSkillProject.Domain;

namespace AlexaSkillProject.Services
{
    public class LaunchRequestHandlerStrategy : IAlexaRequestHandlerStrategy
    {
        private AlexaRequest alexaRequest;

        public LaunchRequestHandlerStrategy(AlexaRequest alexaRequest)
        {
            this.alexaRequest = alexaRequest;
        }

        public AlexaResponse HandleAlexaRequest()
        {
            var response = new AlexaResponse("Welcome to Plural sight. What would you like to hear, the Top Courses or New Courses?");
            response.Session.MemberId = alexaRequest.AlexaMemberId;
            response.Response.Card.Title = "Pluralsight";
            response.Response.Card.Content = "Hello\ncruel world!";
            response.Response.Reprompt.OutputSpeech.Text = "Please pick one, Top Courses or New Courses?";
            response.Response.ShouldEndSession = false;

            return response;
        }
    }
}
