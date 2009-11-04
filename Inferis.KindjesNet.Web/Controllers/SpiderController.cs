using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;
using Inferis.KindjesNet.Core.Managers;
using Microsoft.Practices.Unity;

namespace Inferis.KindjesNet.Web.Controllers
{
    public class SpiderController : Controller
    {
        [Import, Dependency]
        public ISpiderManager SpiderManager { get; set; }

        public ActionResult Index()
        {
            var baseUri = new Uri(string.Format("{0}://{1}", 
                Request.ServerVariables["HTTPS"] == "off" ? "http" : "https",
                Request.ServerVariables["SERVER_NAME"]));

            var urls = new List<string>();
            foreach (var spider in SpiderManager.Spiders) {
                var path = RouteTable.Routes.GetVirtualPath(null, "Spider", new RouteValueDictionary { { "action", SpiderManager.GetSpiderName(spider) } });
                urls.Add(path.VirtualPath);
                
                var request = WebRequest.Create(new Uri(baseUri, path.VirtualPath));
                request.BeginGetResponse(ar => request.EndGetResponse(ar), null);
            }

            return View(urls);
        }

        protected override void HandleUnknownAction(string actionName)
        {
            var spider = SpiderManager.FindSpider(actionName);
            if (spider == null) {
                base.HandleUnknownAction(actionName);
                return;
            }

            spider.Execute();
            View("Execute").ExecuteResult(ControllerContext);
        }

    }
}
