using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inferis.KindjesNet.Web.Models
{
    public class UserProfileModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
