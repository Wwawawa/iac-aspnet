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
* ...and ASP.NET settings
```xml
<authentication mode="Windows" />
```
