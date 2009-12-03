using System.Web.Mvc;
using Inferis.KindjesNet.Core.Mvc.Controllers;
using Inferis.KindjesNet.Core.Security;

namespace Inferis.KindjesNet.Web.Controllers
{
    public class AdminController : KindjesNetController
    {
        [KindjesNetAuthorize]
        public ActionResult Index()
        {
            return View();
        }

    }
}
