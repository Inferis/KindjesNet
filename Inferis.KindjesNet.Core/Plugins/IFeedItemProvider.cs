using System.Collections.Generic;
using Inferis.KindjesNet.Core.Models;

namespace Inferis.KindjesNet.Core.Plugins
{
    public interface IFeedItemProvider
    {
        IEnumerable<IFeedItem> GetItems(int maxItems);
    }
}
