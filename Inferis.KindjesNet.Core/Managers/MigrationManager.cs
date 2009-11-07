using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Text;
using Inferis.KindjesNet.Core.Plugins;
using Migrator;
using Migrator.Framework.Loggers;

namespace Inferis.KindjesNet.Core.Managers
{
    public class MigrationManager : IMigrationManager
    {
        [ImportMany]
        public IEnumerable<IMigrationsProvider> Providers { get; set; }

        public IEnumerable<string> RunAllMigrations()
        {
            var writer = new ToBufferLogWriter();
            var logger = new Logger(true, writer);

            string connectionString = null;
            foreach (var name in new[] { "KindjesNetMigrations", "KindjesNet" }) {
                if (ConfigurationManager.ConnectionStrings[name] != null) {
                    connectionString = ConfigurationManager.ConnectionStrings[name].ConnectionString;
                    break;
                }
            }

            if (connectionString == null)
                throw new ArgumentNullException("connectionString");

            foreach (var provider in Providers) {
                try {
                    logger.Trace("Migrations for {0} with context {1}...",
                                 provider.Assembly.FullName,
                                 provider.MigrationContext);
                    var migrator = new Migrator.Migrator(
                        provider.MigrationContext,
                        ProviderFactory.Create(provider.TransformationProviderName, connectionString),
                        provider.Assembly,
                        true,
                        logger);
                    migrator.MigrateToLastVersion();
                }
                catch { }
            }

            return writer.GetEntries();
        }

        private class ToBufferLogWriter : ILogWriter
        {
            private readonly StringBuilder result = new StringBuilder();

            public void Write(string message, params object[] args)
            {
                result.AppendFormat(message, args);
            }

            public void WriteLine(string message, params object[] args)
            {
                result.AppendFormat(message, args);
                result.Append('\u9999');
            }

            public IEnumerable<string> GetEntries()
            {
                return result.ToString().Split('\u9999');
            }
        }
    }
}
