using System;
using System.Collections;
using System.Web;
using Microsoft.Practices.Unity;

namespace Inferis.KindjesNet.Core.Data
{
    public class HttpContextBasedUnitOfWorkContext : IUnitOfWorkContext
    {
        private readonly IUnityContainer container;

        public HttpContextBasedUnitOfWorkContext(IUnityContainer container)
        {
            this.container = container;
        }

        public IDictionary Items
        {
            get { return HttpContext.Current.Items; }
        }

        public T Resolve<T>()
        {
            return container.Resolve<T>();
        }

        public T Resolve<T>(string name)
        {
            return container.Resolve<T>(name);
        }
    }
}
