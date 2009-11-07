using System;
using System.ComponentModel.Composition;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions;
using Inferis.KindjesNet.Core;
using Inferis.KindjesNet.Core.Models;
using Inferis.KindjesNet.Core.Plugins;
using Inferis.KindjesNet.Vimeo.Models;

namespace Inferis.KindjesNet.Vimeo
{
    [Export(typeof(IMappingConfigurator))]
    public class VimeoMappingConfigurator : IMappingConfigurator
    {
        public void Configure(MappingConfiguration config, Action<IConventionFinder> defaultConventions)
        {
            config.AutoMappings.Add(
                AutoMap.AssemblyOf<VimeoMappingConfigurator>()
                    .WhereTypeIsDataModel()
                    .IgnoreBase<EntityWithKids>()
                    //.OverrideAll(s => s.IgnoreProperty(v => v.DateForUrl))
                    .Conventions.Setup(defaultConventions))
                .ExportTo(@"c:\hbm");
        }

    }
}