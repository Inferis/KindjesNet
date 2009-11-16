using Inferis.Core.Extensions;

namespace Inferis.KindjesNet.Core.Utils
{
    public static class StringExtensions
    {
        public static string GetFirstWords(this string html)
        {
            return html.StripAllTags().Substring(0, 200);
        }
    }
}
