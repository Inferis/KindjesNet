using System;
using System.Linq;

namespace Inferis.Core
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ApiValueAttribute : Attribute
    {
        public ApiValueAttribute(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }
    }

    public static class ApiValueExtensions
    {
        public static string GetApiValue(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            if (fi == null)
                throw new InvalidOperationException("Cannot get field info of enum");

            var attr = fi.GetCustomAttributes(typeof(ApiValueAttribute), false).FirstOrDefault() as ApiValueAttribute;
            return attr == null ? value.ToString() : attr.Value;
        }
    }
}