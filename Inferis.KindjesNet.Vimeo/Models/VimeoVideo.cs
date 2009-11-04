using System;
using Inferis.KindjesNet.Core;
using Inferis.KindjesNet.Core.Models;

namespace Inferis.KindjesNet.Vimeo.Models
{
    [DataModel]
    public class VimeoVideo : IItemWithUrl
    {
        public virtual string Title { get; set; }
        public virtual DateTime UploadDate { get; set; }
        public virtual string Slug { get; set; }

        public DateTime DateForUrl
        {
            get { return UploadDate; }
        }

    }
}