using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Inferis.API.Vimeo.Advanced.Callers
{
    public class SignedCaller : ISignedCaller
    {
        private readonly IApiSettings settings;

        public SignedCaller(IApiSettings settings)
        {
            this.settings = settings;
        }

        public ISignedCallerImpl WithSecret(string secret)
        {
            return new SignedCallerImpl(settings, secret);
        }

        public class SignedCallerImpl : CallerImplBase, ISignedCallerImpl
        {
            private readonly string secret;

            public SignedCallerImpl(IApiSettings settings, string secret)
                : base(settings)
            {
                this.secret = secret;
            }

            protected override Uri GenerateMethodUri(IDictionary<string, string> parameters)
            {
                if (parameters.ContainsKey(Parameters.ApiSig))
                    parameters.Remove(Parameters.ApiSig);
                var signature = GenerateSignature(parameters);
                parameters[Parameters.ApiSig] = signature;

                // just proceed as usual
                return base.GenerateMethodUri(parameters);
            }

            private string GenerateMD5(string inputString)
            {
                var algorithm = MD5.Create();
                var inputBuffer = Encoding.ASCII.GetBytes(inputString);
                var md5Buffer = algorithm.ComputeHash(inputBuffer);

                var hex = new StringBuilder(inputBuffer.Length * 2);
                foreach (var bit in md5Buffer) {
                    hex.AppendFormat("{0:x2}", bit);
                }
                return hex.ToString();
            }

            public string GenerateSignature(object parameters)
            {
                return GenerateSignature(ToDictionary(parameters));
            }

            public string GenerateSignature(IDictionary<string, string> parameters)
            {
                var rawSignature = new StringBuilder();

                // 3 begin with the secret key
                rawSignature.Append(secret);

                // 1+2 Create the signature string (alphabetized params!)
                foreach (var key in parameters.Keys.OrderBy(p => p)) {
                    rawSignature.Append(key);
                    rawSignature.Append(parameters[key] ?? "");
                }

                // 4 MD5 it
                return GenerateMD5(rawSignature.ToString());
            }
        }
    }
}
