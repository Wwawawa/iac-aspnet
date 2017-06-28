using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Thinktecture.IdentityModel.EmbeddedSts
{
    internal static class ClaimsExtensions
    {
        public static bool HasClaim(this ClaimsPrincipal user, string claimType)
        {
            if (user != null)
            {
                return user.HasClaim((Claim x) => x.Type == claimType);
            }
            return false;
        }

        public static string GetValue(this IEnumerable<Claim> claims, string claimType)
        {
            if (claims != null)
            {
                Claim claim = claims.SingleOrDefault((Claim x) => x.Type == claimType);
                if (claim != null)
                {
                    return claim.Value;
                }
            }
            return null;
        }
        public static IEnumerable<string> GetValues(this IEnumerable<Claim> claims, string claimType)
        {
            if (claims == null)
            {
                return Enumerable.Empty<string>();
            }
            return from claim in claims
                   where claim.Type == claimType
                   select claim.Value;
        }

        public static string test(this IEnumerable<string> a)
        {
            return "";
        }
    }
}
