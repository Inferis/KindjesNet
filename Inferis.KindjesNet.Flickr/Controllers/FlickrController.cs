using System.ComponentModel.Composition;
using System.Web.Mvc;

namespace Inferis.KindjesNet.Flickr.Controllers
{
    [Export(typeof(IController))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FlickrController : Controller
    {
        public ActionResult Item(int year, int month, int day, string extra)
        {
            ViewData["Origin"] = extra;
            return View("All");
        }

        public ActionResult ArchiveDay(int year, int month, int day)
        {
            return View("Archive");
        }

        public ActionResult ArchiveMonth(int year, int month)
        {
            return View("Archive");
        }

        public ActionResult ArchiveYear(int year)
        {
            return View("Archive");
        }
    }
}