using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Inferis.KindjesNet.Core.Managers
{
    public class SlugGenerator : ISlugGenerator
    {
        public string GenerateSlug(string source)
        {
            var str = RemoveAccent(source).ToLower();

            str = Regex.Replace(str, @"[^a-z0-9\s-]", ""); // invalid chars           
            str = Regex.Replace(str, @"\s+", " ").Trim(); // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            str = Regex.Replace(str, @"--+", "-"); // combine hyphens   

            return str;
        }

        public string RemoveAccent(string txt)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }
    }
}
