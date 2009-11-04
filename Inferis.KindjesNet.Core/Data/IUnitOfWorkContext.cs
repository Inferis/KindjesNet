using System.Collections;
using System.Web;
using Microsoft.Practices.Unity;

namespace Inferis.KindjesNet.Core.Data
{
    public interface IUnitOfWorkContext
    {
        IDictionary Items { get; }
        T Resolve<T>();
        T Resolve<T>(string name);
    }
}
