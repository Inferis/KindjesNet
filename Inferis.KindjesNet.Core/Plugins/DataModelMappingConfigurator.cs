using System;
using System.Reflection;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions;

namespace Inferis.KindjesNet.Core.Plugins
{
    public abstract class DataModelMappingConfigurator : BaseMappingConfigurator
    {
        protected override AutoPersistenceModel FilterModels(AutoPersistenceModel model)
        {
            return model.WhereTypeIsDataModel();
        }
    }
}
