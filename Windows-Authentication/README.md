#### The ASP.NET Pipeline - what to do in the step
* BeginRequest - Request
* AuthenticateRequest - Copy IIS generated WindowsIdentity - Set Principal
* PostAuthenticateRequest
* AuthorizeRequest
* ExecuteHandler - Resource Rendering
* EndRequest - Response
#### Benefits & Scenarios
* Typical choice for intranet applications
* Heavy lifting is done by the infrastructure
  * Active Directory
  * Operating system
  * IIS + ASP.NET
* Allows access to Windows user name and groups
  *additional claims with Windows Server 2012
#### Configuration
* Combination of IIS(you can configure that through ***project property***)
```xml
 <system.webServer>
    <security>
      <authentication>
        <anonymousAuthentication enabled="false" />
        <windowsAuthentication enabled="true" />
      </authentication>
    </security>
  </system.webServer>
```
* ...and ASP.NET settings(you can configure that through ***project property***)
```xml
<authentication mode="Windows" />
```
