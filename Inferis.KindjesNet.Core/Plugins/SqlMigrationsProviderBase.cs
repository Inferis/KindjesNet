namespace Inferis.KindjesNet.Core.Plugins
{
    public abstract class SqlMigrationsProviderBase : MigrationsProviderBase
    {
        protected SqlMigrationsProviderBase(string context)
            : base(context, "SqlServer2005")
        {
        }
    }
}
