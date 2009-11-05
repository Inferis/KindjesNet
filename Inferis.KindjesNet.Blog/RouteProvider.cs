using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using System.Web.Routing;
using Inferis.KindjesNet.Core.Mvc;
using Inferis.KindjesNet.Core.Plugins;

namespace Inferis.KindjesNet.Blog
{
    [Export(typeof(IRouteProvider))]
    public class RouteProvider : DefaultRouteProvider
    {
        public RouteProvider()
            : base("Blog")
        {
        }

        public override void MapAdditionalRoutes(RouteCollection routes)
        {
            routes.MapRoute("OldBlog",
                "blog/post.aspx",
                new { controller = "Blog", action = "OldPost" }
                );

            routes.MapRoute("ImportOldBlog",
                "import/oldblog",
                new { controller = "Blog", action = "Import" }
                );
        }
    }
}
