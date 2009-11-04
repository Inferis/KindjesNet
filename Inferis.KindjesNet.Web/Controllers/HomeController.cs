using System.Web.Mvc;
using Inferis.KindjesNet.Core.Managers;
using Inferis.KindjesNet.Core.Mvc.Controllers;
using Microsoft.Practices.Unity;

namespace Inferis.KindjesNet.Web.Controllers
{
    [HandleError]
    public class HomeController : ControllerWithKids
    {
        [Dependency]
        public IHomepageManager HomepageManager { get; set; }

        public ActionResult Index()
        {
            return View(HomepageManager.GetFeedItems());
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
