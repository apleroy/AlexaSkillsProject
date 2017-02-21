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

            #region Handler Services

            container.RegisterType<IAlexaRequestHandlerStrategyFactory, AlexaRequestHandlerStrategyFactory>();
            container.RegisterType<IAlexaRequestValidationService, AlexaRequestValidationService>();
            container.RegisterType<IAlexaRequestService, AlexaRequestService>();

            #endregion

            #region Repository Services

            container.RegisterType<IWordService, WordService>();

            #endregion

            DependencyResolver.SetResolver(new UnityResolver(container));
        }
    }
}