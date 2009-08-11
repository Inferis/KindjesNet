using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Hosting;

namespace Inferis.KindjesNet.Core
{
    public class ResourcedAspxProvider : VirtualPathProvider
    {
        public override bool FileExists(string virtualPath)
        {
            string resourcePath;
            Assembly source;

            return IsResourcedPath(virtualPath, out resourcePath, out source) 
                ? source.GetManifestResourceNames().Contains(resourcePath)
                : base.FileExists(virtualPath);
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            string resourcePath;
            Assembly source;

            return IsResourcedPath(virtualPath, out resourcePath, out source) 
                ? new ResourcedVirtualFile(virtualPath, resourcePath, source) 
                : base.GetFile(virtualPath);
        }

        public override bool DirectoryExists(string virtualDir)
        {
            string resourcePath;
            Assembly source;

            return IsResourcedPath(virtualDir, out resourcePath, out source)
                ? source.GetManifestResourceNames().Any(path => path.StartsWith(resourcePath))
                : base.DirectoryExists(virtualDir);
        }

        public override VirtualDirectory GetDirectory(string virtualDir)
        {
            string resourcePath;
            Assembly source;

            return IsResourcedPath(virtualDir, out resourcePath, out source)
                ? new ResourcedVirtualDirectory(virtualDir, resourcePath, source)
                : base.GetDirectory(virtualDir);
        }

        private static bool IsResourcedPath(string virtualPath, out string resourcePath, out Assembly source)
        {
            if (!virtualPath.Contains("/$") || !virtualPath.Contains("$/")) {
                resourcePath = "";
                source = null;
                return false;
            }

            var parts = virtualPath.Split('$');
            resourcePath = parts[1] + parts[2].Replace('/', '.');
            source = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == parts[1]);

            return source != null;
        }
    }
}
