1- add below code into config
```xml
<configSections>
    <section name="system.identityModel"
             type="System.IdentityModel.Configuration.SystemIdentityModelSection, System.IdentityModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B77A5C561934E089" />
    <section name="system.identityModel.services"
             type="System.IdentityModel.Services.Configuration.SystemIdentityModelServicesSection, System.IdentityModel.Services, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B77A5C561934E089" />
  </configSections>

  <system.identityModel>
    <identityConfiguration>
      <!--call custom Authorization class 'ClaimsBasedAuthorization.AuthorizationManager'-->
      <claimsAuthorizationManager type="ClaimsBasedAuthorization.AuthorizationManager, ClaimsBasedAuthorization"/>
    </identityConfiguration>
  </system.identityModel>
```
```xml
    <authentication mode="Windows" />
    <authorization>
      <deny users="?" />
    </authorization>
```
2- change homeController and add customerController
3- add custom Authorization class 'ClaimsBasedAuthorization.AuthorizationManager'
