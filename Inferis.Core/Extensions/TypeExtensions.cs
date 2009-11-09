using System;
using System.Collections.Generic;
using System.Reflection;

namespace Inferis.Core.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Finds all constants in a type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static FieldInfo[] GetConstants(this Type type)
        {
            // Go through the list and only pick out the constants
            var constants = new List<FieldInfo>();
            foreach (var fi in type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)) {
                // IsLiteral determines if its value is written at compile time and not changeable
                // IsInitOnly determine if the field can be set  in the body of the constructor
                // for C# a field which is readonly keyword would have both true but a const field would have only IsLiteral equal to true
                if (fi.IsLiteral && !fi.IsInitOnly)
                    constants.Add(fi);
            }

            return constants.ToArray();
        }

        public static TAttribute GetCustomAttribute<TAttribute>(this Type item, bool inherit)
            where TAttribute : Attribute
        {
            foreach (var attr in item.GetCustomAttributes(inherit)) {
                if (attr is TAttribute)
                    return (TAttribute)attr;
            }
            return null;
        }

        public static TAttribute GetCustomAttribute<TAttribute>(this Type item)
            where TAttribute : Attribute
        {
            return GetCustomAttribute<TAttribute>(item, true);
        }

        public static IEnumerable<TAttribute> GetCustomAttributes<TAttribute>(this Type item)
            where TAttribute : Attribute
        {
            return GetCustomAttributes<TAttribute>(item, true);
        }

        public static IEnumerable<TAttribute> GetCustomAttributes<TAttribute>(this Type item, bool inherit)
            where TAttribute : Attribute
        {
            foreach (var attr in item.GetCustomAttributes(inherit)) {
                if (attr is TAttribute)
                    yield return (TAttribute)attr;
            } 
        }

    }
}