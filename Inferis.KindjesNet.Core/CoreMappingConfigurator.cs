using System.ComponentModel.Composition;
using Inferis.KindjesNet.Core.Models;
using Inferis.KindjesNet.Core.Plugins;

namespace Inferis.KindjesNet.Core
{
    [Export(typeof(IMappingConfigurator))]
    public class CoreMappingConfigurator : DataModelMappingConfigurator
    {
    }
}