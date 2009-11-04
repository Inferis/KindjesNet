using System;
using System.Web.Mvc;
using System.Web.Routing;
using Inferis.KindjesNet.Core.Plugins;

namespace Inferis.KindjesNet.Core.Mvc
{
    public class DefaultRouteProvider : IRouteProvider
    {
        private string baseName;

        protected DefaultRouteProvider(string baseName)
        {
            this.baseName = baseName;
        }

        public void MapRoutes(RouteCollection routes)
        {
            //routes.MapRoute(baseName + "Index",
            //    "blog/post.aspx",
            //    new { controller = "Blog", action = "OldPost" }
            //    );

            MapAdditionalRoutes(routes);
        }

        public virtual void MapAdditionalRoutes(RouteCollection routes)
        {
        }

    }
}