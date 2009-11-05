using System.Web.Mvc;
using System.Web.Routing;
using Inferis.KindjesNet.Core.Plugins;

namespace Inferis.KindjesNet.Core.Mvc
{
    public abstract class DefaultRouteProvider : IRouteProvider
    {
        private readonly string controllerName;
        private readonly string slug;

        protected DefaultRouteProvider(string controllerName)
        {
            this.controllerName = controllerName;
            slug = controllerName.ToLowerInvariant();
        }

        public void MapRoutes(RouteCollection routes)
        {
            routes.MapRoute(controllerName + "ArchiveYear",
                "{year}/" + slug,
                new { controller = controllerName, action = "ArchiveYear" },
                new { year = @"\d{4}" }
            );

            routes.MapRoute(controllerName + "ArchiveMonth",
                "{year}/{month}/" + slug,
                new { controller = controllerName, action = "ArchiveMonth" },
                new { year = @"\d{4}", month = @"\d{2}" }
            );

            routes.MapRoute(controllerName + "ArchiveDay",
                "{year}/{month}/{day}/" + slug,
                new { controller = controllerName, action = "ArchiveDay" },
                new { year = @"\d{4}", month = @"\d{2}", day = @"\d{2}", }
            );

            routes.MapRoute(controllerName + "Item",
                "{year}/{month}/{day}/" + slug + "/{extra}",
                new { controller = controllerName, action = "Item", extra = "" },
                new { year = @"\d{4}", month = @"\d{2}", day = @"\d{2}", }
            );

            routes.MapRoute(controllerName + "Search",
                "search/" + slug,
                new { controller = controllerName, action = "Search", q = "" }
            );

            MapAdditionalRoutes(routes);
        }

        public abstract void MapAdditionalRoutes(RouteCollection routes);
    }
}