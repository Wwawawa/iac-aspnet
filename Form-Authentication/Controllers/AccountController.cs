using AuthenticationDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AuthenticationDemo.Controllers
{
    public class AccountController : Controller
    {
        //
        // POST: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            // check if all required fields are set
            if (ModelState.IsValid)
            {
                // authenticate user
                //var success = ValidateUser(model.UserName, model.Password);
                
                // using roleManager and membership block authentication
                var success = Membership.ValidateUser(model.UserName, model.Password);

                if (success)
                {
                    // set authentication cookie
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);

                    return RedirectToLocal(returnUrl);
                }
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        private bool ValidateUser(string userName, string password)
        {
            return (userName == password);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
