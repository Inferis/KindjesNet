using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Inferis.KindjesNet.Core.Managers;
using Inferis.KindjesNet.Core.Models;
using Microsoft.Practices.Unity;

namespace Inferis.KindjesNet.Core.Mvc.Controllers
{
    public abstract class ControllerWithKids : Controller
    {
        [Dependency]
        public IKidManager KidManager { get; set; }

        protected IList<Kid> Kids
        {
            get { return ViewData["Kids"] as IList<Kid>; }
        }

        protected IList<Kid> HighlightedKids
        {
            get { return ViewData["HighlightedKids"] as IList<Kid>; }
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            ViewData["Kids"] = KidManager.GetAll();
            ViewData["HighlightedKids"] = new List<Kid>();
        }

        protected void HighlightKid(string tag)
        {
            HighlightedKids.Add(Kids.FirstOrDefault(k => k.Tag == tag));
        } 
    }
}
