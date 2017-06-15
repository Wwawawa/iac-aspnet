##Demo
* Config 
  * add configSections
  ```xml
    <configSections>
    <section name="system.identityModel"
             type="System.IdentityModel.Configuration.SystemIdentityModelSection, System.IdentityModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B77A5C561934E089" />
    <section name="system.identityModel.services"
             type="System.IdentityModel.Services.Configuration.SystemIdentityModelServicesSection, System.IdentityModel.Services, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B77A5C561934E089" />
    </configSections>
  ```
  * this module only available for ssl, so for simple demo, remove this limitation  
  ```xml
    <system.identityModel.services>
      <federationConfiguration>
        <cookieHandler requireSsl="false" />
      </federationConfiguration>
    </system.identityModel.services>
  ```
  * add this module
  ```xml
      <add name="SessionAuthenticationModule"
           type="System.IdentityModel.Services.SessionAuthenticationModule, System.IdentityModel.Services, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" />
      </modules>
  ```
* change the ClaimsTransformer

* remove the postAuthenticationRequest from global.asax.cs, because this will run every request, and we don't need this here, since we cache that.

* change the login logic in the AccountController
  * need to config the ClaimsAuthenticationManager in the web.config, can call our customer ClaimsAuthenticationManager that is Web.Security.ClaimsTransformer method
  ```xml
    <system.identityModel>
      <identityConfiguration>
        <claimsAuthenticationManager type="Web.Security.ClaimsTransformer, Web" />
      </identityConfiguration>
    </system.identityModel>
  ```

