using System.Collections.Generic;
using System.Linq;
using Inferis.KindjesNet.Core.Data;
using Inferis.KindjesNet.Core.Models;
using Microsoft.Practices.Unity;

namespace Inferis.KindjesNet.Core.Managers
{
    public class UserManager : IUserManager
    {
        [Dependency]
        public IRepository Repository { get; set; }

        public List<User> GetAllLegacyUsers()
        {
            return Repository.Query<User>().Where(u => u.LegacyId != null).ToList();
        }
    }
}
