using System;
using System.Collections.Generic;
using System.Linq;
using Inferis.KindjesNet.Core.Models;
using Inferis.KindjesNet.Core.Utils;

namespace Inferis.KindjesNet.Vimeo.Models
{
    public class VimeoVideoFeedItem : IFeedItem
    {
        private readonly List<VimeoVideo> videos;

        public VimeoVideoFeedItem(IEnumerable<VimeoVideo> videos, int order)
        {
            if (videos == null)
                throw new ArgumentNullException("videos");
            if (!videos.Any())
                throw new ArgumentException("videos");

            this.videos = new List<VimeoVideo>(videos);
            Body = string.Join(", ", videos.Select(v => v.Title).ToArray());
            Order = order;
        }

        public IEnumerable<IFeedAttachment> Attachments
        {
            get { return videos.Cast<IFeedAttachment>(); }
        }

        public string Color
        {
            get { return "orange"; }
        }

        public string Icon
        {
            get { return "vimeo"; }
        }

        public int Order { get; private set; }
        public string Provider { get { return "Vimeo"; } }
        public string Body { get; private set; }

        public DateTime Date { get { return videos.First().UploadDate; } }

        public string Url
        {
            get { return videos.First().GenerateUrl(); }
        }

        public string Title
        {
            get { return videos.Count() + " nieuwe videos op Vimeo"; }
        }

        public bool CanComment
        {
            get { return true; }
        }
    }
}