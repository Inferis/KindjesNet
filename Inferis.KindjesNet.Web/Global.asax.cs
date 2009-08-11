using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using Inferis.KindjesNet.Core;
using Inferis.KindjesNet.Core.Mvc;

namespace Inferis.KindjesNet.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private PluginContainer pluginsContainer;

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
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
                );

        }

        protected void Application_Start()
        {
            InitializePlugins();
            RegisterRoutes(RouteTable.Routes);
            //RouteDebug.RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);
        }

        private void InitializePlugins()
        {
            pluginsContainer = new PluginContainer("~/Plugins");

            // set controller factory for plugins
            pluginsContainer.SetControllerFactory(ControllerBuilder.Current);
            pluginsContainer.AddViewEngine(ViewEngines.Engines);

            // compose this app for the route providers
            pluginsContainer.CompositionContainer.ComposeParts(this);
        }

        [ImportMany]
        public IEnumerable<IRouteProvider> RouteProviders { get; set; }
    }
}