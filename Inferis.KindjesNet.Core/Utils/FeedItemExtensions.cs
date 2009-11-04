using Inferis.KindjesNet.Core.Models;

namespace Inferis.KindjesNet.Core.Utils
{
    public static class FeedItemExtensions
    {
        public static string GenerateHtml(this IFeedItem item)
        {
            return string.Format(@"<div class='feeditem'>
                <p class='content'>
                    <div class='title'><span class='fluo {0}'><span><a href='{2}'>{1}</a></span></span></div>
                    <img src='/KindjesWeb/Content/img/dummy.jpg' class='thumbnail'>
                    {3}
                </p>
                <p class='actions'>
                    <span class='info'>
                        <a href='' class='icon {5}' title='{4}'></a> 
                        <a href='' class='date'>10 uur geleden</a> op <a href='' class='via'>{4}</a>
                    </span>
                    - <a href=''>reageer</a> - <a href='' class='thup'>is tof!</a>
                </p>
            </div>",
                   item.Color,
                   item.Title,
                   item.Url,
                   item.Body,
                   item.Provider,
                   item.Icon);
        }
    }
}
