using System.Linq;
using System.Web.Mvc;
using Inferis.Core.Extensions;
using Inferis.KindjesNet.Core.Mvc.Controllers;
using Inferis.KindjesNet.Core.Utils;

namespace Inferis.KindjesNet.Web.Controllers
{
    public class KidsController : ControllerWithKids
    {
        // Geeft lijstjes terug van alle kinderen
        public ActionResult Index()
        {
            return View(KidManager.GetAll());
        }

        public ActionResult One(string kid)
        {
            var data = KidManager.GetKidByTag(kid);
            if (data != null) {
                HighlightKid(kid);
                return View("One", data);
            }

            // use (object) cast here so that the name is used as a model and not a master name
            return View("Unknown", (object)kid.ToUpperFirst());
        }

        protected override void HandleUnknownAction(string actionName)
        {
            One(actionName).ExecuteResult(ControllerContext);
        }
    }
}
