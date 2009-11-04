using System;

namespace Inferis.KindjesNet.Core.Models
{
    [DataModel]
    public class User
    {
        public User()
        {
            Id = new Guid();
        }

        public virtual Guid Id { get; private set; }
        public virtual string Nick { get; set; }
        public virtual string Name { get; set; }
        public virtual int? LegacyId { get; set; }
    }
}