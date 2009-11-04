using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inferis.KindjesNet.Vimeo.Models;

namespace Inferis.KindjesNet.Vimeo.Managers
{
    public interface IVimeoManager
    {
        VimeoVideo GetVideo(int year, int month, int day, string slug);
        IEnumerable<VimeoVideo> GetMostRecentVideos(int maxItems);
    }
}
