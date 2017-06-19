using System.Web;
using System.Web.Mvc;
using Thinktecture.IdentityModel.Authorization.Mvc;

namespace ClaimsBasedAuthorization
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            // invoke the ClaimsAuthenticationManager and call the customized method at the node of claimsAuthorizationManager configured in the web.config
            filters.Add(new ClaimsAuthorizeAttribute());
        }
    }
}
