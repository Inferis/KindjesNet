using System;

namespace Inferis.KindjesNet.Core
{
    public class ItemWithUrlTypeAttribute : Attribute
    {
        public ItemWithUrlTypeAttribute(string type)
        {
            Type = type;
        }

        public string Type { get; private set; }
    }
}
