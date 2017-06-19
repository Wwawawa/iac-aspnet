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
    * Other invoke ways(maybe upon second points example): we can add below config node in the web.config, and invoke that in code, for this refer to [Authentication-Sessions project](https://github.com/Wwawawa/iac-aspnet/tree/master/Authentication-Sessions) and [Authorizaiton project](https://github.com/Wwawawa/iac-aspnet/tree/master/5-Authorizaiton) .etc
    ```sh
    <system.identityModel>
    <identityConfiguration>
      <!--call custom Authorization class 'ClaimsBasedAuthorization.AuthorizationManager'-->
      <claimsAuthorizationManager type="ClaimsBasedAuthorization.AuthorizationManager, ClaimsBasedAuthorization" />
    </identityConfiguration>
    </system.identityModel>
    ```
#### Demo(base on form authenticaiton project)
* Add a custom 'ClaimsTransformer' function override the Authenticate
* Add remove module for roleManager into Web.config, because we only use it for roles store
```sh
    <modules runAllManagedModulesForAllRequests="true">
      <remove name="RoleManager" />
    </modules>
```
* Add 'Application_PostAuthenticateRequest' function into global.asax.cs to make Claims Transformation manually in PostAuthenticateRequest
* Change the show user name on the page logic in the _Layout.cshtml
```cshtml
    @if (User.Identity.IsAuthenticated)
    {
        <text>Hello, </text>
        <span class="username">
            @System.Security.Claims.ClaimsPrincipal.Current.FindFirst(System.IdentityModel.Claims.ClaimTypes.GivenName).Value
        </span>
    }
```
