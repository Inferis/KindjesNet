using System.ComponentModel.Composition;
using Inferis.KindjesNet.Core.Plugins;

namespace Inferis.KindjesNet.Blog.Migrations
{
    [Export(typeof(IMigrationsProvider))]
    public class VimeoMigrationsProvider : SqlMigrationsProviderBase, IMigrationsProvider
    {
        public const string Context = "Vimeo";

        public VimeoMigrationsProvider()
            : base(Context)
        {
        }
    }
}
