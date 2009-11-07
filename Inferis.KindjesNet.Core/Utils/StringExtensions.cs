namespace Inferis.KindjesNet.Core.Utils
{
    public static class StringExtensions
    {
        public static string ToUpperFirst(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return source;

            return string.Concat(
                source.Substring(0, 1).ToUpper(),
                source.Substring(1));
        }

        public static string ToLowerFirst(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return source;

            return string.Concat(
                source.Substring(0, 1).ToLower(),
                source.Substring(1));
        }

        public static string GetFirstWords(this string html)
        {
            return html.StripAllTags().Substring(0, 200);
        }

        public static string StripAllTags(this string html)
        {
            var array = new char[html.Length];
            var arrayIndex = 0;
            var inside = false;

            foreach (var let in html) {
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
