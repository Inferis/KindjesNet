using System.Linq;
using System.Web.Mvc;
using Inferis.KindjesNet.Core.Mvc.Controllers;

namespace Inferis.KindjesNet.Web.Controllers
{
    public class KidsController : ControllerWithKids
    {
        // Geeft lijstjes terug van alle kinderen
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult One(string kid)
        {
            HighlightKid(kid);
            return View("One");
        }

        protected override void HandleUnknownAction(string actionName)
        {
            var kids = new string[] { "fien", "klaas", "trijn" };
            if (kids.Contains(actionName.ToLower())) {
                One(actionName).ExecuteResult(ControllerContext);
            }
            else
                base.HandleUnknownAction(actionName);
        }
    }
}
