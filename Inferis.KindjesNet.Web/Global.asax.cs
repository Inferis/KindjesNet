using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
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
using Spark.Web.Mvc;

namespace Inferis.KindjesNet.Web
{
    public class MvcApplication : HttpApplication, IWithContainer
    {
        private PluginContainer pluginsContainer;

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

        public MvcApplication()
        {
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
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
                );

        }

        protected void Application_Start()
        {
            //InitializeViewEngines();

            InitializeContainer();
            InitializePlugins();
            InitializeSessionFactory();

            RegisterRoutes(RouteTable.Routes);
            //RouteDebug.RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);
        }

        private void InitializeViewEngines()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new SparkViewFactory());
        }

        private void InitializeSessionFactory()
        {
            var configuration = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2005.ConnectionString(c => c.FromConnectionStringWithKey("KindjesNet")));

            Action<IConventionFinder> defaultConventions = f => {
                f.Add(PrimaryKey.Name.Is(x => x.Property.DeclaringType.Name + "Id"));
                f.Add(ForeignKey.EndsWith("Id"));
            };

            foreach (var configurator in MappingConfigurators) {
                var config = configurator;
                configuration.Mappings(m => config.Configure(m, defaultConventions));
            }

            Container.RegisterInstance(configuration.BuildSessionFactory());
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

        [ImportMany]
        public IEnumerable<IPluginBootstrapper> PluginBootstrappers { get; set; }

        [ImportMany]
        public IEnumerable<IRouteProvider> RouteProviders { get; set; }

        [ImportMany]
        public IEnumerable<IMappingConfigurator> MappingConfigurators { get; set; }
    }
}