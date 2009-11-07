using System.ComponentModel.Composition;
using Inferis.KindjesNet.Core.Managers;
using Inferis.KindjesNet.Core.Plugins;
using Microsoft.Practices.Unity;

namespace Inferis.KindjesNet.Core
{
    [Export(typeof(IPluginBootstrapper))]
    public class CorePluginBootstrapper : IPluginBootstrapper, IWithContainer
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public void Boot()
        {
            Container.RegisterType<IDateFixer, DateFixer>();
            Container.RegisterType<ISlugGenerator, SlugGenerator>();

            Container.RegisterType<IUserManager, UserManager>();
            Container.RegisterType<IKidManager, KidManager>();
            Container.RegisterType<IHomepageManager, HomepageManager>();
            Container.RegisterType<ISpiderManager, SpiderManager>();
            Container.RegisterType<IMigrationManager, MigrationManager>();
        }

    }
}