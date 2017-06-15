# iac-aspnet
#### 1-pipeline
this pipeline only show the step involved authentication, not all steps of aspnet 
* BeginRequest: Client make a request
* AuthenticateRequest: 
  * look for incoming credential, see if there is a credential of this request(like cookie/authentication header .etc).
  * if present, get authenticaiton(windows authenticaiton / form authenticaiton / authenticaiton wsfederation)
  * if failed, emit some error response, and jump to EndRequest
  * if succeed, set principal(identity initial)
* PostAuthenticateRequest: add cliams to identity
* AuthorizeRequest: determine if user is allowed to access resource
* ExecuteHandler: resource rendering
* EndRequest: post processing(error response/ redirect)
#### 2-[Windows Identity Foundation](https://docs.microsoft.com/en-us/dotnet/framework/security/index)

#### Project sort
* [1-Windows-Authentication](https://github.com/Wwawawa/iac-aspnet/tree/master/Windows-Authentication)
* [2-Form-Authentication](https://github.com/Wwawawa/iac-aspnet/tree/master/Form-Authentication)
* [3-ClaimsTransformation](https://github.com/Wwawawa/iac-aspnet/tree/master/ClaimsTransformation)
* [4-Authentication-Sessions](https://github.com/Wwawawa/iac-aspnet/tree/master/Authentication-Sessions)

