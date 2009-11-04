using System;
using System.Collections.Generic;
using Microsoft.Practices.ObjectBuilder2;

namespace Inferis.KindjesNet.Core.Unity
{
    public interface IDeepBuildupPolicy : IBuilderPolicy
    {
        void Mark<T>();
        void Mark(Type t);
        bool IsMarked<T>();
        bool IsMarked(Type t);
    }

    public class DeepBuildupPolicy : IDeepBuildupPolicy
    {
        private readonly List<Type> types = new List<Type>();
        private object typesLock = new object();

        public void Mark<T>()
        {
            Mark(typeof(T));
        }

        public void Mark(Type t)
        {
            lock (typesLock) {
                if (!types.Contains(t))
                    types.Add(t);
            }
        }

        public bool IsMarked<T>()
        {
            return IsMarked(typeof(T));
        }

        public bool IsMarked(Type t)
        {
            return types.Contains(t);
        }
    }
}
