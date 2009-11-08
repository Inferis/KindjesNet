using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Inferis.KindjesNet.Blog.Models;
using Inferis.KindjesNet.Blog.OldServices.Blog;
using Inferis.KindjesNet.Core.Managers;
using Inferis.KindjesNet.Core.Models;
using Microsoft.Practices.Unity;

namespace Inferis.KindjesNet.Blog.Managers
{
    public class BlogImporter : IBlogImporter
    {
        [Dependency]
        public IBlogManager BlogManager { get; set; }

        [Dependency]
        public IKidManager KidManager { get; set; }

        [Dependency]
        public IUserManager UserManager { get; set; }

        public IEnumerable<string> Import()
        {
            var users = new Dictionary<int, User>();
            foreach (var user in UserManager.GetAllLegacyUsers()) {
                users[(int)user.LegacyId] = user;
            }

            var kids = KidManager.GetAll();

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

                        post.Kids.Clear();
                        foreach (var kid in kids.Where(k => k.Birthdate < post.PostDate)) {
                            if (Regex.Match(post.Body, kid.Tag, RegexOptions.IgnoreCase).Success) {
                                post.Kids.Add(kid);
                            }
                        }
                        if (!post.Kids.Any()) {
                            foreach (var kid in kids.Where(k => k.Birthdate < post.PostDate)) {
                                post.Kids.Add(kid);
                            }
                        }

                        BlogManager.SavePost(post);
                    }
                }
                catch {
                    client.Abort();
                }
            }

            return null;
        }
    }
}
