#### Sort
* 1-[Using-Real-STS-DemoUsingThinktecture](https://github.com/Wwawawa/iac-aspnet/tree/master/6-WS-Federation/Using-Real-STS-DemoUsingThinktecture)
* 2-[Advance-Dynamic-Other](https://github.com/Wwawawa/iac-aspnet/tree/master/6-WS-Federation/Advance)
* 3-[Single-SignIn-And-SignOut](https://github.com/Wwawawa/iac-aspnet/tree/master/6-WS-Federation/Single-SignIn-And-SignOut)
* 4-[Federating with multiple Identity Providers](https://github.com/Wwawawa/iac-aspnet/tree/master/6-WS-Federation/Federating%20with%20multiple%20Identity%20Providers)
* 5-[EmbeddedSTS](https://github.com/Wwawawa/iac-aspnet/tree/master/6-WS-Federation/EmbeddedSTS)
* 6-[Several Tokens Authenticaiton Demo](https://github.com/Wwawawa/Thinktecture.IdentityModel/tree/master/source/EmbeddedSts/Tokens)

### Reference [IdentityServer2 Document](https://github.com/IdentityServer/IdentityServer2/wiki)
#### 1- External authentication is a separate authenticaition logic which can reuse for muiltple application.
#### 2- workflow
```th
  client->sts(security token service)--token-->client->relying party(include physical/external/cloud application)
```
#### 3- SAML (Security Assertion Markup Language) token: a language formated token information
```xml
<saml:Assertion xmlns:saml="urn:oasis:names:tc:SAML:1.0:assertion">
  <saml:AttributeStatement>
    <saml:Attribute AttributeName="userid" AttributeNamespace="http://...">
      <saml:AttributeValue>42</saml:AttributeValue>
    </saml:Attribute>
    <saml:Attribute AttributeName="name" AttributeNamespace="http://... ">
      <saml:AttributeValue>Bob</saml:AttributeValue>
    </saml:Attribute>
    <saml:Attribute AttributeName="department" AttributeNamespace="http://... ">
      <saml:AttributeValue>Research</saml:AttributeValue>
    </saml:Attribute>
  </saml:AttributeStatement>
  <Signature xmlns="http://www.w3.org/2000/09/xmldsig#" />
</saml:Assertion>
```
#### 4- WS-Federation
* mainframe workflow
  ```th
  request resource from relying party Get /: client->relying party--(401) return "using wsfed?wa=wsignin1.0&wtrealm=address_of_rp" (url used to redirect to sts)-->client
  request token GET/wsfed : client--using "using wsfed?wa=wsignin1.0&wtrealm=address_of_rp" -->STS--return below form-->client
  request resource from relying party again Post /: client--with upon form-->relying party(200)
  ```
sts return anthentication info format form:
  ```xml
  <form method="POST" action="address_of_rp">
    <input name="wresult" value=""
    <saml:Assertion…" /> …
    <script > window.setTimeout('document.forms[0].submit()', 0); </script>
  </form>
  ```
* config for WS-Federation
	* Disable built-in authentication
    ```xml
    <authentication mode="None" />
    ```

  * Add WS-Federation & SessionAuthentication modules
  
    ```xml
    <modules>
      <add name="WSFederationAuthenticationModule" type="…WSFederationAuthenticationModule, …" preCondition="managedHandler" />
      <add name="SessionAuthenticationModule" type="…SessionAuthenticationModule, …" preCondition="managedHandler" />
    </modules>
    ```
* Pipeline Detials of WS-Federation anthentication
	* Anonymous user hits application (FAM(federation authentication module))
		* AuthorizeRequest -> 401
		* EndRequest -> 302 to security token service
	* Token post from security token service (FAM)
		* AuthenticateRequest
			* parse and validate incoming token
			* run claims transformation
			* establish session
	* Use session (SAM)
		* AuthenticateRequest
			* set principal
* Security Token Services
	* Commercial products
		* Microsoft Active Directory Federation Service 2
		* IBM Tivoli Federation Manager
		* Oracle Identity Manager
		* Ping Federate
	* .NET 4.5 contains base-classes to build you own STS
		* be aware you are building critical security infrastructure
		* open source: [Thinktecture.IdentityServer.v2](https://github.com/IdentityServer/IdentityServer2)
		* "Development STS" Visual Studio extension
