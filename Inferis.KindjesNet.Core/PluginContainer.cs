using System.ComponentModel.Composition.Hosting;

namespace Inferis.KindjesNet.Core
{
    public class PluginContainer
    {
        private readonly DirectoryCatalog compositionCatalog;

        public PluginContainer(string pluginsPath)
        {
            PluginsPath = pluginsPath;
            compositionCatalog = new DirectoryCatalog(pluginsPath);
            CompositionContainer = new CompositionContainer(compositionCatalog);
        }

        public string PluginsPath { get; private set; }

        public CompositionContainer CompositionContainer { get; private set; }
    }
}
