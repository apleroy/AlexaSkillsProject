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
using AlexaSkillProject.Core;

namespace AlexaSkillProject
{
    public static class WebApiConfig
    {
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

            //config.MessageHandlers.Add(new AlexaRequestValidationHandler());
            
            var container = new UnityContainer();
            
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());

            container.RegisterType<IAlexaRequestMapper, AlexaRequestMapper>(new HierarchicalLifetimeManager());
            container.RegisterType<IAlexaRequestPersistenceService, AlexaRequestPersistenceService>(new HierarchicalLifetimeManager());

            container.RegisterType<IAlexaRequestHandlerStrategyFactory, AlexaRequestHandlerStrategyFactory>(new HierarchicalLifetimeManager());

            container.RegisterType<IAlexaRequestService, AlexaRequestService>(new HierarchicalLifetimeManager());

            container.RegisterType<IAlexaRequestSignatureVerifierService, AlexaRequestSignatureVerifierService>(new HierarchicalLifetimeManager());
            container.RegisterType<IAlexaRequestValidationService, AlexaRequestValidationService>(new HierarchicalLifetimeManager());

            config.DependencyResolver = new UnityResolver(container);

        }
    }
}
