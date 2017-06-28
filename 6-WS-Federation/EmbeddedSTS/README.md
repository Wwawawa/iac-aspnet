### [EmbeddedSts](https://github.com/Wwawawa/Thinktecture.IdentityModel/tree/master/source/EmbeddedSts)

* Which is EmbeddedSts project, you can use it as your EmbeddedSts directtly by making some change for your client:
  * add **.dll** file of this project into your client or add reference of this project into your client
  * add **EmbeddedStsUsers.json** user list file into your client **App_Data** folder(default the EmbeddedSts project will add EmbeddedStsUsers.json if you don't create this file).
  * add below config into your client web.config
    ```xml
    <configSections>
        <section name="system.identityModel" type="System.IdentityModel.Configuration.SystemIdentityModelSection, System.IdentityModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B77A5C561934E089" />
        <section name="system.identityModel.services" type="System.IdentityModel.Services.Configuration.SystemIdentityModelServicesSection, System.IdentityModel.Services, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B77A5C561934E089" />
    </configSections>

    ...

    <system.web>
        <authentication mode="None" />
    </system.web>

    ....

    <system.webServer>    
        <modules>     
          <add name="SessionAuthenticationModule" type="System.IdentityModel.Services.SessionAuthenticationModule, System.IdentityModel.Services, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" preCondition="managedHandler" />
          <add name="WSFederationAuthenticationModule" type="System.IdentityModel.Services.WSFederationAuthenticationModule, System.IdentityModel.Services, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" preCondition="managedHandler" />
        </modules>
    </system.webServer>

    ...

    <system.identityModel.services>
        <federationConfiguration>
          <cookieHandler requireSsl="false" />
          <wsFederation passiveRedirectEnabled="true"
                        issuer="http://EmbeddedSts"
                        realm="http://localhost:28428/"
                        requireHttps="false"
          />
        </federationConfiguration>
    </system.identityModel.services>
    ````
