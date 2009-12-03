using System.Web.Mvc;

namespace Inferis.KindjesNet.Core.Security
{
    public class KindjesNetAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            return httpContext.User.Identity.IsAuthenticated;
        }
    }
}