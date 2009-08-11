using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using System.Web.Routing;
using Inferis.KindjesNet.Core;
using Inferis.KindjesNet.Core.Mvc;

namespace Inferis.KindjesNet.Blog
{
    [Export(typeof(IRouteProvider))]
    public class RouteProvider : DefaultRouteProvider
    {
        public RouteProvider() : base("Blog")
        {
            
        }

        public override void MapAdditionalRoutes(RouteCollection routes)
        {
            routes.MapRoute("OldBlog",
                "blog/post.aspx",
                new { controller = "Blog", action = "OldPost" }
                );
        }
    }
}
