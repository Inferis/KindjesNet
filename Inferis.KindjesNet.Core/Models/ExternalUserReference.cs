using System;
using System.Collections.Generic;

namespace Inferis.KindjesNet.Core.Models
{
    [DataModel]
    public class ExternalUserReference : EntityWithId
    {
        public virtual User User { get; set; }
        public virtual string Type { get; set; }
        public virtual string Value { get; set; }
    }
}