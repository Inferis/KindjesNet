using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace Inferis.KindjesNet.Core.Mvc
{
    public class PluginControllerFactory : IControllerFactory
    {
        private readonly DefaultControllerFactory defaultFactory;
        private readonly PluginContainer container;

        public PluginControllerFactory(PluginContainer container)
        {
            defaultFactory = new DefaultControllerFactory();
            container.CompositionContainer.ComposeParts(this);
        }

        [ImportMany(AllowRecomposition = true)]
        public IEnumerable<IController> Controllers { get; set; }

        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            IController controller = null;
            if (controllerName != null) {
                controller = Controllers.FirstOrDefault(c => string.Equals(c.GetType().Name, controllerName + "Controller", StringComparison.OrdinalIgnoreCase));
            }

            if (controller == null)
                controller = defaultFactory.CreateController(requestContext, controllerName);

            return controller;
        }

        public void ReleaseController(IController controller)
        {
            var disposable = controller as IDisposable;
            if (disposable != null)
                disposable.Dispose();
        }
    }
}
