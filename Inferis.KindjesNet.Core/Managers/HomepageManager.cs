using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Inferis.KindjesNet.Core.Models;
using Inferis.KindjesNet.Core.Plugins;

namespace Inferis.KindjesNet.Core.Managers
{
    public class HomepageManager : IHomepageManager
    {
        [ImportMany(AllowRecomposition = true)]
        public IEnumerable<IFeedItemProvider> FeedItemProviders { get; set; } 

        public List<IFeedItem> GetFeedItems()
        {
            var result = new List<IFeedItem>();
            if (FeedItemProviders == null)
                return result;

            foreach (var provider in FeedItemProviders) {
                var items = provider.GetItems(15);
                if (items != null) 
                    result.AddRange(items);
            }

            return result
                .OrderByDescending(item => item.Date)
                .ThenByDescending(item => item.Order)
                .ThenBy(item => item.Provider).Take(15).ToList();
        }
    }
}
