using System;

namespace Inferis.KindjesNet.Core.Models
{
    [DataModel]
    public class Kid : EntityWithId
    {
        public virtual string Name { get; set; }
        public virtual string Tag { get; set; }
        public virtual DateTime Birthdate { get; set; }
    }
}