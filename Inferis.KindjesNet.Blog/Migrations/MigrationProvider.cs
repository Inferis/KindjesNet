using System.ComponentModel.Composition;
using Inferis.KindjesNet.Core.Plugins;

namespace Inferis.KindjesNet.Blog.Migrations
{
    [PartCreationPolicy(CreationPolicy.Shared)]
    [Export(typeof(IMigrationsProvider))]
    public class BlogMigrationsProvider : SqlMigrationsProviderBase, IMigrationsProvider
    {
        public const string Context = "Blog";

        public BlogMigrationsProvider()
            : base(Context)
        {
        }
    }
}
