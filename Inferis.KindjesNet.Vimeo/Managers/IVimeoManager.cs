using System;
using System.Collections.Generic;
using Inferis.KindjesNet.Vimeo.Models;

namespace Inferis.KindjesNet.Vimeo.Managers
{
    public interface IVimeoManager
    {
        bool VideoExistsWithVimeoId(string vimeoId);
        VimeoVideo GetVideoByVimeoId(string vimeoId);

        /// <summary>
        /// Gets a post by year, month, day and slug.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="slug"></param>
        /// <returns></returns>
        VimeoVideo GetVideo(int year, int month, int day, string slug);
        
        List<VimeoVideo> GetVideos(int year, int month, int day);
        List<VimeoVideo> GetVideos(int year, int month);
        List<VimeoVideo> GetVideos(int year);

        void SaveVideo(VimeoVideo video);
        
        string Slugify(string source, DateTime checkDate, Guid? ignorePostId);
        
        List<VimeoVideo> GetMostRecentVideos(int items);
    }
}