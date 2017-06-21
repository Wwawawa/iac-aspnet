using WebApplication1.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using System.IdentityModel.Services;
using System;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            var signIn = new SignInRequestMessage(
                new Uri("https://mysts/wsfed"),
                "http://myapp");
            //using below code, you can pick up some values from config, that you could provide some values dynamically.
            //FederatedAuthentication.FederationConfiguration.WsFederationConfiguration.
            ViewBag.SignInUrl = signIn.WriteQueryString();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }
        [Authorize]
        public ActionResult Identity()
        {
            var claims = Get();

            ViewBag.PrincipalType = ClaimsPrincipal.Current.GetType().FullName;
            ViewBag.IdentityType = ClaimsPrincipal.Current.Identity.GetType().FullName;

            return View(claims);
        }

        private IEnumerable<ViewClaim> Get()
        {
            var principal = ClaimsPrincipal.Current;

            var claims = new List<ViewClaim>(
                from c in ClaimsPrincipal.Current.Claims
                select new ViewClaim
                {
                    Type = c.Type,
                    Value = c.Value
                });

            return claims;
        }
    }
}
