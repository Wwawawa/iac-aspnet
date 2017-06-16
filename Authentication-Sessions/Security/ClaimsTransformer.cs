using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Security;

namespace Web.Security
{
    public class ClaimsTransformer : ClaimsAuthenticationManager
    {
        public override ClaimsPrincipal Authenticate(string resourceName, ClaimsPrincipal incomingPrincipal)
        {
            if (!incomingPrincipal.Identity.IsAuthenticated)
            {
                return base.Authenticate(resourceName, incomingPrincipal);
            }

            var principal= CreatePrincipal(incomingPrincipal.Identity.Name);

            EstablishSession(principal);

            return principal;
        }
        private void EstablishSession(ClaimsPrincipal principal)
        {
            var sessionToken = new SessionSecurityToken(principal, TimeSpan.FromHours(8));
            
            // cache on server
            //sessionToken.IsReferenceMode = true;
            
            FederatedAuthentication.SessionAuthenticationModule.WriteSessionTokenToCookie(sessionToken);
        }
        private ClaimsPrincipal CreatePrincipal(string userName)
        {
            var claims = new List<Claim>();
            if (userName == "dom")
            {
                claims.Add(new Claim(ClaimTypes.Name, userName));
                claims.Add(new Claim(ClaimTypes.GivenName, "Dominick"));

                Roles.GetRolesForUser(userName).ToList().ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Name, userName));
                claims.Add(new Claim(ClaimTypes.GivenName, userName));
            }
            return new ClaimsPrincipal(new ClaimsIdentity(claims, "Custom"));
        }
    }
}
