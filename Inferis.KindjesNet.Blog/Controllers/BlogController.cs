using System.ComponentModel.Composition;
using System.Web.Mvc;

namespace Inferis.KindjesNet.Blog.Controllers
{
    [Export(typeof(IController))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BlogController : Controller
    {
        public ActionResult OldPost()
        {
            ViewData["Origin"] = Request.QueryString;
            return View("Post");
        }

        public ActionResult Item(int year, int month, int day, string extra)
        {
            ViewData["Origin"] = extra;
            return View("Post");
        }

        public ActionResult ArchiveDay(int year, int month, int day)
        {
            return View("PostArchive");
        }

        public ActionResult ArchiveMonth(int year, int month)
        {
            return View("PostArchive");
        }

        public ActionResult ArchiveYear(int year)
        {
            return View("PostArchive");
        }
    }
}