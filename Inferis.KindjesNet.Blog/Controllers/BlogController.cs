using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Web.Mvc;
using Inferis.KindjesNet.Blog.Managers;
using Inferis.KindjesNet.Blog.Models;
using Inferis.KindjesNet.Blog.Models.Controllers;
using Inferis.KindjesNet.Blog.OldServices.Blog;
using Inferis.KindjesNet.Core.Managers;
using Inferis.KindjesNet.Core.Models;
using Inferis.KindjesNet.Core.Mvc;
using Inferis.KindjesNet.Core.Mvc.Controllers;
using Inferis.KindjesNet.Core.Utils;
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
        public IBlogImporter BlogImporter { get; set; }

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

            HighlightKids(post);
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
            BlogImporter.Import();


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