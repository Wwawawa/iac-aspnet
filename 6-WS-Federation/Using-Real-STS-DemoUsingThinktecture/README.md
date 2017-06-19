#### 1- Configuration
* Set up the external login
	* login URL
	* application identifier
	```xml
	<system.identityModel.services>
	  <federationConfiguration>
		<wsFederation passiveRedirectEnabled="true" issuer="https://idsrv/issue/wsfed" realm="http://myapp" />
	  </federationConfiguration>
	</system.identityModel.services>
	```
#### 2- Setting up trust with security token service
* Only accept tokens from issuers you trust
	* by default determined using the thumbprint of the signing certificate
	* extensible
	* name value becomes Issuer property on claims
	```xml
	<system.identityModel>
	  <identityConfiguration>
		<issuerNameRegistry type="… ConfigurationBasedIssuerNameRegistry, …">
		  <trustedIssuers>
			<add thumbprint="425F81FE4A7871BABE9343EA2E95087B97AD5C3A" <!--this is an sign-in X.509 certificate which need to sign to come into application as a trust issuer-->
			name="STS" />
		  </trustedIssuers>
		</issuerNameRegistry>
	  </identityConfiguration>
	</system.identityModel>
	```
#### 3- Certificate Validation(optional)
* Additionally you can also run validation on the signing certificate
	* chain or peer trust
	* CRL checking
* Often not needed (set to ‚none‘)
```xml
<certificateValidation certificateValidationMode="ChainTrust"<!--check if the issuer is in your trusted CA folder--> 
revocationMode="Online" <!--make sure this certificate hasn't been revoked--> />
```
#### 4- Setting up token validation
* Identifier of the intended token receiver is embedded in token
	* mitigates token re-purposing attacks
	* you only want to accept tokens with a known value
	```xml
	<system.identityModel>
	  <identityConfiguration>
		<audienceUris>
		  <add value="http://myapp" />
		</audienceUris>
	  </identityConfiguration>
	</system.identityModel>
	```
#### 5- Federation Metadata
* Most of the configuration settings can be derived from federation metadata
	* XML document describing a token service (or relying party)
	* that‘s how the Identity & Access wizard does it
	```sh
	https://idsrv/FederationMetadata/2007-06/FederationMetadata.xml
	```
