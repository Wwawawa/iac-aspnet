#### 1- Single Sign-On
* A simple extension on top of the „external authentication“ scenario
  * identity provider establishes a logon session with user („remember me“)
  * identity provider is shared across multiple applications
  * during that logon session, user can request token without re-authentication
#### 2- Single sign-out
* sign out from relying parties, sign out from the token service
* Security Token Service: call endpoint like 
```th
'/wsfed?wa=wsignout1.0'
```
* below show the example of all the relying parties sign out clean up query string
```html
<p> 
    <img src = "https://rp1/?wa=wsignoutcleanup1.0" /> 
</p> 
<p> 
    < img src="https://rp2/?wa=wsignoutcleanup1.0" /> 
</p> 
<p> 
    <img src = "https://rp3/?wa=wsignoutcleanup1.0" /> 
</p>
```
#### 3- Sample: initiating single sign-out([Demo](https://github.com/Wwawawa/iac-aspnet/blob/master/6-WS-Federation/Single-SignIn-And-SignOut/Controllers/AccountController.cs))
* SignOutRequestMessage helps in constructing the URL
```cs
public ActionResult SignOut()
{
    var fam = FederatedAuthentication.WSFederationAuthenticationModule; 
    // clear local cookie 
    fam.SignOut(isIPRequest: false);

    // initiate a federated sign out request to the sts. 
    var signOutRequest = new SignOutRequestMessage( 
        new Uri(fam.Issuer), 
        fam.Realm);
    return new RedirectResult(signOutRequest.WriteQueryString());
}
```
