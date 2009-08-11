using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Hosting;

namespace Inferis.KindjesNet.Core
{
    public class ResourcedVirtualFile : VirtualFile
    {
        private readonly Assembly source;
        private readonly string resourcePath; 

        public ResourcedVirtualFile(string virtualPath, string resourcePath, Assembly source) : base(virtualPath)
        {
            this.source = source;
            this.resourcePath = resourcePath;
        }

        public override Stream Open()
        {
            return source.GetManifestResourceStream(resourcePath);
        }
    }
}
