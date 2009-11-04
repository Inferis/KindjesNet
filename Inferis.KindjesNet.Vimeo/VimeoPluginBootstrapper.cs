using System.ComponentModel.Composition;
using Inferis.KindjesNet.Core;
using Inferis.KindjesNet.Core.Plugins;
using Inferis.KindjesNet.Vimeo.Managers;
using Microsoft.Practices.Unity;

namespace Inferis.KindjesNet.Vimeo
{
    [Export(typeof(IPluginBootstrapper))]
    public class VimeoPluginBootstrapper : IPluginBootstrapper, IWithContainer
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public void Boot()
        {
            Container.RegisterType<IVimeoManager, VimeoManager>();
            Container.RegisterType<IVimeoSettings, VimeoSettings>();
        }

    }
}