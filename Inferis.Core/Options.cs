using System;
using System.Collections.Generic;
using System.Linq;

namespace Inferis.Core
{
    public class Options
    {
        public static IDictionary<string, string> Merge<TOptions>(params Action<TOptions>[] options)
            where TOptions : Options, new()
        {
            return Merge((IEnumerable<Action<TOptions>>)options);
        }

        public static IDictionary<string, string> Merge<TOptions>(IEnumerable<Action<TOptions>> options)
            where TOptions : Options, new()
        {
            var target = new TOptions();
            foreach (var opt in options) {
                opt(target);
            }

            var result = new Dictionary<string, string>();
            foreach (var prop in target.GetType().GetProperties()) {
                var attr = prop.GetCustomAttributes(typeof(ApiNameAttribute), true).FirstOrDefault() as ApiNameAttribute;
                if (attr == null) continue;

                var value = prop.GetValue(target, null);
                if (value == null) continue;

                if (value is Enum)
                    result[attr.Name] = ((Enum)value).GetApiValue();
                else
                    result[attr.Name] = value.ToString();
            }
            return result;
        }
    }
}
