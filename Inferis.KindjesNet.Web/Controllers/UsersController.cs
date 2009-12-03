using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Inferis.KindjesNet.Core.Managers;
using Inferis.KindjesNet.Core.Models;
using Inferis.KindjesNet.Core.Mvc;
using Inferis.KindjesNet.Core.Mvc.Controllers;
using Inferis.KindjesNet.Core.Plugins;
using Inferis.KindjesNet.Core.Security;
using Inferis.KindjesNet.Web.Models;
using Microsoft.Practices.Unity;

namespace Inferis.KindjesNet.Web.Controllers
{
    public class UsersController : ControllerWithKids
    {
        [Dependency]
        public IUserManager UserManager { get; set; }

        protected override void HandleUnknownAction(string actionName)
        {
            Profile(actionName).ExecuteResult(ControllerContext); 
        }

        public ActionResult Profile(string id)
        {
            if (string.IsNullOrEmpty(id))
                id = HttpContext.User.Identity.Name;
            var users = UserManager.FuzzyFind(id);

            if (users == null) 
                return View("Unknown");

            return View("Profile", new UserProfileModel() {
                Email = users.Email,
                Name = users.Name,
                Id = users.Id
            });
        }

        [ActionName("logout")]
        public ActionResult UnauthorizedLogout()
        {
            return Redirect("~/");
        }

        [ActionName("logout"), WhenAuthorized(typeof(AuthorizeAttribute))]
        public ActionResult AuthorizedLogout()
        {
            var redirect = RedirectToAction("Profile", new { id = HttpContext.User.Identity.Name });

            FormsAuthentication.SignOut();
            Facebook.Logout();

            return redirect;
        }
        //[ActionName("entrance")]
        //public ActionResult EntranceWhenUnauthorized()
        //{
        //    if (Facebook.IsConnected()) {
        //        var fbuserid = Facebook.Api.Users.GetLoggedInUser();
        //        var user = UserManager.GetUserByExternalReference("facebook", fbuserid.ToString());
        //        if (user == null) {
        //            // create a user
        //            var fbuser = Facebook.Api.Users.GetInfo();
        //            user = new User() {
        //                Email = fbuser.proxied_email,
        //                Name = fbuser.name,
        //            };
        //            user.AddExternalReference(new ExternalUserReference() { Type = "facebook", Value = fbuserid.ToString() });
        //            UserManager.Update(user);
        //        }

        //        FormsAuthentication.SetAuthCookie(user.Email, false);
        //        return RedirectToAction("Index");
        //    }

        //    return View();
        //}

        //[ActionName("entrance"), AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult EntranceWhenUnauthorizedPost(string email, string password)
        //{
        //    var user = UserManager.GetUserByCredentials(email, password);
        //    if (user != null) {
        //        FormsAuthentication.SetAuthCookie(user.Email, false);
        //        return RedirectToAction("Index");
        //    }

        //    return View();
        //}

        //[ActionName("entrance"), WhenAuthorized(typeof(AuthorizeAttribute))]
        //public ActionResult EntranceWhenAuthorized()
        //{
        //    return RedirectToAction("Index");
        //}
    }
}
