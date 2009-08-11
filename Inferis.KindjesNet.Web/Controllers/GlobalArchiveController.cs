using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace Inferis.KindjesNet.Web.Controllers
{
    public class GlobalArchiveController : Controller
    {
        public ActionResult Index(int year, int month, int day)
        {
            if (year > 0) {
                if (month > 0) {
                    if (day > 0) {
                        return View("Day");
                    }
                    return View("Month");
                }
                return View("Year");
            }
            return Redirect("~/");
        }
    }
}
