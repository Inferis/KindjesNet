using System;
using System.Collections.Generic;
using Inferis.KindjesNet.Vimeo.Models;

namespace Inferis.KindjesNet.Vimeo.Managers
{
    public class VimeoManager : IVimeoManager
    {
        public VimeoVideo GetVideo(int year, int month, int day, string slug)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VimeoVideo> GetMostRecentVideos(int maxItems)
        {
            throw new NotImplementedException();
        }
    }
}
