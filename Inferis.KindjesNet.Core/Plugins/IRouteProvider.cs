using System.Web.Routing;

namespace Inferis.KindjesNet.Core.Plugins
{
    public interface IRouteProvider
    {
        void MapRoutes(RouteCollection routes);
    }
}