## Claims Transformation & Validation
#### Abstraction layer between authentication process and application code
```sh
    Authentication->Claims Transformation->Application Code
```
* to validate incoming identity data
* allows adding application specific claims to the principal
#### Framework provided class to do transformation & validation, we can customer that
```cs
    public class ClaimsTransformer : ClaimsAuthenticationManager
    {
        public override ClaimsPrincipal Authenticate(string resourceName, ClaimsPrincipal incomingPrincipal)
        {
            if (incomingPrincipal.Identity.IsAuthenticated)
            {
                return TranformClaims(incomingPrincipal);
            }
            return incomingPrincipal;
        }
    }

```
#### Enabling Claims Transformation
* ClaimsAuthenticationManager needs to be called after the authentication stage
    * either manually in PostAuthenticateRequest
    * using an HTTP module
        * Thinktecture.IdentityModel has one
        * WS-Federation plumbing does that automatically
