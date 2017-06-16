## Authentication Sessions
#### Claims transformation is triggered on every request
* might be OK, but might be also expensive
#### Outcome of claims transformation can be „cached“ in a session
#### Pipe line
* First Request
```sh
    Authentication->Claims Transformation->cache principal->Application Code
```
* Subsequent Requests() 
```sh
    Authentication->load cache principal->Application Code
```
#### Session security token
* SessionSecurityToken is a serialization wrapper around a ClaimsPrincipal
  * read/write via a SessionSecurityTokenHandler
  ```cs
    var sessionToken = new SessionSecurityToken( principal, TimeSpan.FromHours(8));
  ```
#### Session Authentication Module
* HTTP module that provides a number of services
  *converting session tokens into cookies
  * automatically turning incoming cookies into a ClaimsPrincipal
  * processing pipeline
  * do this using add some code into web.config
  ```xml
    <modules runAllManagedModulesForAllRequests="true">
      <add name="SessionAuthenticationModule" type="...System.IdentityModel.Services.SessionAuthenticationModule" />
    </modules>
  ```
  * This module has some help methods:
  ```cs
    //turn the sessionToken into a cookie
    FederatedAuthentication.SessionAuthenticationModule .WriteSessionTokenToCookie(sessionToken);
    //sign out
    FederatedAuthentication.SessionAuthenticationModule .SignOut();
  ```
#### Session management is highly customizable
    * eventing pipeline
        * The session authentication module exposes a number of events to customize the standard behavior
            * SecurityTokenReceived: Allows modifying the token (cancellable)
            * SessionSecurityTokenCreated: Allows modifying the session details
            * SignedIn/SignedOut: Fire after successful sign in/sign out
            * SignOutError: Fire after failed sign in/sign out
    * [sliding expiration](https://github.com/Wwawawa/iac-aspnet/blob/master/Authentication-Sessions/Global.asax.cs)
        * By default an authentication session has a fixed expiration time
            * actually good to have an authentication token with a finite lifetime
        * But some scenarios require sliding expiration
        ```cs
        void SessionAuthenticationModule_SessionSecurityTokenReceived(object sender, SessionSecurityTokenReceivedEventArgs e)
        {
            // use e.SessionToken to inspect session details if (extendSession) 
            {
                var sam = sender as SessionAuthenticationModule;

                e.SessionToken = sam.CreateSessionSecurityToken(…);
                e.ReissueCookie = true;
            } 
        }
        ```
    * cookie handling / web farm support
        * Cookies are chunked when they reach a certain size (2 KB)
            * ChunkedCookieHandler (derived from CookieHandler)
        * Session token protection is done by the token handler
            * DPAPI and machine key based (extensible)
            * web farms need shared key material(similar with separate server authentication)
            ```xml
            <system.identityModel>
              <identityConfiguration>
                <securityTokenHandlers>
                  <remove type="… SessionSecurityTokenHandler, …" />
                  <add type="… MachineKeySessionSecurityTokenHandler, … " />
                </securityTokenHandlers>
              </identityConfiguration>
            </system.identityModel>
            ```
    * [server-side caching](https://github.com/Wwawawa/iac-aspnet/blob/master/Authentication-Sessions/Security/ClaimsTransformer.cs)
        * Session tokens can be also cached on the server side
            * only session token identifier gets serialized into a cookie
            * can reduce size on the wire substantially
            * needs server side (distributed) caching infrastructure
        * Set IsReference on the session token
        ```cs
        var sessionToken = new SessionSecurityToken(principal, TimeSpan.FromHours(8))
        {
            IsPersistent = false, // make persistent 
            IsReferenceMode = true // cache on server 
        };
        ```
    * Token Cache
        * By default an in-memory MRU cache is used
            * limited scalability & robustness
        * Can provide your own implementation by deriving from SessionSecurityTokenCache
        ```xml
        <system.identityModel>
          <identityConfiguration>
            <caches>
              <sessionSecurityTokenCache type="..."/>
            </caches>
          </identityConfiguration>
        </system.identityModel>
        ```
