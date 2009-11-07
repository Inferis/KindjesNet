using System.ComponentModel.Composition;
using Inferis.KindjesNet.Core.Plugins;

namespace Inferis.KindjesNet.Blog.Migrations
{
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
