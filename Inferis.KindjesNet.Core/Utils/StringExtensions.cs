using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Inferis.KindjesNet.Core.Utils
{
    public static class StringExtensions
    {
        public static string GetFirstWords(this string html)
        {
            return html.StripAllTags().Substring(0, 200);
        }

        public static string StripAllTags(this string html)
        {
            var array = new char[html.Length];
            var arrayIndex = 0;
            var inside = false;

            for (var i = 0; i < html.Length; i++) {
                var let = html[i];
                if (let == '<') {
                    inside = true;
                    continue;
                }
                if (let == '>') {
                    inside = false;
                    continue;
                }
                if (inside) continue;
                array[arrayIndex] = let;
                arrayIndex++;
            }
            return new string(array, 0, arrayIndex);
        }
    }
}
