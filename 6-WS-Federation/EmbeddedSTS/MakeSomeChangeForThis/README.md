#### Base on that config and code, we can make some change as below:
* 1- add below config into web.config
  ```xml
  <system.identityModel>
    <identityConfiguration>
      <audienceUris>
        <add value="http://localhost:28428/" />
      </audienceUris>  
      <certificateValidation certificateValidationMode="None" />
      <issuerNameRegistry type="Thinktecture.IdentityModel.Tokens.MetadataBasedIssuerNameRegistry, Thinktecture.IdentityModel">
        <!--this code can ignore, not have no means-->
          <trustedIssuerMetadata issuerName="local-sts" metadataAddress="http://localhost:28428/_sts/FederationMetadata/2007-06/FederationMetadata.xml" />
        <!--this code can ignore, not have no means-->
      </issuerNameRegistry>
      <securityTokenHandlers>
        <add type="System.IdentityModel.Services.Tokens.MachineKeySessionSecurityTokenHandler, System.IdentityModel.Services, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" />
        <remove type="System.IdentityModel.Tokens.SessionSecurityTokenHandler, System.IdentityModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" />
      </securityTokenHandlers>
    </identityConfiguration>
  </system.identityModel>
  ```
* 2-  Change code of EmbeddedStsConfiguration
    * comment below code, because this part has deploy in the web.config as upon
    ```cs
    //FederatedAuthentication.FederationConfigurationCreated += FederatedAuthentication_FederationConfigurationCreated;
    
     ....
     
    //var rpRealm = new Uri(config.WsFederationConfiguration.Realm);
    //if (!config.IdentityConfiguration.AudienceRestriction.AllowedAudienceUris.Contains(rpRealm))
    //{
    //    config.IdentityConfiguration.AudienceRestriction.AllowedAudienceUris.Add(rpRealm);
    //}
    //config.IdentityConfiguration.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.None;
    //config.IdentityConfiguration.RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck; 
    ```
* add **Thinktecture.IdentityModel** by NuGet
    
