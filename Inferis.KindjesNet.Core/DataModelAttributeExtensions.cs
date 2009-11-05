using FluentNHibernate.Automapping;

namespace Inferis.KindjesNet.Core
{
    public static class DataModelAttributeExtensions
    {
        public static AutoPersistenceModel WhereTypeIsDataModel(this AutoPersistenceModel persistenceModel)
        {
            return persistenceModel.Where(t =>
                t.GetCustomAttributes(typeof(DataModelAttribute), true).Length == 1
                );
        }
    }
}
