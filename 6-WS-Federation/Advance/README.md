#### 1- Dynamic Configuration([Demo](https://github.com/Wwawawa/iac-aspnet/blob/master/6-WS-Federation/Advance/Global.asax.cs))
* Sometimes you need to configure your application dynamically at startup
	* register configuration event handler in Application_Start
	* you can get all element of IdentityConfiguration/FederationConfiguration, and do it dynamically
	```cs
	protected void Application_Start()
	{
		FederatedAuthentication.FederationConfigurationCreated += FederatedAuthentication_FederationConfigurationCreated;
	}
	void FederatedAuthentication_FederationConfigurationCreated(object sender, FederationConfigurationCreatedEventArgs e)
	{ 
		// e.FederationConfiguration.IdentityConfiguration.
		// set e.FederationConfiguration 
		// dynamic config
	}
	```
#### 2- WS-Federation Events
* Similar to the session authentication module
	* allows customizing various aspects of token handling
		* SecurityTokenReceived: Allows modifying the token (cancellable)
		* SecurityTokenValidated: Allows modifying the principal (cancellable)
		* SessionSecurityTokenCreated: Allows modifying the session details
		* SignedIn/SignedOut: Fire after successful sign in/sign out
		* SignInError/SignOutError: Fire after failed sign in/sign out
		* RedirectingToIdentityProvider: Allows modifying the sign in message
#### 3- Dynamic Redirection([Demo](https://github.com/Wwawawa/iac-aspnet/blob/master/6-WS-Federation/Advance/Global.asax.cs))
* Sometimes you want to determine details of redirection to the identity provider at runtime
	* use Forms Authentication to redirect to a local login page, then do the redirect manually
	* use the RedirectingToIdentityProvider event (global.asax)
	* .NET SignInRequestMessage class allow constructing signin requests
	* you can dynamically change the parameters of the sign-in request message
	```cs
	public void WSFederationAuthenticationModule_RedirectingToIdentityProvider(object sender, RedirectingToIdentityProviderEventArgs e)
	{ 
		// modify e.SignInRequestMessage 
	}
	```
#### 4- Constructing Sign-In URLs(Demo using [HomeController](https://github.com/Wwawawa/iac-aspnet/blob/master/6-WS-Federation/Advance/Controllers/HomeController.cs) and [View](https://github.com/Wwawawa/iac-aspnet/blob/master/6-WS-Federation/Advance/Views/Home/index.cshtml))
* Useful for constructing redirect URLs
	* dynamically
	* soft login
	```cs
	var signin = new SignInRequestMessage(
		new Uri("https://idsrv/issue/wsfed"), "http://myapp");
	var url = signin.WriteQueryString();
	```
  ```html
  <a href="@ViewBag.SignInUrl">Sign-in</a>
  ```
#### 5- Signing Out(Demo of Logout using [AccountController](https://github.com/Wwawawa/iac-aspnet/blob/master/6-WS-Federation/Advance/Controllers/AccountController.cs) and [View](https://github.com/Wwawawa/iac-aspnet/blob/master/6-WS-Federation/Advance/Views/Shared/_Layout.cshtml))
* Need to clear the authentication session cookie
	* does not sign-out the user at the token service (see next module), so when go back to the page you logout, you can still access to this page with the sign-in 
	```cs
	FederatedAuthentication.WSFederationAuthenticationModule.SignOut();
	```
#### 6- Server-side Session Caching & Sliding Expiration
* Sliding expiration
	* see the Session Management module
* Server-side Caching
	* Instructs the session authentication module to use the server-side caching infrastructure
	```cs
	void WSFederationAuthenticationModule_SessionSecurityTokenCreated(object sender, SessionSecurityTokenCreatedEventArgs e)
	{
		e.SessionToken.IsReferenceMode = true;
	}
	```
