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
    public class VimeoMappingConfigurator : DataModelMappingConfigurator
    {
        protected override AutoPersistenceModel FilterModels(AutoPersistenceModel model)
        {
            return base.FilterModels(model)
                .Override<VimeoVideo>(map => map.HasManyToMany(post => post.Kids));
        }
    }
}