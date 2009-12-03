using System;
using System.Collections.Generic;
using Inferis.Core.Helpers;

namespace Inferis.KindjesNet.Core.Models
{
    [DataModel]
    public class User : EntityWithId
    {
        public User()
        {
            ExternalReferences = new List<ExternalUserReference>();
        }

        public virtual string Nick { get; set; }
        public virtual string Name { get; set; }
        public virtual int? LegacyId { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }

        public virtual void AddExternalReference(ExternalUserReference reference)
        {
            Guard.ArgumentNotNull(x => reference);

            if (!ExternalReferences.Contains(reference))
                ExternalReferences.Add(reference);
            if (reference.User != null)
                reference.User.RemoveExternalReference(reference);
            reference.User = this;
        }

        public virtual void RemoveExternalReference(ExternalUserReference reference)
        {
            if (ExternalReferences.Contains(reference))
                ExternalReferences.Remove(reference);
            reference.User = null;
        }

        public virtual IList<ExternalUserReference> ExternalReferences { get; set; }
    }
}