using System;
using Inferis.KindjesNet.Core;
using Inferis.KindjesNet.Core.Models;

namespace Inferis.KindjesNet.Blog.Models
{
    [DataModel]
    [ItemWithUrlType("blog")]
    public class Post : KidsRelated, IItemWithUrl
    {
        public virtual Guid Id { get; set; }

        public virtual string Slug { get; set; }
        public virtual string Title { get; set; }
        public virtual string Body { get; set; }
        public virtual DateTime PostDate { get; set; }
        public virtual User Author { get; set; }
        public virtual int? LegacyId { get; set; }

        public virtual DateTime DateForUrl
        {
            get { return PostDate; }
        }
    }
}
