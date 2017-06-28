/*
 * Copyright (c) Dominick Baier, Brock Allen.  All rights reserved.
 * see LICENSE
 */

using System;
using System.Collections.Generic;
using System.IdentityModel;
using System.IdentityModel.Configuration;
using System.IdentityModel.Protocols.WSTrust;
using System.Security.Claims;

namespace Thinktecture.IdentityModel.EmbeddedSts.WsFed
{
    class EmbeddedTokenService : SecurityTokenService
    {
        public EmbeddedTokenService(SecurityTokenServiceConfiguration config)
            : base(config)
        {
        }

        protected override Scope GetScope(ClaimsPrincipal principal, RequestSecurityToken request)
        {
            return new Scope(
                request.AppliesTo.Uri.AbsoluteUri,
                this.SecurityTokenServiceConfiguration.SigningCredentials)
                {
                    ReplyToAddress = request.ReplyTo,
                    TokenEncryptionRequired = false
                };
        }
        
        protected override ClaimsIdentity GetOutputClaimsIdentity(ClaimsPrincipal principal, RequestSecurityToken request, Scope scope)
        {
            //var id = new ClaimsIdentity(principal.Claims, "EmbeddedSTS");
            //return id;
            return EmbeddedTokenService.CreateClaimsIdentity(principal.Claims);
        }

        public static ClaimsIdentity CreateClaimsIdentity(IEnumerable<Claim> claims)
        {         
            ClaimsIdentity id = new ClaimsIdentity(claims, "EmbeddedSTS");
            string name = claims.GetValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name");
            if (name != null)
            {
                id.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifierTest", name));
            }
            id.AddClaim(new Claim("http://schemas.microsoft.com/test1", DateTime.UtcNow.ToString("o")));
            id.AddClaim(new Claim("http://schemas.microsoft.com/test2", "1234"));
            return id;
        }

        //can add extention methods for variable which is declared by IEnumerable<string> as below
        public void IEnumerableTest(IEnumerable<string> testp)
        {
            var test1 = testp.test();
        }
    }
}
