using System.ComponentModel.Composition;
using FluentNHibernate.Automapping;
using Inferis.KindjesNet.Blog.Models;
using Inferis.KindjesNet.Core.Plugins;

namespace Inferis.KindjesNet.Blog
{
    [Export(typeof(IMappingConfigurator))]
    public class BlogMappingConfigurator : DataModelMappingConfigurator
    {
        protected override AutoPersistenceModel FilterModels(AutoPersistenceModel model)
        {
            return base.FilterModels(model)
                .Override<Post>(map => 
                    map.HasManyToMany(post => post.Kids).Cascade.All());
        }
    }
}
