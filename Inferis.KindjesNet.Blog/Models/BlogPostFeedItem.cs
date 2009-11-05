using System;
using System.Collections.Generic;
using Inferis.KindjesNet.Core;
using Inferis.KindjesNet.Core.Models;
using Inferis.KindjesNet.Core.Utils;

namespace Inferis.KindjesNet.Blog.Models
{
    public class BlogPostFeedItem : IFeedItem
    {
        private readonly Post post;

        public BlogPostFeedItem(Post post, int order)
        {
            if (post == null)
                throw new ArgumentNullException("post");

            this.post = post;
            Order = order;
        }

        public IFeedAttachment Attachment
        {
            get { return null; }
        }

        public int Order { get; private set; }
        public string Provider { get { return "Blog"; } }

        public string Body { get { return post.Body.GetFirstWords(); } }

        public DateTime Date { get { return post.PostDate; } }

        public IEnumerable<IFeedAttachment> Attachments
        {
            get
            {
                return null;
            }
        }

        public string Color { get { return "green"; } }

        public string Icon { get { return "blog"; } }

        public string Url
        {
            get { return post.GenerateUrl(); }
        }

        public string Title
        {
            get { return post.Title; }
        }

        public bool CanComment
        {
            get { return true; }
        }
    }
}
