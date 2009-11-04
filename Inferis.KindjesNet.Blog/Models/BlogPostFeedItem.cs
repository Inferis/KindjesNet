using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inferis.KindjesNet.Core;
using Inferis.KindjesNet.Core.Models;

namespace Inferis.KindjesNet.Blog.Models
{
    public class BlogPostFeedItem : IFeedItem
    {
        private readonly Post post;

        public BlogPostFeedItem(Post post, int order)
        {
            this.post = post;
            Order = order;
        }

        public int Order { get; private set; }

        public string Provider
        {
            get { return "dagboek"; }
        }

        public string Url
        {
            get { return post.GenerateUrl(); }
        }

        public string Title
        {
            get { throw new NotImplementedException(); }
        }

        public string Body
        {
            get { throw new NotImplementedException(); }
        }

        public DateTime Date
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<IFeedAttachment> Attachments
        {
            get { throw new NotImplementedException(); }
        }

        public string Color
        {
            get { throw new NotImplementedException(); }
        }

        public string Icon
        {
            get { throw new NotImplementedException(); }
        }
    }
}
