using System.Collections.Generic;

namespace Inferis.KindjesNet.Blog.Managers
{
    public interface IBlogImporter
    {
        IEnumerable<string> Import();
    }
}
