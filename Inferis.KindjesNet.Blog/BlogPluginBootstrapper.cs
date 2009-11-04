using System.ComponentModel.Composition;
using Inferis.KindjesNet.Blog.Managers;
using Inferis.KindjesNet.Core;
using Inferis.KindjesNet.Core.Plugins;
using Microsoft.Practices.Unity;

namespace Inferis.KindjesNet.Blog
{
    [Export(typeof(IPluginBootstrapper))]
    public class BlogPluginBootstrapper : IPluginBootstrapper, IWithContainer
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public void Boot()
        {
            Container.RegisterType<IBlogManager, BlogManager>();
        }

    }
}