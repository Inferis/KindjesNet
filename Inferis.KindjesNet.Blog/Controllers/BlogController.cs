using System.ComponentModel.Composition;
using System.Web.Mvc;

namespace Inferis.KindjesNet.Blog.Controllers
{
    [Export(typeof(IController))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BlogController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Post(string info, string s)
        {
            ViewData["Origin"] = info + " " + s;
            return View("Post");
        }

        protected override void HandleUnknownAction(string actionName)
        {
            Post(actionName, ControllerContext.HttpContext.Request.QueryString.ToString()).ExecuteResult(ControllerContext);
        }
    }
}