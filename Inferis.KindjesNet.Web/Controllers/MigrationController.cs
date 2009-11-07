using System.Web.Mvc;
using Inferis.KindjesNet.Core.Managers;
using Microsoft.Practices.Unity;

namespace Inferis.KindjesNet.Web.Controllers
{
    public class MigrationController : Controller
    {
        [Dependency]
        public IMigrationManager MigrationManager { private get; set; }

        public ActionResult Index()
        {
            return View(MigrationManager.RunAllMigrations());
        }

    }
}
