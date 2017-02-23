using AlexaSkillProject.Repository;
using AlexaSkillProject.Services;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlexaSkillProject.WebApp
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            // https://msdn.microsoft.com/en-us/library/dn178469(v=pandp.30).aspx


            #region UnitOfWork

            container.RegisterType<IUnitOfWork, UnitOfWork>();

            #endregion

            #region Caching

            container.RegisterType<ICacheService, MemoryCacheService>();

            #endregion

            #region RequestMapping and Persistence

            container.RegisterType<IAlexaRequestMapper, AlexaRequestMapper>();
            container.RegisterType<IAlexaRequestPersistenceService, AlexaRequestPersistenceService>();

            #endregion

            #region Dictionary Mapping

            container.RegisterType<IDictionaryService, LocalDictionaryService>();

            #endregion

            #region Request and Validation

            container.RegisterType<IAlexaRequestValidationService, AlexaRequestValidationService>();
            container.RegisterType<IAlexaRequestService, AlexaRequestService>();

            #endregion

            #region Handler Strategies

            container.RegisterType<IAlexaRequestHandlerStrategy, AnotherWordIntentHandlerStrategy>("AnotherWordIntentHandlerStrategy");
            container.RegisterType<IAlexaRequestHandlerStrategy, CancelOrStopIntentHandlerStrategy>("CancelOrStopIntentHandlerStrategy");
            container.RegisterType<IAlexaRequestHandlerStrategy, HelloWorldIntentHandlerStrategy>("HelloWorldIntentHandlerStrategy");
            container.RegisterType<IAlexaRequestHandlerStrategy, HelpIntentHandlerStrategy>("HelpIntentHandlerStrategy");
            container.RegisterType<IAlexaRequestHandlerStrategy, LaunchRequestHandlerStrategy>("LaunchRequestHandlerStrategy");
            container.RegisterType<IAlexaRequestHandlerStrategy, SayWordIntentHandlerStrategy>("SayWordIntentHandlerStrategy");
            container.RegisterType<IAlexaRequestHandlerStrategy, SessionEndedRequestHandlerStrategy>("SessionEndedRequestHandlerStrategy");
            container.RegisterType<IAlexaRequestHandlerStrategy, WordOfTheDayIntentHandlerStrategy>("WordOfTheDayIntentHandlerStrategy");

            container.RegisterType<IEnumerable<IAlexaRequestHandlerStrategy>, IAlexaRequestHandlerStrategy[]>();
            container.RegisterType<IAlexaRequestHandlerStrategyFactory, AlexaRequestHandlerStrategyFactory>();

            #endregion

            #region Repository Services

            container.RegisterType<IWordService, WordService>();

            #endregion


            DependencyResolver.SetResolver(new UnityResolver(container));
        }
    }
}