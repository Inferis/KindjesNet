using System;

namespace Inferis.KindjesNet.Core.Models
{
    public abstract class EntityWithId
    {
        protected EntityWithId()
        {
            Id = new Guid();
        }

        public virtual Guid Id { get; private set; }

        public override bool Equals(object obj)
        {
            var entity = obj as EntityWithId;
            if (entity == null) return false;
            return entity.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
