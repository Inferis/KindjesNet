using System.Collections.Generic;
using Inferis.KindjesNet.Core.Models;

namespace Inferis.KindjesNet.Core.Managers
{
    public interface ISpiderManager {
        IEnumerable<ISpider> Spiders { get; }
        ISpider FindSpider(string name);
        string GetSpiderName(ISpider spider);
    }
}