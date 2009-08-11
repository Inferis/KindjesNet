using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Compilation;
using System.Web.Mvc;

namespace Inferis.KindjesNet.Core.Mvc
{
    public class PluginViewEngine : System.Web.Mvc.WebFormViewEngine
    {
        public PluginViewEngine()
        {
            
        }

        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            var prefix = "~/$" + controllerContext.Controller.GetType().Assembly.GetName().Name + "$/";
            return base.FileExists(controllerContext, virtualPath.Replace("~/", prefix));
        }

        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            return base.FindPartialView(controllerContext, partialViewName, useCache);
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            return base.CreateView(controllerContext, viewPath, masterPath);
        }
    }
}
