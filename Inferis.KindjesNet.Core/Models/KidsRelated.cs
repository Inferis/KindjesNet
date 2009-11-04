using System.Collections.Generic;

namespace Inferis.KindjesNet.Core.Models
{
    public abstract class KidsRelated
    {
        protected KidsRelated()
        {
            Kids = new List<Kid>();
        }
        
        public virtual IList<Kid> Kids { get; private set; }
    }
}
