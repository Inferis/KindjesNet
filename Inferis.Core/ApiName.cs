using System;

namespace Inferis.Core
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ApiNameAttribute : Attribute
    {
        public ApiNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}