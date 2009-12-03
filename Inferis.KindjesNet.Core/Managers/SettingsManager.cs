using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Configuration;
using System.Xml.Serialization;
using Inferis.KindjesNet.Core.Data;
using Inferis.KindjesNet.Core.Models;
using Microsoft.Practices.Unity;

namespace Inferis.KindjesNet.Core.Managers
{
    public class SettingsManager : ISettingsManager
    {
        [Dependency]
        public IRepository Repository { get; set; }

        public ISettingsInstance For<T>()
        {
            return new Instance<T>(Repository);
        }

        public class Instance<T> : ISettingsInstance
        {
            private readonly IRepository repository;

            public Instance(IRepository repository)
            {
                this.repository = repository;
            }

            public void Set<TAs>(string name, TAs value)
            {
                var sname = SettingName(name);
                var setting = repository.Query<Setting>().FirstOrDefault(s => s.Id == sname)
                              ?? new Setting() { Id = sname };

                if (typeof(TAs) == typeof(string)) {
                    setting.Value = (string)(object)value;
                }
                else {
                    var buffer = new StringBuilder();
                    using (var stream = new StringWriter(buffer)) {
                        var serializer = new XmlSerializer(typeof(TAs));
                        serializer.Serialize(stream, value);
                    }
                    setting.Value = buffer.ToString();
                }

                repository.SaveOrUpdate(setting);
            }

            public TAs Get<TAs>(string name)
            {
                var sname = SettingName(name);
                var setting = repository.Query<Setting>().FirstOrDefault(s => s.Id == sname);

                string source;
                if (setting == null || setting.Value == null) {
                    // fallback through appsettings
                    if (WebConfigurationManager.AppSettings[sname] == null)
                        return default(TAs); // still not found
                    source = WebConfigurationManager.AppSettings[sname];
                }
                else
                    source = setting.Value;

                TAs result;
                if (typeof(TAs) == typeof(string)) {
                    result = (TAs)(object)source;                    
                }
                else {
                    using (var stream = new StringReader(source)) {
                        var serializer = new XmlSerializer(typeof(TAs));
                        result = (TAs)serializer.Deserialize(stream);
                    }
                }

                return result;
            }

            private static string SettingName(string name)
            {
                return string.Concat(typeof(T).FullName, ".", name.Replace(" ", "_"));
            }
        }
    }
}
