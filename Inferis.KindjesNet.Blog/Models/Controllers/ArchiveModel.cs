using System.Collections.Generic;

namespace Inferis.KindjesNet.Blog.Models.Controllers
{
    public class ArchiveModel
    {
        public ArchiveModel(int year, IEnumerable<Post> posts) : this(year, -1, posts)
        {
            
        }

        public ArchiveModel(int year, int month, IEnumerable<Post> posts)
            : this(year, month, -1, posts)
        {

        }

        public ArchiveModel(int year, int month, int day, IEnumerable<Post> posts)
        {
            Year = year;
            Month = month;
            Day = day;
            Posts = posts;
        }

        public int Year { get; private set; }
        public int Month { get; private set; }
        public int Day { get; private set; }
        public IEnumerable<Post> Posts { get; private set; }
    }
}
