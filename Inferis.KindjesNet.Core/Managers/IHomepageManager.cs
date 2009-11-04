using System.Collections.Generic;
using Inferis.KindjesNet.Core.Models;

namespace Inferis.KindjesNet.Core.Managers
{
    public interface IHomepageManager
    {
        List<IFeedItem> GetFeedItems();
    }
}
