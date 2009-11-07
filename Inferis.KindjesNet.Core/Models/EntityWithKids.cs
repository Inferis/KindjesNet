using System.Collections.Generic;

namespace Inferis.KindjesNet.Core.Models
{
    public abstract class EntityWithKids : EntityWithId
    {
        protected EntityWithKids()
        {
            Kids = new List<Kid>();
        }
        
        public virtual IList<Kid> Kids { get; private set; }
    }

}
