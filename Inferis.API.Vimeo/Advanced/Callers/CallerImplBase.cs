using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace Inferis.API.Vimeo.Advanced.Callers
{
    public abstract class CallerImplBase : ICallerImpl
    {
        private readonly string baseUri;
        private readonly string apiKey;
        private readonly INetwork network;

        protected CallerImplBase(IApiSettings settings)
        {
            this.baseUri = settings.BaseUri;
            this.apiKey = settings.ApiKey;
            this.network = settings.Network ?? Network.Default;
        }

        public virtual XDocument Call(string method, IDictionary<string, string> parameters)
        {
            parameters = parameters ?? new Dictionary<string, string>();

            parameters[Parameters.Method] = method;
            parameters[Parameters.ApiKey] = apiKey;

            XDocument result = null;
            using (var reader = network.GetResponse(GenerateMethodUri(parameters))) {
                var temp = reader.ReadToEnd();
                using (var r2 = new StringReader(temp)) 
                    result = XDocument.Load(r2);
            }

            if (result == null || result.Root == null)
                throw new InvalidVimeoResponseException();

            var stat = result.Root.Attribute("stat");
            if (stat == null)
                throw new FailedVimeoCallException(method);
            else if (stat.Value == "fail") {
                var err = result.Descendants("err").FirstOrDefault() ?? new XElement("err", new XAttribute("code", ""), new XAttribute("msg", ""));
                throw new FailedVimeoCallException(method,
                                                   err.Attribute("code").Value,
                                                   err.Attribute("msg").Value);
            }
            else {
                return result;
            }
        }

        public XDocument Call(string method, object parameters)
        {
            return Call(method, ToDictionary(parameters));
        }

        protected virtual Dictionary<string, string> ToDictionary(object parameters) {
            var translatedParameters = new Dictionary<string, string>();
            if (parameters != null) {
                foreach (PropertyDescriptor desc in TypeDescriptor.GetProperties(parameters)) {
                    translatedParameters[desc.Name] = desc.GetValue(parameters).ToString();
                }
            }

            return translatedParameters;
        }

        public XDocument Call(string method)
        {
            return Call(method, new Dictionary<string, string>());
        }

        protected virtual Uri GenerateMethodUri(IDictionary<string, string> parameters)
        {
            return new Uri(baseUri).AddParameters(parameters);
        }

        //protected virtual TextReader GetResponse(Uri uri)
        //{
        //    var request = (HttpWebRequest)WebRequest.Create(uri);
        //    return new StreamReader(request.GetResponse().GetResponseStream());
        //}
    }
}
