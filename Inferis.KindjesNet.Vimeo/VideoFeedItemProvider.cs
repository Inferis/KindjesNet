using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Inferis.KindjesNet.Core.Models;
using Inferis.KindjesNet.Core.Plugins;
using Inferis.KindjesNet.Vimeo.Managers;
using Inferis.KindjesNet.Vimeo.Models;

namespace Inferis.KindjesNet.Vimeo
{
    [Export(typeof(IFeedItemProvider))]
    public class VideoFeedItemProvider : IFeedItemProvider
    {
        [Import]
        public IVimeoManager VideoManager { get; set; }

        public IEnumerable<IFeedItem> GetItems(int maxItems)
        {
            var order = 0;

            var result = new List<IFeedItem>();
            var batch = new List<VimeoVideo>();

            foreach (var video in VideoManager.GetMostRecentVideos(maxItems*3)) {
                batch.Add(video);
                if (batch.First().UploadDate <= video.UploadDate.AddHours(1)) 
                    continue;
                
                result.Add(new VimeoVideoFeedItem(batch, order++));
                batch.Clear();
            }
            if (batch.Count > 0)
                result.Add(new VimeoVideoFeedItem(batch, order));

            return result.Take(maxItems);
        }
    }
}
