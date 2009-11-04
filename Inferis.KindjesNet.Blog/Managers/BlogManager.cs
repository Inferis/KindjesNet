using System;
using System.Collections.Generic;
using System.Linq;
using Inferis.KindjesNet.Blog.Models;
using Inferis.KindjesNet.Core.Data;
using Inferis.KindjesNet.Core.Managers;
using Microsoft.Practices.Unity;
using NHibernate;
using NHibernate.Linq;

namespace Inferis.KindjesNet.Blog.Managers
{
    public class BlogManager : IBlogManager
    {
        [Dependency]
        public IRepository Repository { get; set; }

        [Dependency]
        public IDateFixer DateFixer { get; set; }

        [Dependency]
        public ISlugGenerator SlugGenerator { get; set; }

        public Post GetPostByLegacyId(int id)
        {
            return Repository.Query<Post>()
                .FirstOrDefault(p => p.LegacyId == id);
        }

        public Post GetPost(int year, int month, int day, string slug)
        {
            slug = slug.ToLower();
            var fromDate = DateFixer.Fix(ref year, ref month, ref day);
            var toDate = fromDate.AddDays(1);

            return Repository.Query<Post>()
                .FirstOrDefault(p =>
                    p.Slug == slug &&
                    p.PostDate >= fromDate && p.PostDate < toDate);
        }

        public List<Post> GetPosts(int year, int month, int day)
        {
            var fromDate = DateFixer.Fix(ref year, ref month, ref day);
            var toDate = fromDate.AddDays(1);
            return GetPostsBetween(fromDate, toDate);
        }

        public List<Post> GetPosts(int year, int month)
        {
            var day = 1;
            var fromDate = DateFixer.Fix(ref year, ref month, ref day);
            var toDate = fromDate.AddMonths(1);
            return GetPostsBetween(fromDate, toDate);
        }

        public List<Post> GetPosts(int year)
        {
            var day = 1;
            var month = 1;
            var fromDate = DateFixer.Fix(ref year, ref month, ref day);
            var toDate = fromDate.AddYears(1);
            return GetPostsBetween(fromDate, toDate);
        }

        public void SavePost(Post post)
        {
            Repository.SaveOrUpdate(post);
        }

        public string Slugify(string source, DateTime checkDate, Guid? ignorePostId)
        {
            var root = SlugGenerator.GenerateSlug(source);
            var posts = GetPosts(checkDate.Year, checkDate.Month, checkDate.Day);
            var tries = 0;
            while (true) {
                var candidate = tries > 0 ? (root + "-" + tries) : root;
                if (!posts.Any(p => p.Slug == candidate && (ignorePostId == null || ignorePostId != p.Id)))
                    return candidate;
                tries++;
            }

            return null;
        }

        public List<Post> Search(string query)
        {
            IQueryable<Post> results = Repository.Query<Post>();

            foreach (var q in query.Split(' ', ',')) {
                var tq = q.Trim();
                if (!string.IsNullOrEmpty(tq)) {
                    results = results.Where(p => p.Title.Contains(tq) || p.Body.Contains(tq));
                }
            }

            return results.ToList();
        }

        public List<Post> GetMostRecentPosts(int maxPosts)
        {
            return Repository.Query<Post>()
                .OrderByDescending(p => p.PostDate)
                .OrderBy(p => p.Title)
                .Take(maxPosts)
                .ToList();
        }

        #region Private Helpers
        private List<Post> GetPostsBetween(DateTime fromDate, DateTime toDate)
        {
            return Repository.Query<Post>()
                .Where(p => p.PostDate >= fromDate && p.PostDate < toDate)
                .ToList();
        }
        #endregion

    }
}
