using System.Collections.Generic;
using System.Linq;
using System.Web;
using Spark;
using Spark.Web.Mvc;

namespace Inferis.KindjesNet.Core.Mvc.Spark
{
    public class PluginDescriptorBuilder : DefaultDescriptorBuilder
    {
        private const string PluginRootKey = "PluginRoot";
        private string pluginsPath;

        public PluginDescriptorBuilder(ISparkViewEngine engine, string pluginsPath)
            : base(engine)
        {
            this.pluginsPath = VirtualPathUtility.ToAppRelative(pluginsPath) + "/";
        }

        protected override IEnumerable<string> PotentialViewLocations(string controllerName, string viewName, IDictionary<string, object> extra)
        {
            var locations = base.PotentialViewLocations(controllerName, viewName, extra);

            object root;
            if (extra.TryGetValue(PluginRootKey, out root) && root is string) {
                locations = locations
                    .Select(location => pluginsPath + root + @"/Views/" + location.Replace(@"\", "/"))
                    .Union(locations)
                    .ToList();
            }

            return locations;
        }

        public override IDictionary<string, object> GetExtraParameters(System.Web.Mvc.ControllerContext controllerContext)
        {
            var extras = base.GetExtraParameters(controllerContext);

            var pluginRoot = controllerContext.Controller.GetType().Namespace;
            var index = pluginRoot.IndexOf(".Controllers");
            extras["PluginRoot"] = index >= 0 ? pluginRoot.Substring(0, index) : pluginRoot;

            return extras;
        }
    }
}
