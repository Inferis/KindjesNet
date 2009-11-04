using System;
using System.ComponentModel.Composition;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions;
using Inferis.KindjesNet.Core;
using Inferis.KindjesNet.Core.Models;
using Inferis.KindjesNet.Core.Plugins;

namespace Inferis.KindjesNet.Blog
{
    [Export(typeof(IMappingConfigurator))]
    public class BlogMappingConfigurator : IMappingConfigurator
    {
        public void Configure(MappingConfiguration config, Action<IConventionFinder> defaultConventions)
        {
            config.AutoMappings.Add(
                    AutoMap.AssemblyOf<BlogMappingConfigurator>()
                    .WhereTypeIsDataModel()
                    .Conventions.Setup(defaultConventions))
                .ExportTo(@"c:\hbm");
        }

    }
}
