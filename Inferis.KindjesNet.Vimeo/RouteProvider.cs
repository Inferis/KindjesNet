using System.ComponentModel.Composition;
using System.Web.Routing;
using Inferis.KindjesNet.Core.Mvc;
using Inferis.KindjesNet.Core.Plugins;

namespace Inferis.KindjesNet.Vimeo
{
    [Export(typeof(IRouteProvider))]
    public class RouteProvider : DefaultRouteProvider
    {
        public RouteProvider() : base("Vimeo")
        {
            
        }

        public override void MapAdditionalRoutes(RouteCollection routes)
        {
        }
    }
}