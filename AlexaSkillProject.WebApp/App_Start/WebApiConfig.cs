using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Microsoft.Practices.Unity;
using AlexaSkillProject.Services;
using AlexaSkillProject.Repository;
using System.Configuration;

namespace AlexaSkillProject
{
    public static class WebApiConfig
    {
        /// <summary>
        /// WebAPI specifics and setup - includes Dependency Injection and custom Validation Handler
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            log4net.Config.XmlConfigurator.Configure();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // https://msdn.microsoft.com/en-us/library/dn178469(v=pandp.30).aspx

            var container = new UnityContainer();

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


            config.DependencyResolver = new UnityResolver(container);


            // does certificate and header level validation for all api requests
            // make route specific if needed
            config.MessageHandlers.Add(new AlexaRequestValidationHandler());


        }
    }
}
