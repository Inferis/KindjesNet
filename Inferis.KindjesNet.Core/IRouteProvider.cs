using System.Web.Routing;

namespace Inferis.KindjesNet.Core
{
    public interface IRouteProvider
    {
        void MapRoutes(RouteCollection routes);
    }
}
