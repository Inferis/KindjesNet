using Microsoft.Practices.ObjectBuilder2;

namespace Inferis.KindjesNet.Core.Unity
{
    public class DeepBuildupStrategy : BuilderStrategy
    {
        public override void PreBuildUp(IBuilderContext context)
        {
            var key = context.BuildKey as IBuildKey;
            if (key != null && context.Existing != null && 
                context.Existing.GetType() != key.Type &&
                key.Type.IsAssignableFrom(context.Existing.GetType())) {
                var policy = context.Policies.Get<IDeepBuildupPolicy>(key.Type);
                if (policy != null) {
                    context.BuildKey = key.ReplaceType(context.Existing.GetType());
                }
            }

            base.PostBuildUp(context);
        }
    }
}
