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
