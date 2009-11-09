using System;
using System.Reflection;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions;

namespace Inferis.KindjesNet.Core.Plugins
{
    public abstract class BaseMappingConfigurator : IMappingConfigurator
    {
        public void Configure(MappingConfiguration config, Action<IConventionFinder> defaultConventions)
        {
            var model = AutoMap.Assembly(GetMappingAssembly());
            model = FilterModels(model);
            model.Conventions.Setup(FilterConventions(defaultConventions));
            config.AutoMappings.Add(model);
        }

        protected abstract AutoPersistenceModel FilterModels(AutoPersistenceModel model);

        protected virtual Assembly GetMappingAssembly()
        {
            return GetType().Assembly;
        }

        protected virtual Action<IConventionFinder> FilterConventions(Action<IConventionFinder> conventions)
        {
            return conventions;
        }

    }
}
