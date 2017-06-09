
#### The ASP.NET Pipeline - What to do on the step
* BeginRequest - Request(login page)
* AuthenticateRequest - Check cookie - Set Principal
* PostAuthenticateRequest
* AuthorizeRequest
* ExecuteHandler - Resource Rendering
* EndRequest - Redirect to login page - Response
#### Benefits & Scenarios
* Applications not always backed by Active Directory user store
  * maybe not even connected to AD
  * accounts not stored in AD
    * e.g. SQL database
* Allows providing your own login UI
  * but requires to implement your own user management
  * Membership and Role Manager may be useful
* Simple mechanism
  * redirect to login page on „access denied“
  * simple cookie as authentication token
  * ...but also limited

### Deployment methods
#### 1-Configuration(old)
* Allow anonymous connections
```xml
<system.webServer>
  <security>
    <authentication>
      <anonymousAuthentication enabled="true" />
    </authentication>
  </security>
</system.webServer>
```
* Enable in <system.web />
```xml
<authentication mode="Forms">
  <forms loginUrl="~/Account/Login"
  timeout="2880" />
</authentication>
```
#### 2-[OWIN](http://owin.org/)
* MVC Individual Account Authenticaiton default deployment using owin UseCookieAuthentication
#### 3-This project build based on windows authentication project using old authentication logic
