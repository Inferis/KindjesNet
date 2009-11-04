using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Inferis.KindjesNet.Blog.Managers;
using Inferis.KindjesNet.Blog.Models;
using Inferis.KindjesNet.Core.Models;
using Inferis.KindjesNet.Core.Plugins;
using Microsoft.Practices.Unity;

namespace Inferis.KindjesNet.Blog
{
    [Export(typeof(IFeedItemProvider))]
    public class BlogFeedItemProvider : IFeedItemProvider
    {
        [Import]
        public IBlogManager BlogManager { get; set; }

        public IEnumerable<IFeedItem> GetItems(int maxItems)
        {
            var order = 0;
            return BlogManager.GetMostRecentPosts(maxItems)
                .ConvertAll<IFeedItem>(p => new BlogPostFeedItem(p, order++));
        }
    }
}
