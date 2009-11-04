using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Inferis.API.Vimeo.Advanced.Callers;

namespace Inferis.API.Vimeo.Advanced
{
    public abstract class ApiBase : IApiSettings
    {
        public const string DefaultBaseUri = "http://vimeo.com/api/rest/v2";
        public const string DefaultAuthUri = "http://vimeo.com/services/auth";

        protected ApiBase(string key)
        {
            BaseUri = DefaultBaseUri;
            Network = API.Vimeo.Network.Default;
            ApiKey = key;

            Anonymous = new AnonymousCaller(this);
            Authenticated = new AuthenticatedCaller(this);
            Signed = new SignedCaller(this);
        }

        public string BaseUri { get; set; }
        public string ApiKey { get; set; }
        public INetwork Network { get; set; }

        protected IAnonymousCaller Anonymous { get; private set; }
        protected IAuthenticatedCaller Authenticated { get; private set; }
        protected ISignedCaller Signed { get; private set; }

        protected string MethodName()
        {
            var stackTrace = new StackTrace();
            var method = stackTrace.GetFrame(1).GetMethod();
            return string.Join(".", new[] { "vimeo", method.DeclaringType.Name.ToLower(), method.Name.Substring(0, 1) + method.Name.Substring(1) });
        }

        protected static T MapFromXml<T>(XElement from)
            where T : class, new()
        {
            var to = new T();
            if (MapFromXml(from, to).Count == 0)
                throw new InvalidOperationException("No properties could be mapped from xml");
            return to;
        }

        protected static IDictionary<string, string> MapFromXml<T>(XElement from, T to)
            where T : class
        {
            if (from == null)
                throw new ArgumentNullException("from");
            if (to == null)
                throw new ArgumentNullException("to");

            var strategies = new Func<string, string>[] { 
                name => name,
                name => name.ToLower(),
                name => name.Substring(0,1).ToUpper() + name.Substring(1),
                name => string.Join("", name.Split('_').ToList().ConvertAll(s => s.Substring(0,1).ToUpper() + s.Substring(1)).ToArray()),
            };
            var mapped = new Dictionary<string, string>();

            var toType = typeof(T);
            foreach (var attr in from.Attributes()) {
                foreach (var strategy in strategies) {
                    var name = strategy(attr.Name.LocalName);
                    var prop = toType.GetProperty(name);
                    if (prop == null) continue;

                    mapped[name] = attr.Name.LocalName;
                    if (prop.PropertyType == typeof(DateTime))
                        prop.SetValue(to, DateTime.Parse(attr.Value), null);
                    else if (prop.PropertyType == typeof(int))
                        prop.SetValue(to, int.Parse(attr.Value), null);
                    else if (prop.PropertyType == typeof(double))
                        prop.SetValue(to, double.Parse(attr.Value), null);
                    else if (prop.PropertyType == typeof(string))
                        prop.SetValue(to, attr.Value, null);
                    else if (prop.PropertyType == typeof(bool)) {
                        bool value;
                        if (bool.TryParse(attr.Value, out value))
                            prop.SetValue(to, value, null);
                        else {
                            var avalue = attr.Value.Trim().ToLower();
                            switch (avalue) {
                                case "no":
                                case "0":
                                    prop.SetValue(to, false, null);
                                    break;
                                case "yes":
                                case "1":
                                    prop.SetValue(to, true, null);
                                    break;
                                default:
                                    throw new FormatException("String was not recognized as a valid Boolean");
                            }
                        }
                    }
                    else
                        throw new InvalidOperationException("cannot map to other types than string, datetime, int, double, bool");
                    break;
                }

            }

            return mapped;
        }
    }
}
