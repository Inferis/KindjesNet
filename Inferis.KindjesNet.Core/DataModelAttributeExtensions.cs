using System.Linq;
using FluentNHibernate.Automapping;

namespace Inferis.KindjesNet.Core
{
    public static class DataModelAttributeExtensions
    {
        public static AutoPersistenceModel WhereTypeIsDataModel(this AutoPersistenceModel model)
        {
            return model.Where(t => t.GetCustomAttributes(typeof (DataModelAttribute), false).Any());
        }
    }
}