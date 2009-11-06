using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Xml.Linq;
using Inferis.KindjesNet.Core.Mvc;
using MefContrib.Integration.Unity;
using Microsoft.Practices.Unity;

namespace Inferis.KindjesNet.Core.Plugins
{
    public class PluginContainer
    {
        private readonly ComposablePartCatalog compositionCatalog;

        public PluginContainer(string pluginsPath, IUnityContainer container)
        {
            PluginsPath = pluginsPath;

            var config = WebConfigurationManager.OpenWebConfiguration("~");
            var compilation = (CompilationSection)config.GetSection("system.web/compilation");

            var absolutePath = HttpContext.Current.Server.MapPath(pluginsPath);
            var catalogs = new List<ComposablePartCatalog>();
            var mustSave = false;

            foreach (var directory in Directory.GetDirectories(absolutePath)) {
                var actualDirectory = Path.Combine(directory, "bin");
                if (!Directory.Exists(actualDirectory))
                    actualDirectory = directory;
                var catalog = new DirectoryCatalog(actualDirectory);
                catalogs.Add(catalog);

                var name = AssemblyName.GetAssemblyName(Path.Combine(actualDirectory, Path.GetFileName(directory) + ".dll")).FullName;
                if (!compilation.Assemblies.OfType<AssemblyInfo>().Any(a => a.Assembly == name)) {
                    //compilation.Assemblies.Add(new AssemblyInfo(name));
                    //mustSave = true;
                }

                var baseDir = actualDirectory.Replace(AppDomain.CurrentDomain.BaseDirectory, "");
                var runtimeSection = config.GetSection("runtime");
                var xml = runtimeSection.SectionInformation.GetRawXml();
                var xdoc = XDocument.Parse(xml);
                var probing = xdoc.Descendants(XName.Get("probing", "urn:schemas-microsoft-com:asm.v1")).FirstOrDefault();
                var path = probing.Attributes().FirstOrDefault(a => a.Name == "privatePath");
                if (path.Value == null || !path.Value.Contains(baseDir)) {
                    path.Value = path.Value +
                        (string.IsNullOrEmpty(path.Value) ? "" : ";") +
                        baseDir;
                    xml = xdoc.ToString();
                    //runtimeSection.SectionInformation.SetRawXml(xml);
                    //mustSave = true;
                }
            }
            if (mustSave)
                config.Save();

            compositionCatalog = new AggregateCatalog(catalogs);
            CompositionContainer = new CompositionContainer(compositionCatalog);
            container.RegisterCatalog(compositionCatalog);

            AppDomain.CurrentDomain.AssemblyResolve += (s, e) => {
                   var name = new AssemblyName(e.Name);
                var rootPath = Path.Combine(absolutePath, name.Name);
                var file = Path.Combine(rootPath, name.Name + ".dll");

                Assembly result = null;
                try {
                    result = Assembly.LoadFrom(file);
                }
                catch (Exception ex) {
                    Debug.WriteLine(ex.ToString());

                    rootPath = Path.Combine(rootPath, "bin");
                    file = Path.Combine(rootPath, name.Name + ".dll");
                    try {
                        result = Assembly.LoadFrom(file);
                    }
                    catch (Exception ex2) {
                        Debug.WriteLine(ex2.ToString());
                    }
                }
                
                return result;
            };

        }

        public string PluginsPath { get; private set; }

        public CompositionContainer CompositionContainer { get; private set; }

        public PluginControllerFactory SetControllerFactory(ControllerBuilder builder)
        {
            var factory = new PluginControllerFactory(this);
            builder.SetControllerFactory(factory);
            return factory;
        }

        public PluginControllerFactory SetControllerFactory(ControllerBuilder builder, IControllerFactory fallbackFactory)
        {
            var factory = new PluginControllerFactory(this) { FallbackFactory = fallbackFactory };
            builder.SetControllerFactory(factory);
            return factory;
        }

        public void AddViewEngine(ViewEngineCollection engines)
        {
            engines.Insert(0, new PluginViewEngine(this));
        }
    }
}