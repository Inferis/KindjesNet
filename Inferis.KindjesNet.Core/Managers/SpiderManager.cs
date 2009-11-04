using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Inferis.KindjesNet.Core.Models;

namespace Inferis.KindjesNet.Core.Managers
{
    public class SpiderManager : ISpiderManager
    {
        [ImportMany(AllowRecomposition = true)]
        public IEnumerable<ISpider> Spiders { get; set; }

        public ISpider FindSpider(string name)
        {
            return Spiders.FirstOrDefault(s => GetSpiderName(s) == name);
        }

        public string GetSpiderName(ISpider spider)
        {
            return spider.GetType().Name;
        }
    }
}
