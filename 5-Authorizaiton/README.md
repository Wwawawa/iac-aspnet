## Two Approach
* Pipeline Authorization
  * ClaimsAuthenticationManager: reject request based on missing identity information
  * URL authorization module: common and old approach
    * Authenticated users only can access config
    ```xml
    <location path="customers"><!--this customers means you need to give a authorization method path, this is a customization, but this will have a big operation, because you might change that due to some reasons-->
      <system.web>
        <authorization>
          <deny users="?" />
        </authorization>
      </system.web>
    </location>
    ```
    * roles allow
      * Users in role ‚'Marketing' only config:
      ```xml
      <location path="customers"><!--this customers means you need to give a authorization method path, this is a customization, but this will have a big operation, because you might change that due to some reasons-->
        <system.web>
          <authorization>
            <allow roles="Marketing" />
            <deny users="*" />
          </authorization>
        </system.web>
      </location>
      ```
  * Claims authorization module: base on claimsAuthorizationManager(check that in the [ClaimsTransformation](https://github.com/Wwawawa/iac-aspnet/tree/master/ClaimsTransformation))
    * claims-based
    * still uses the URL
    * ClaimsAuthenticationManager: reject request based on missing identity information, so how to work, --todo... 
    ```xml
    <modules runAllManagedModulesForAllRequests="true">
      <add name="ClaimsAuthorizationModule" type="System.IdentityModel.Services.ClaimsAuthorizationModule, ... " />
    </modules>
    ```
    ```xml
    <system.identityModel>
      <identityConfiguration>
        <claimsAuthorizationManager type="AuthorizationManager, ..." />
      </identityConfiguration>
    </system.identityModel>
    ```
* Intra-app Authorization
  * ClaimsPrincipalPermission
    * Ships with the .NET Framework (since version 4.5)
      * same underlying implementation as PrincipalPermission
      * based on ClaimsAuthorizationManager
      ```cs
      [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "Customer", Operation = "Add")]
      public ActionResult AddCustomer() 
      { ... }
      ```
  * MVC Authorize Attribute
    * Uses the MVC security pipeline
      * does not get invoked in unit testing
      * plays nice with post processing (no exceptions)
      * limited to roles
      ```cs
      [Authorize] 
      public ActionResult AddCustomer() 
      { ... }
      ```
      ```cs
      [Authorize(Roles = "Sales")] 
      public ActionResult AddCustomer() 
      { ... }
      ```
  * MVC ClaimsAuthorize Attribute
    * using Thinktecture.IdentityModel
      * uses ClaimsAuthorizationManager
      * same attribute exists also for Web API
      ```cs
      [ClaimsAuthorize("Add", "Customer")] 
      public ActionResult AddCustomer() 
      { ... }
      ```
  * Imperative
    * For authorization from within your code
      * has the most intimate knowledge of the current operation
      * can reach out to registered authorization manager using the FederatedAuthentication class
      * Thinktecture.IdentityModel has an easy to use wrapper as below code:
      ```cs
      public string Get(int id)
      {
          var allowed = ClaimsAuthorization.CheckAccess("Get", "Customer", id.ToString());
          if (allowed) { ... }
      }
      ```      
