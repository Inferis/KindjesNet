using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Hosting;

namespace Inferis.KindjesNet.Core
{
    public class ResourcedVirtualDirectory : VirtualDirectory
    {
        public ResourcedVirtualDirectory(string virtualPath, string resourcedPath, Assembly source) : base(virtualPath)
        {
        }

        public override IEnumerable Directories
        {
            get { return new object[] {}; }
        }

        public override IEnumerable Files
        {
            get { return new object[] { }; }
        }

        public override IEnumerable Children
        {
            get { return new object[] { }; }
        }
    }
}
