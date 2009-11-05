using System;
using Inferis.KindjesNet.Core;
using Inferis.KindjesNet.Core.Models;

namespace Inferis.KindjesNet.Vimeo.Models
{
    [DataModel]
    [ItemWithUrlType("vimeo")]
    public class VimeoVideo : KidsRelated, IItemWithUrl
    {
        public virtual Guid Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Caption { get; set; }
        public virtual DateTime UploadDate { get; set; }
        public virtual string Slug { get; set; }

        public virtual int Width { get; set; }
        public virtual int Height { get; set; }
        public virtual int Duration { get; set; }
        
        public virtual string VimeoPageUrl { get; set; }
        public virtual string VimeoId { get; set; }
        public virtual string VimeoOwnerId { get; set; }
        public virtual string Privacy { get; set; }
        public virtual bool IsHighDefinition { get; set; }

        public virtual DateTime DateForUrl
        {
            get { return UploadDate; }
        }

    }
}
