using AuthenticationDemo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace AuthenticationDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

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
