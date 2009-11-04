using System;

namespace Inferis.KindjesNet.Core.Models
{
    [DataModel]
    public class Kid
    {
        public Kid()
        {
            Id = new Guid();
        }

        public virtual Guid Id { get; private set; }
        public virtual string Name { get; set; }
        public virtual string Tag { get; set; }
        public virtual DateTime Birthdate { get; set; }
    }
}