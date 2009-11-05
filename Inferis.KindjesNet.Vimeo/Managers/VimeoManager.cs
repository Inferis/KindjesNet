using System;
using System.Collections.Generic;
using System.Linq;
using Inferis.KindjesNet.Core.Data;
using Inferis.KindjesNet.Core.Managers;
using Inferis.KindjesNet.Vimeo.Models;
using Microsoft.Practices.Unity;

namespace Inferis.KindjesNet.Vimeo.Managers
{
    public class VimeoManager : IVimeoManager
    {
        [Dependency]
        public IRepository Repository { get; set; }

        [Dependency]
        public IDateFixer DateFixer { get; set; }

        [Dependency]
        public ISlugGenerator SlugGenerator { get; set; }

        public bool VideoExistsWithVimeoId(string vimeoId)
        {
            return Repository.Query<VimeoVideo>()
                .Any(v => v.VimeoId == vimeoId);
        }

        public VimeoVideo GetVideoByVimeoId(string vimeoId)
        {
            return Repository.Query<VimeoVideo>()
                .FirstOrDefault(v => v.VimeoId == vimeoId);
        }

        public VimeoVideo GetVideo(int year, int month, int day, string slug)
        {
            slug = slug.ToLower();
            var fromDate = DateFixer.Fix(ref year, ref month, ref day);
            var toDate = fromDate.AddDays(1);

            return Repository.Query<VimeoVideo>()
                .FirstOrDefault(p =>
                    p.Slug == slug &&
                    p.UploadDate >= fromDate && p.UploadDate < toDate);
        }

        public List<VimeoVideo> GetVideos(int year, int month, int day)
        {
            var fromDate = DateFixer.Fix(ref year, ref month, ref day);
            var toDate = fromDate.AddDays(1);
            return GetVideosBetween(fromDate, toDate);
        }

        public List<VimeoVideo> GetVideos(int year, int month)
        {
            var day = 1;
            var fromDate = DateFixer.Fix(ref year, ref month, ref day);
            var toDate = fromDate.AddMonths(1);
            return GetVideosBetween(fromDate, toDate);
        }

        public List<VimeoVideo> GetVideos(int year)
        {
            var day = 1;
            var month = 1;
            var fromDate = DateFixer.Fix(ref year, ref month, ref day);
            var toDate = fromDate.AddYears(1);
            return GetVideosBetween(fromDate, toDate);
        }

        public void SaveVideo(VimeoVideo video)
        {
            Repository.SaveOrUpdate(video);
        }

        public string Slugify(string source, DateTime checkDate, Guid? ignorePostId)
        {
            var root = SlugGenerator.GenerateSlug(source);
            var posts = GetVideos(checkDate.Year, checkDate.Month, checkDate.Day);
            var tries = 0;
            while (true) {
                var candidate = tries > 0 ? (root + "-" + tries) : root;
                if (!posts.Any(p => p.Slug == candidate && (ignorePostId == null || ignorePostId != p.Id)))
                    return candidate;
                tries++;
            }

            return null;
        }

        public List<VimeoVideo> GetMostRecentVideos(int maxPosts)
        {
            return Repository.Query<VimeoVideo>()
                .OrderByDescending(p => p.UploadDate)
                .OrderBy(p => p.Title)
                .Take(maxPosts)
                .ToList();
        }

        #region Private Helpers
        private List<VimeoVideo> GetVideosBetween(DateTime fromDate, DateTime toDate)
        {
            return Repository.Query<VimeoVideo>()
                .Where(p => p.UploadDate >= fromDate && p.UploadDate < toDate)
                .ToList();
        }
        #endregion
    }
}
