using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Inferis.KindjesNet.Core.Models;

namespace Inferis.KindjesNet.Core.Mvc.Markup
{
    public static class KidsHtmlExtensions
    {
        public static string GenerateKidsLabels(this HtmlHelper html)
        {
            var kids = html.ViewData["Kids"] as IEnumerable<Kid>;
            if (kids == null) 
                return "";

            var hightlightedKids = html.ViewData["HighlightedKids"] as IEnumerable<Kid> ?? new List<Kid>();

            var result = new StringBuilder();
            result.Append(@"<ul class=""kids-labels"">");
            foreach (var kid in kids) {
                result.Append(@"<li class=""label-");
                result.Append(kid.Tag);
                result.Append(@""">");
                result.Append(html.ActionLink(kid.Name, kid.Tag, "kids", null, new { Class = hightlightedKids.Any(k => k.Tag == kid.Tag) ? "hl" : ""}));
                result.Append(@"</li>");
            }
            result.Append(@"</ul>");
            return result.ToString();
        }
    }
}
