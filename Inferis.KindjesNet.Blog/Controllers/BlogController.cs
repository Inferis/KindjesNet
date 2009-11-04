using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Web.Mvc;
using Inferis.KindjesNet.Blog.Managers;
using Inferis.KindjesNet.Blog.Models;
using Inferis.KindjesNet.Blog.Models.Controllers;
using Inferis.KindjesNet.Blog.OldServices.Blog;
using Inferis.KindjesNet.Core;
using Inferis.KindjesNet.Core.Managers;
using Inferis.KindjesNet.Core.Models;
using Inferis.KindjesNet.Core.Mvc;
using Inferis.KindjesNet.Core.Mvc.Controllers;
using Microsoft.Practices.Unity;

namespace Inferis.KindjesNet.Blog.Controllers
{
    [Export(typeof(IController))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BlogController : ControllerWithKids
    {
        [Dependency]
        public IBlogManager BlogManager { get; set; }

        [Dependency]
        public IUserManager UserManager { get; set; }

        public ActionResult OldPost()
        {
            int id;
            if (!int.TryParse(Request.QueryString.ToString(), out id))
                return new NotFoundResult();

            var post = BlogManager.GetPostByLegacyId(id);
            if (post == null)
                return new NotFoundResult();

            return new RedirectResult("../" + post.GenerateUrl());
        }

        public ActionResult Item(int year, int month, int day, string extra)
        {
            // extra = slug
            var post = BlogManager.GetPost(year, month, day, extra);
            if (post == null)
                return new NotFoundResult();
            
            return Item(post);
        }

        private ActionResult Item(Post post)
        {
            return View("Post", post);
        }

        public ActionResult ArchiveDay(int year, int month, int day)
        {
            return View(new ArchiveModel(year, month, day, BlogManager.GetPosts(year, month, day)));
        }

        public ActionResult ArchiveMonth(int year, int month)
        {
            return View(new ArchiveModel(year, month, BlogManager.GetPosts(year, month)));
        }

        public ActionResult ArchiveYear(int year)
        {
            return View(new ArchiveModel(year, BlogManager.GetPosts(year)));
        }

        public ActionResult Import()
        {
            // get users cache first
            var users = new Dictionary<int, User>();
            foreach (var user in UserManager.GetAllLegacyUsers()) {
                users[(int)user.LegacyId] = user;
            }

            using (var client = new BlogBackendSoapClient()) {
                try {
                    var data = client.GetArticlesByBlog(1);

                    foreach (DataRow row in data.Tables[0].Rows) {
                        var id = Convert.ToInt32(row["ArticleId"]);
                        var post = BlogManager.GetPostByLegacyId(id) ?? new Post { LegacyId = id };

                        post.Title = Convert.ToString(row["Title"]);
                        post.Body = Convert.ToString(row["Body"]);
                        post.PostDate = Convert.ToDateTime(row["Stamp"]).ToUniversalTime();
                        post.Author = users[Convert.ToInt32(row["PosterId"])];
                        post.Slug = BlogManager.Slugify(post.Title, post.PostDate, post.Id);

                        BlogManager.SavePost(post);
                    }
                }
                catch {
                    client.Abort();
                }
            }

            return View();
        }

        public ActionResult Search(string q)
        {
            if (string.IsNullOrEmpty(q)) {
                // show search input page
                return View("Search");
            }
            else {
                return View("SearchResults", BlogManager.Search(q) ?? new List<Post>());
            }
        }
    }
}