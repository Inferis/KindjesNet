using System.Reflection;

namespace Inferis.KindjesNet.Core.Plugins
{
    public interface IMigrationsProvider
    {
        string MigrationContext { get;  }
        string TransformationProviderName { get; }
        Assembly Assembly { get; }
    }
}
