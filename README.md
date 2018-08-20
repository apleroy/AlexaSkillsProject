# Project README

This project is an Alexa Skill application called "Grammar Tool".

## Tech Stack

* It is built as a layered .NET/C# application, with tiers for web app & web api, service layer, repo layer, and domain specifics.

* The project stack is .NET 4.5, Entity Framework 6, and Unity for Dependnecy Injection and Inversion of Control.  The project is hosted on Microsoft Azure.

* The AlexaSkillProject.WebApp project hosts the web app and web api.

* The initial entry point for the Alexa Skill is in the AlexaController at [HttpPost, Route("api/v1/alexa/grammartool")]

* The Alexa skill uses the service layer (AlexaSkillProject.Services) to validate the request, map and persist the request (Azure SQL Server), and deliver an appropriate response.

* The Pearsons Dictionary API is used to retireve a Part of Speech, Definition, and Example for any given word (and the result is stored locally). (http://developer.pearson.com/apis/dictionaries)

* Unity Inversion of Control Container is used for Dependency Injection (AlexaSkillProject.WebApp > App_Start > WebApiConfig)

## About the Alexa Skill

* Invoke the Skill by saying "Alexa, Open Grammar Tool".

* Ask for the Word of the Day: "What is the Word of The Day?"

* Alexa will give a word of the day, its part of speech, definition, and an example.  Alexa will then prompt the user to repeat the word.

* Alexa will continue with additional words with "Get Another Word".

* One shot request: "Alexa, Ask Grammar Tool what is the Word of The Day?""

## About the Web App 

* www.grammartoolapp.com (in progress)
