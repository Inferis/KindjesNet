using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Inferis.KindjesNet.Core.Mvc;

namespace Inferis.KindjesNet.Core
{
    public class PluginContainer
    {
        private readonly ComposablePartCatalog compositionCatalog;

        public PluginContainer(string pluginsPath)
        {
            PluginsPath = pluginsPath;
            var absolutePath = HttpContext.Current.Server.MapPath(pluginsPath);
            var catalogs = new List<ComposablePartCatalog>();
            foreach (var directory in Directory.GetDirectories(absolutePath)) {
                catalogs.Add(new DirectoryCatalog(directory));
            } 
            compositionCatalog = new AggregateCatalog(catalogs);
            CompositionContainer = new CompositionContainer(compositionCatalog);
        }

        public string PluginsPath { get; private set; }

        public CompositionContainer CompositionContainer { get; private set; }

        public void SetControllerFactory(ControllerBuilder builder)
        {
            builder.SetControllerFactory(new PluginControllerFactory(this));
        }

        public void AddViewEngine(ViewEngineCollection engines)
        {
            engines.Add(new PluginViewEngine(this));
        }
    }
}
