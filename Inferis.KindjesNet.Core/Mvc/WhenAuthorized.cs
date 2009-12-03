using System;
using System.Reflection;
using System.Web.Mvc;

namespace Inferis.KindjesNet.Core.Mvc
{
    public class WhenAuthorizedAttribute : ActionMethodSelectorAttribute
    {
        private Type authorizeType;

        public WhenAuthorizedAttribute(Type authorizeType)
        {
            if (authorizeType == null)
                throw new ArgumentNullException("authorizeType");

            if (!typeof(IAuthorizationFilter).IsAssignableFrom(authorizeType))
                throw new ArgumentException("The authorizeType passed should implement " + typeof(IAuthorizationFilter).FullName, "authorizeType");

            this.authorizeType = authorizeType;
        }

        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            var filter = (IAuthorizationFilter)Activator.CreateInstance(authorizeType);
            var filterContext = new AuthorizationContext(controllerContext);
            filter.OnAuthorization(filterContext);
            return filterContext.Result == null;
        }
    }
}
