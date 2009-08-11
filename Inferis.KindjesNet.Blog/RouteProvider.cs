using System.ComponentModel.Composition;
using System.Web.Mvc;
using System.Web.Routing;
using Inferis.KindjesNet.Core;

namespace Inferis.KindjesNet.Blog
{
    [Export(typeof(IRouteProvider))]
    public class RouteProvider : IRouteProvider
    {
        public void MapRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                "Blog",                                              // Route name
                "blog/{action}/{id}",                           // URL with parameters
                new { controller = "Blog", action = "Index", id = "" }  // Parameter defaults
                );

        }
    }
}
