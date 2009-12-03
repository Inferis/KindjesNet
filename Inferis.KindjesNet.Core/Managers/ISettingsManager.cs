using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inferis.KindjesNet.Core.Managers
{
    public interface ISettingsManager
    {
        ISettingsInstance For<T>();
    }

    public interface ISettingsInstance
    {
        void Set<TAs>(string name, TAs value);
        TAs Get<TAs>(string name);
    }
}
