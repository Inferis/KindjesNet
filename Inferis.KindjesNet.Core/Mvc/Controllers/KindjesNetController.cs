using System.ComponentModel.Composition;
using System.Web.Mvc;
using Inferis.KindjesNet.Core.Connections.Facebook;
using Microsoft.Practices.Unity;

namespace Inferis.KindjesNet.Core.Mvc.Controllers
{
    public class KindjesNetController : Controller
    {
        [Dependency]
        public IFacebookConnection Facebook { get; set; }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            ViewData["FacebookApiKey"] = Facebook.ApiKey;
            ViewData["FacebookApiSecret"] = Facebook.ApiSecret;
        }
    }
}
