#### 1- install the Thinktecture.IdentityServer.v2.5
* unzip file
* set app_data acl
* setup ssl
* setup iis
* Thinktecture install successfully
  * initial your configuration
    * create a default user
  * sign in as your default user
  * you can add another user or use your default user
    * must set access of IdentityServerUsers: this is used to authenticate your app.
  * turn off the SSL requirement on the WS-Fed protocol config page in order to support http return.
#### 2- deployment the required element in the web.config manually
* add configSections
```xml
<configSections>
    <section name="system.identityModel"
             type="System.IdentityModel.Configuration.SystemIdentityModelSection, System.IdentityModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B77A5C561934E089" />
    <section name="system.identityModel.services"
             type="System.IdentityModel.Services.Configuration.SystemIdentityModelServicesSection, System.IdentityModel.Services, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B77A5C561934E089" />
</configSections>
```
* add required module
```xml
 <system.webServer>    
    <modules>
      <remove name="FormsAuthentication" />
      <add name="WSFederationAuthenticationModule"
           type="System.IdentityModel.Services.WSFederationAuthenticationModule, System.IdentityModel.Services, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
           preCondition="managedHandler" />
      <add name="SessionAuthenticationModule"
           type="System.IdentityModel.Services.SessionAuthenticationModule, System.IdentityModel.Services, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
           preCondition="managedHandler" />      
    </modules>
  </system.webServer>
```
* add system.identityModel and system.identityModel.services
```xml
	<system.identityModel>
    <identityConfiguration>
      <audienceUris>
        <add value="http://localhost:15701/" />
      </audienceUris>

      <issuerNameRegistry type="System.IdentityModel.Tokens.ConfigurationBasedIssuerNameRegistry, System.IdentityModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">
        <trustedIssuers>
	<!--thumbprint value which can be got from 'Signing Thumbprint' in the key configuration-->
          <add thumbprint="D8E0BD015980C24F96FD0EBAD88EA61BEFA7DF82"
               name="IdSrv" />
        </trustedIssuers>
      </issuerNameRegistry>
    </identityConfiguration>
  </system.identityModel>

  <system.identityModel.services>
    <federationConfiguration>
      <cookieHandler requireSsl="false" />
      <!--issuer is WS-Federation from Application Integration of home page-->
      <wsFederation passiveRedirectEnabled="true"		
                    issuer="https://localhost/idsrv/issue/wsfed"
                    realm="http://localhost:15701/"
                    requireHttps="false" />
    </federationConfiguration>
  </system.identityModel.services>
```
* add authentication config
```xml
<location path="FederationMetadata">
  <system.web>
    <authorization>
      <allow users="*" />
    </authorization>
  </system.web>
</location>
<system.web>
  <!--<authorization>
      <deny users="?" />
    </authorization>-->
  <authentication mode="None" />
```
