using System.Collections.Generic;
using Inferis.KindjesNet.Core.Models;

namespace Inferis.KindjesNet.Core.Managers
{
    public interface IUserManager {
        List<User> GetAllLegacyUsers();
        User GetUserByCredentials(string email, string password);
        User GetUserByEmail(string email);
        User GetUserByExternalReference(string referenceName, string referenceValue);
        void Update(User user);
        User FuzzyFind(string reference);
    }
}