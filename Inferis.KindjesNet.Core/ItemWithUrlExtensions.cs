using System.Linq;
using Inferis.KindjesNet.Core.Models;

namespace Inferis.KindjesNet.Core
{
    public static class ItemWithUrlExtensions
    {
        public static string GenerateUrl(this IItemWithUrl item)
        {
            var type = item.GetType().Name.ToLower();
            var attr = (ItemWithUrlTypeAttribute)item
                .GetType()
                .GetCustomAttributes(typeof(ItemWithUrlTypeAttribute), true).FirstOrDefault();
            if (attr != null)
                type = attr.Type;
            return string.Concat(item.DateForUrl.FormatForUrl(), "/", type, "/", item.Slug);
        }
    }
}
