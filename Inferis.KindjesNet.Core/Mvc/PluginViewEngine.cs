using System.Web;
using Inferis.KindjesNet.Core.Mvc.Spark;
using Inferis.KindjesNet.Core.Plugins;
using Spark.Web.Mvc;

namespace Inferis.KindjesNet.Core.Mvc
{
    public class PluginViewEngine : SparkViewFactory
    {
        public PluginViewEngine(PluginContainer container)
        {
            DescriptorBuilder = new PluginDescriptorBuilder(Engine, container.PluginsPath);
        }
    }
}
