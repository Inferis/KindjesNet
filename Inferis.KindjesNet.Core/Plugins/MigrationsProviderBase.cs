using System.Reflection;

namespace Inferis.KindjesNet.Core.Plugins
{
    public abstract class MigrationsProviderBase : IMigrationsProvider
    {
        protected MigrationsProviderBase(string context, string transformationProviderName)
        {
            MigrationContext = context;
            TransformationProviderName = transformationProviderName;
        }

        public string MigrationContext { get; protected set; }
        public string TransformationProviderName { get; protected set; }
        public virtual Assembly Assembly
        {
            get
            {
                return GetType().Assembly;
            }
        }
    }
}
