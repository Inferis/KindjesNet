using System;

namespace Inferis.KindjesNet.Core
{
    public static class DateExtensions
    {
        public static string FormatForUrl(this DateTime date)
        {
            return string.Format("{0:yyyy}/{0:MM}/{0:dd}", date);
        }
    }
}