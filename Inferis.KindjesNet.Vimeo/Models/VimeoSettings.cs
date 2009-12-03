using Inferis.KindjesNet.Core;
using Inferis.KindjesNet.Core.Models;

namespace Inferis.KindjesNet.Vimeo.Models
{
    [DataModel]
    public class VimeoSettings : EntityWithId
    {
        public virtual string ConsumerKey { get; set; }
        public virtual string ConsumerSecret { get; set; }
        public virtual string AuthToken { get; set; }
        public virtual string AuthSecret { get; set; }
        public virtual string UserId { get; set; }
    }
}
