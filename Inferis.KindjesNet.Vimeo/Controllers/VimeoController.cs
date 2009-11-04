using System.ComponentModel.Composition;
using System.Web.Mvc;
using Inferis.KindjesNet.Core.Mvc;
using Inferis.KindjesNet.Vimeo.Managers;
using Microsoft.Practices.Unity;

namespace Inferis.KindjesNet.Vimeo.Controllers
{
    [Export(typeof(IController))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class VimeoController : Controller
    {
        [Dependency]
        public IVimeoManager VideoManager { get; set; }

        public ActionResult Item(int year, int month, int day, string extra)
        {
            var video = VideoManager.GetVideo(year, month, day, extra);
            if (video == null)
                return new NotFoundResult();

            return View("Item", video);
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