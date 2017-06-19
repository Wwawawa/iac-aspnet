### Base on Form-authentication project, this demo is claims-based using thinktecture.identityModel as authorization model
#### 1- add below code into config
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
    <authentication mode="Forms">
      <forms loginUrl="~/account/login" />
    </authentication>
    <authorization>
      <deny users="?" />
    </authorization>
```
#### 2- change homeController and add customerController(call customerController using url [http://localhost:1096/api/customers/], this is webApi, need to add webApi package)
#### 3- add custom Authorization class 'ClaimsBasedAuthorization.AuthorizationManager'
#### 4- add webAPi config in global.asax
#### 5- add webApiConfig.cs into the folder of app_Start
#### 6 a new filter into 'FilterConfig' from the folder of app_Start
```cs
  filters.Add(new ClaimsAuthorizeAttribute());
```
