using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.ObjectBuilder;

namespace Inferis.KindjesNet.Core.Unity
{
    public class DeepBuildup : UnityContainerExtension, IUnityContainerExtensionConfigurator
    {
        protected override void Initialize()
        {
            Context.Strategies.AddNew<DeepBuildupStrategy>(UnityBuildStage.TypeMapping);
        }

        public DeepBuildup Mark<T>()
        {
            Context.Policies.Set<IDeepBuildupPolicy>(new DeepBuildupPolicy(), typeof(T));
            return this;
        }
    }

}
