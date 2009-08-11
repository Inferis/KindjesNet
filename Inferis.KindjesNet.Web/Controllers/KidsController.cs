using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace Inferis.KindjesNet.Web.Controllers
{
    public class KidsController : Controller
    {
        // Geeft lijstjes terug van alle kinderen
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult One(string kid)
        {
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
