#### pipeline
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

#### This demo show the every step what need to do, we make customer httpmodule, and configure that in web.config. of caurse, we should do this thing in global.asax which include all steps of pipeline
