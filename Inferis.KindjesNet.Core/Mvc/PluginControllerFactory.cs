using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Inferis.KindjesNet.Core.Plugins;

namespace Inferis.KindjesNet.Core.Mvc
{
    public class PluginControllerFactory : IControllerFactory
    {
        private IControllerFactory fallbackFactory;
        private readonly PluginContainer container;

        public event DecorateControllerHandler DecorateController = delegate {};

        public PluginControllerFactory(PluginContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            fallbackFactory = new DefaultControllerFactory();
            container.CompositionContainer.ComposeParts(this);
            this.container = container;
        }

        public IControllerFactory FallbackFactory
        {
            get { return fallbackFactory; }
            set { fallbackFactory = value ?? new DefaultControllerFactory(); }
        }

        //[ImportMany(AllowRecomposition = true)]
        //public IEnumerable<IController> Controllers { get; set; }

        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            IController controller = null;

            if (controllerName != null) {
                var controllers = container.CompositionContainer.GetExportedValues<IController>();
                controller = controllers.FirstOrDefault(c => string.Equals(c.GetType().Name, controllerName + "Controller", StringComparison.OrdinalIgnoreCase));
            }

            if (controller == null)
                controller = fallbackFactory.CreateController(requestContext, controllerName);

            DecorateController(controller);
            return controller;
        }

        public void ReleaseController(IController controller)
        {
            var disposable = controller as IDisposable;
            if (disposable != null)
                disposable.Dispose();
        }
    }

    public delegate void DecorateControllerHandler(IController controller);
}
