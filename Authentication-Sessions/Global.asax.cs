using System;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Web.Security;

namespace WebApplication1
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            var config = new HttpConfiguration();
            //WebApiConfig.Register(config);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        // manual way of invoking claims transformation
        //protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        //{
        //    var transformer = new ClaimsTransformer();
        //    var principal = transformer.Authenticate(string.Empty, ClaimsPrincipal.Current);

        //    Thread.CurrentPrincipal = principal;
        //    HttpContext.Current.User = principal;
        //}
        
        // sliding expiration

        //void SessionAuthenticationModule_SessionSecurityTokenReceived(object sender, SessionSecurityTokenReceivedEventArgs e)
        //{ 
        //    // use e.SessionToken to inspect session details 
        //    if (extendSession)
        //    {
        //        var sam = sender as SessionAuthenticationModule;
        //        e.SessionToken = sam.CreateSessionSecurityToken(…);
        //        e.ReissueCookie = true; } 
        //}
    }
}
