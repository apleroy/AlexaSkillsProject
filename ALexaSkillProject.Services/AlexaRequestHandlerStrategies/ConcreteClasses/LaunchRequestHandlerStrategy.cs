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
        
        public AlexaResponse HandleAlexaRequest(AlexaRequest alexaRequest)
        {

            var response = new AlexaResponse("Welcome to the Vocabulary App.  You can start by asking What is the word of the day?");
            response.Session.MemberId = alexaRequest.AlexaMemberId;
            response.Response.Card.Title = "Vocabulary App";
            response.Response.Card.Content = "Welcome to the Vocabulary App";
            response.Response.Reprompt.OutputSpeech.Text = "Please ask What is the Word of The Day?";
            response.Response.ShouldEndSession = false;

            return response;
        }
    }
}
