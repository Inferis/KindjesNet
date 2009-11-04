using System;
using System.Linq;

namespace Inferis.KindjesNet.Core.Data
{
    public interface IRepository : IDisposable
    {
        IQueryable<T> Query<T>();
        object Save<T>(T item1);
        void SaveOrUpdate<T>(T item1);
    }
}
