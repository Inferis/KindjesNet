using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions;

namespace Inferis.KindjesNet.Core.Plugins
{
    public interface IMappingConfigurator
    {
        void Configure(MappingConfiguration config, Action<IConventionFinder> defaultConventions);
    }
}
