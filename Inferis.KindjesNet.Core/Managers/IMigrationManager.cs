using System.Collections.Generic;

namespace Inferis.KindjesNet.Core.Managers
{
    public interface IMigrationManager {
        IEnumerable<string> RunAllMigrations();
    }
}