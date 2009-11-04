using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Inferis.API.Vimeo.Advanced.Responses;

namespace Inferis.API.Vimeo.Advanced
{
    public class Auth : ApiBaseWithSecret
    {
        public class Methods
        {
            public const string CheckToken = "vimeo.auth.checkToken";
            public const string GetFrob = "vimeo.auth.getFrob";
            public const string GetToken = "vimeo.auth.getToken";
        }

        public Auth(string key, string secret)
            : base(key, secret)
        {
        }

        public string GetFrob()
        {
            var xdoc = Signed.WithSecret(Secret).Call(Methods.GetFrob);
            return xdoc.Root.Descendants("frob").First().Value;
        }

        public TokenInfo GetToken(string frob)
        {
            return InfoFrom(Signed.WithSecret(Secret).Call(Methods.GetToken, new { frob }));
        }

        public TokenInfo CheckToken(string token)
        {
            return InfoFrom(Authenticated.WithToken(token).Call(Methods.CheckToken));
        }

        public Uri GenerateAuthenticationUri(string frob)
        {
            var uriParams = new Dictionary<string, string>() {
                { Parameters.ApiKey, ApiKey },
                { Parameters.Perms, "read" },
                { Parameters.Frob, frob },
            };

            uriParams[Parameters.ApiSig] = Signed.WithSecret(Secret).GenerateSignature(uriParams);

            return new Uri(DefaultAuthUri).AddParameters(uriParams);
        }

        private TokenInfo InfoFrom(XDocument xdoc)
        {
            var auth = xdoc.Root.Descendants("auth").First();
            var user = auth.Descendants("user").First();
            return new TokenInfo() {
                Token = auth.Descendants("token").First().Value,
                Perms = auth.Descendants("perms").First().Value.ToPermission(),
                UserFullName = user.Attribute("fullname").Value,
                UserName = user.Attribute("username").Value,
                UserId = int.Parse(user.Attribute("id").Value)
            };
        }
    }
}