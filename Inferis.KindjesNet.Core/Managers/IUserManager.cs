using System.Collections.Generic;
using Inferis.KindjesNet.Core.Models;

namespace Inferis.KindjesNet.Core.Managers
{
    public interface IUserManager {
        List<User> GetAllLegacyUsers();
    }
}