using System.Collections.Generic;

namespace Inferis.API.Vimeo.Advanced.Callers
{
    public interface ISignedCallerImpl : ICallerImpl
    {
        string GenerateSignature(object parameters);
        string GenerateSignature(IDictionary<string, string> parameters);
    }
}
