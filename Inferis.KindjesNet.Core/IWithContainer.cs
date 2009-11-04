using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace Inferis.KindjesNet.Core
{
    public interface IWithContainer
    {
        [Dependency]
        IUnityContainer Container { get; set; }
    }
}
