using System.ComponentModel.Composition;
using Inferis.KindjesNet.Core.Plugins;

namespace Inferis.KindjesNet.Core.Migrations
{
    [PartCreationPolicy(CreationPolicy.Shared)]
    [Export(typeof(IMigrationsProvider))]
    public class CoreMigrationsProvider : SqlMigrationsProviderBase, IMigrationsProvider
    {
        public const string Context = null;

        public CoreMigrationsProvider()
            : base(Context)
        {
        }
    }
}