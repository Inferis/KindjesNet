using System.Web.Mvc;
using Inferis.KindjesNet.Core.Models;
using Inferis.KindjesNet.Core.Unity;
using Microsoft.Practices.Unity;

namespace Inferis.KindjesNet.Web
{
    public static class UnityExtensions
    {
        public static TUnity Initialize<TUnity>(this TUnity container)
            where TUnity : IUnityContainer
        {
            container.RegisterInstance<IUnityContainer>(container);
            container.AddNewExtension<DeepBuildup>();
            container.Configure<DeepBuildup>()
                .Mark<IController>()
                .Mark<ISpider>();

            return container;
        }
    }
}
