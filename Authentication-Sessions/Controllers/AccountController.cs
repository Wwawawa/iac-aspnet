using WebApplication1.Models;
using System.IdentityModel.Services;
using System.Security.Claims;
using System.Web.Mvc;
using System.Web.Security;
using System.Collections.Generic;

namespace WebApplication1.Controllers
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

                //roleManager and membership
                var success = Membership.ValidateUser(model.UserName, model.Password);

                if (success)
                {
                    // set authentication cookie
                    //FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.UserName)
                    };
                    var principal=new ClaimsPrincipal(new ClaimsIdentity(claims, "Forms"));
                    
                    #region //this piece code may be another way to add claims
                    //SessionSecurityToken token = new SessionSecurityToken(principal);
                    //if (FederatedAuthentication.FederationConfiguration.IdentityConfiguration.SaveBootstrapContext)
                    //{
                    //    id.BootstrapContext = this.CreateBootStrapContext(claims);
                    //}
		    //SessionAuthenticationModule sam = FederatedAuthentication.SessionAuthenticationModule;
		    //sam.WriteSessionTokenToCookie(token);
		    //return this.Redirect("~/");
                    #endregion

                    // need to config the ClaimsAuthenticationManager in the web.config, can call our customer ClaimsAuthenticationManager that is Web.Security.ClaimsTransformer method.
                    var transformer =FederatedAuthentication.FederationConfiguration
                                           .IdentityConfiguration
                                           .ClaimsAuthenticationManager;
                    transformer.Authenticate(string.Empty, principal);
                    // 
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
        
        public BootstrapContext CreateBootStrapContext(IEnumerable<Claim> claims)
	{
		ClaimsIdentity id = new ClaimsIdentity(claims);
		SamlSecurityToken bootStrapToken = TokenHelper.CreateSamlToken(id, FederatedAuthentication.FederationConfiguration.WsFederationConfiguration.Realm);
		return new BootstrapContext(bootStrapToken, FederatedAuthentication.FederationConfiguration.IdentityConfiguration.SecurityTokenHandlers[bootStrapToken.GetType()]);
	}
	public ActionResult SignOut()
	{
		SessionAuthenticationModule sam = FederatedAuthentication.SessionAuthenticationModule;
		sam.SignOut();
		return this.Redirect("~/");
	}	
    }
}
