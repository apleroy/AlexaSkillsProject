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


            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());

            container.RegisterType<IAlexaRequestMapper, AlexaRequestMapper>(new HierarchicalLifetimeManager());
            container.RegisterType<IAlexaRequestPersistenceService, AlexaRequestPersistenceService>(new HierarchicalLifetimeManager());

            container.RegisterType<IDictionaryService, PearsonsDictionaryApiService>();

            container.RegisterType<IAlexaRequestHandlerStrategyFactory, AlexaRequestHandlerStrategyFactory>();

            container.RegisterType<IAlexaRequestValidationService, AlexaRequestValidationService>(new HierarchicalLifetimeManager());

            container.RegisterType<IAlexaRequestService, AlexaRequestService>();

            container.RegisterType<IWordService, WordService>();


            DependencyResolver.SetResolver(new UnityResolver(container));
        }
    }
}