using System;
using System.ComponentModel.Composition;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions;
using Inferis.KindjesNet.Core.Models;
using Inferis.KindjesNet.Core.Plugins;

namespace Inferis.KindjesNet.Core
{
    [Export(typeof(IMappingConfigurator))]
    public class CoreMappingConfigurator : IMappingConfigurator
    {
        public void Configure(MappingConfiguration config, Action<IConventionFinder> defaultConventions)
        {
            config.AutoMappings.Add(AutoMap.AssemblyOf <CoreMappingConfigurator>()
                .WhereTypeIsDataModel()
                .Conventions.Setup(defaultConventions));
        }
    }
}