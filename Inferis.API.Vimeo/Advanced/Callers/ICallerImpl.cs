using System.Collections.Generic;
using System.Xml.Linq;

namespace Inferis.API.Vimeo.Advanced.Callers
{
    public interface ICallerImpl
    {
        XDocument Call(string method, IDictionary<string, string> parameters);
        XDocument Call(string method, object parameters);
        XDocument Call(string method);
    }
}
