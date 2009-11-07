using System;

namespace Inferis.KindjesNet.Core.Models
{
    [DataModel]
    public class User : EntityWithId
    {
        public virtual string Nick { get; set; }
        public virtual string Name { get; set; }
        public virtual int? LegacyId { get; set; }
    }
}