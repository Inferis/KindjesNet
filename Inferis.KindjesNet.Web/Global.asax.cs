using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Helpers;
using Inferis.KindjesNet.Core;
using Inferis.KindjesNet.Core.Data;
using Inferis.KindjesNet.Core.Plugins;
using Microsoft.Practices.Unity;
using NHibernate;
using Spark.Web.Mvc;

namespace Inferis.KindjesNet.Web
{
    public class MvcApplication : HttpApplication, IWithContainer
    {
        private Guid id;
        private PluginContainer pluginsContainer;


        public MvcApplication()
        {
            id = Guid.NewGuid();

            //PreRequestHandlerExecute += (sender, e) => {
            //    try {
            //        // see if we can resolve the session
            //        Container.Resolve<ISessionFactory>();
            //    }
            //    catch (ResolutionFailedException ex) {
            //        // we can't. Initialize it.
            //        InitializeSessionFactory(false);
            //    }
            //};

            PostRequestHandlerExecute += (sender, e) => {
                var uow = Container.Resolve<IRepository>();
                uow.Dispose();
            };
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "GlobalArchive",
                "{year}/{month}/{day}",
                new { controller = "GlobalArchive", action = "Index", month = "00", day = "00" },
                new { year = @"\d{4}", month = @"\d{2}", day = @"\d{2}" }
            );

            foreach (var routeProvider in RouteProviders) {
                routeProvider.MapRoutes(routes);
            }

            routes.MapRoute(
                "Kids",                                              // Route name
                "kids/{action}/{view}",                           // URL with parameters
                new { controller = "Kids", action = "Index" }  // Parameter defaults
                );

            routes.MapRoute(
                "Spider",                                              // Route name
                "spider/{action}",                           // URL with parameters
                new { controller = "Spider", action = "Index" }  // Parameter defaults
                );

            routes.MapRoute(
                "Migrate",                                              // Route name
                "migrate",                           // URL with parameters
                new { controller = "Migration", action = "Index" }  // Parameter defaults
                );

            routes.MapRoute(
                "Users",                                              // Route name
                "users/{action}/{id}",                           // URL with parameters
                new { controller = "Users", action = "Profile", id = "" }  // Parameter defaults
                );

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
                );

        }

        public IUnityContainer Container
        {
            get
            {
                return (IUnityContainer)HttpContext.Current.Application["Container"];
            }
            set
            {
                HttpContext.Current.Application.Lock();
                try {
                    HttpContext.Current.Application["Container"] = value;
                }
                finally {
                    HttpContext.Current.Application.UnLock();
                }
            }
        }

        protected void Application_Start()
        {
            File.AppendAllText(@"c:\mvc\mvc.log", "Application_Start with id=" + id + "\r\n");
            //InitializeViewEngines();

            InitializeContainer();
            InitializePlugins();
            InitializeSessionFactory(true);

            RegisterRoutes(RouteTable.Routes);
            //RouteDebug.RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);
        }

        private void InitializeViewEngines()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new SparkViewFactory());
        }

        private void InitializeSessionFactory(bool appStart)
        {
            var configuration = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2005.ConnectionString(c => c.FromConnectionStringWithKey("KindjesNet")));

            Action<IConventionFinder> defaultConventions = f => {
                f.Add(PrimaryKey.Name.Is(x => x.Property.ReflectedType.Name + "Id"));
                f.Add(ForeignKey.EndsWith("Id"));
                f.Add(new Core.Data.ManyToManyTableNameConvention());
                f.Add(new CascadingSaveUpdateConvention());
            };

            foreach (var configurator in MappingConfigurators) {
                var config = configurator;
                configuration.Mappings(m => config.Configure(m, defaultConventions));
            }

            try {
                Container.RegisterInstance((ISessionFactory)configuration.BuildSessionFactory());
            }
            catch (FluentConfigurationException ex) {
                if (!appStart) throw;
            }
            Container.RegisterType<IUnitOfWorkContext, HttpContextBasedUnitOfWorkContext>();
            Container.RegisterType<IRepository, Repository>();
        }

        /// <summary>
        /// Initializes the unity container.
        /// </summary>
        private void InitializeContainer()
        {
            Container = new UnityContainer().Initialize();
        }

        /// <summary>
        /// Initialize plugin infrastructure.
        /// </summary>
        private void InitializePlugins()
        {
            pluginsContainer = new PluginContainer("~/Plugins", Container);

            // set controller factory for plugins. 
            // apply the unity decorator so that all controllers will be built up 
            // by Unity.
            pluginsContainer.SetControllerFactory(ControllerBuilder.Current)
                .DecorateController += c => Container.BuildUp(c);
            pluginsContainer.AddViewEngine(ViewEngines.Engines);

            // compose this app for the route providers
            pluginsContainer.CompositionContainer.ComposeParts(this);

            Debug.WriteLine(PluginBootstrappers.Count());
            foreach (var bootstrapper in PluginBootstrappers) {
                var bootstrapperWithContainer = bootstrapper as IWithContainer;
                if (bootstrapperWithContainer != null)
                    Container.BuildUp(bootstrapperWithContainer);
                bootstrapper.Boot();
            }
        }

        [ImportMany(AllowRecomposition = true)]
        public IEnumerable<IPluginBootstrapper> PluginBootstrappers
        {
            get { return HttpContext.Current.Application["PluginBootstrappers"] as IEnumerable<IPluginBootstrapper>; }
            set
            {
                try {
                    HttpContext.Current.Application.Lock();
                    HttpContext.Current.Application["PluginBootstrappers"] = value;
                }
                finally {
                    HttpContext.Current.Application.UnLock();
                }
            }
        }

        [ImportMany(AllowRecomposition = true)]
        public IEnumerable<IRouteProvider> RouteProviders
        {
            get { return HttpContext.Current.Application["RouteProviders"] as IEnumerable<IRouteProvider>; }
            set
            {
                try {
                    HttpContext.Current.Application.Lock();
                    HttpContext.Current.Application["RouteProviders"] = value;
                }
                finally {
                    HttpContext.Current.Application.UnLock();
                }
            }
        }

        [ImportMany(AllowRecomposition = true)]
        public IEnumerable<IMappingConfigurator> MappingConfigurators
        {
            get { return HttpContext.Current.Application["MappingConfigurators"] as IEnumerable<IMappingConfigurator>; }
            set
            {
                try {
                    HttpContext.Current.Application.Lock();
                    HttpContext.Current.Application["MappingConfigurators"] = value;
                }
                finally {
                    HttpContext.Current.Application.UnLock();
                }
            }
        }
    }
}