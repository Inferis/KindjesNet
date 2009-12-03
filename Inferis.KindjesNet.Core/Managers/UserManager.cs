using System;
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

        public User GetUserByCredentials(string email, string password)
        {
            return Repository.Query<User>().FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public User GetUserByEmail(string email)
        {
            return Repository.Query<User>().FirstOrDefault(u => u.Email == email);
        }

        public User GetUserByExternalReference(string referenceType, string referenceValue)
        {
            return Repository.Query<User>().FirstOrDefault(u => u.ExternalReferences.Any(ur => ur.Type == referenceType && ur.Value == referenceValue));
        }

        public void Update(User user)
        {
            Repository.SaveOrUpdate(user);
        }

        public User FuzzyFind(string reference)
        {
            if (string.IsNullOrEmpty(reference))
                return null;

            reference = reference.ToLower();
            return Repository.Query<User>()
                .FirstOrDefault(u => u.Nick == reference);
        }
    }
}
