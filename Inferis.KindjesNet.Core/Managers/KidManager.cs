using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inferis.KindjesNet.Core.Data;
using Inferis.KindjesNet.Core.Models;
using Microsoft.Practices.Unity;

namespace Inferis.KindjesNet.Core.Managers
{
    public class KidManager : IKidManager
    {
        [Dependency]
        public IRepository Repository { get; set; }

        public List<Kid> GetAll()
        {
            return Repository.Query<Kid>().OrderBy(k => k.Birthdate).ToList();
        }

        public Kid GetKidByTag(string tag)
        {
            return Repository.Query<Kid>().FirstOrDefault(k => k.Tag == tag);
        }
    }
}
