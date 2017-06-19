using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Thinktecture.IdentityModel.Authorization.WebApi;

namespace ClaimsBasedAuthorization
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // add global authorization filter
            // invoke the ClaimsAuthenticationManager and call the customized method at the node of claimsAuthorizationManager configured in the web.config
            config.Filters.Add(new ClaimsAuthorizeAttribute());
        }
    }
}
