using System.Collections.Generic;
using System.Xml.Linq;

namespace Inferis.API.Vimeo.Advanced.Callers
{
    public class AuthenticatedCaller : IAuthenticatedCaller
    {
        private readonly IApiSettings settings;

        public AuthenticatedCaller(IApiSettings settings)
        {
            this.settings = settings;
        }

        public ICallerImpl WithToken(string token)
        {
            return new AuthenticatedCallerImpl(settings, token);
        }

        private class AuthenticatedCallerImpl : CallerImplBase
        {
            private readonly string token;

            public AuthenticatedCallerImpl(IApiSettings settings, string token)
                : base(settings)
            {
                this.token = token;
            }

            public override XDocument Call(string method, IDictionary<string, string> parameters)
            {
                parameters = parameters ?? new Dictionary<string, string>();
                parameters[Parameters.AuthToken] = token;
                return base.Call(method, parameters);
            }

        }
    }
}
