using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Inferis.KindjesNet.Blog.Models;

namespace Inferis.KindjesNet.Blog.Managers
{
    public interface IBlogManager
    {
        /// <summary>
        /// Returns a post by it's legacy id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Post GetPostByLegacyId(int id);

        /// <summary>
        /// Gets a post by year, month, day and slug.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="slug"></param>
        /// <returns></returns>
        Post GetPost(int year, int month, int day, string slug);

        /// <summary>
        /// Gets all posts by year, month and day.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        List<Post> GetPosts(int year, int month, int day);

        /// <summary>
        /// Gets all posts by year and month.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        List<Post> GetPosts(int year, int month);

        /// <summary>
        /// Gets all posts by year.
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        List<Post> GetPosts(int year);

        /// <summary>
        /// Saves a post in the DB.
        /// </summary>
        /// <param name="post"></param>
        void SavePost(Post post);

        /// <summary>
        /// Creates a slug from a string.
        /// Does check for unique slugs for a specific date.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="checkDate"></param>
        /// <param name="guid"></param>
        /// <returns></returns>
        string Slugify(string source, DateTime checkDate, Guid? ignorePostId);

        /// <summary>
        /// Searches blogposts.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        List<Post> Search(string query);

        /// <summary>
        /// Returns the most recent X posts.
        /// </summary>
        /// <param name="maxPosts"></param>
        /// <returns></returns>
        List<Post> GetMostRecentPosts(int maxPosts);
    }
}
