using System.Web;
using System.Web.Mvc;
using Inferis.KindjesNet.Core.Plugins;

namespace Inferis.KindjesNet.Core.Mvc
{
    public class PluginViewEngine : System.Web.Mvc.WebFormViewEngine
    {
        private readonly PluginContainer container;
        private readonly string pluginsPath;

        public PluginViewEngine(PluginContainer container)
        {
            this.container = container;
            this.pluginsPath = VirtualPathUtility.ToAppRelative(container.PluginsPath) + "/";
        }

        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            return base.FileExists(controllerContext, FixPath(virtualPath, controllerContext));
        }

        private string FixPath(string path, ControllerContext controllerContext)
        {
            return path.Replace(
                "~/",
                pluginsPath + controllerContext.Controller.GetType().Assembly.GetName().Name + "/");
        }

        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            return base.FindPartialView(controllerContext, partialViewName, useCache);
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            return base.CreateView(controllerContext, FixPath(viewPath, controllerContext), masterPath);
        }
    }
}
