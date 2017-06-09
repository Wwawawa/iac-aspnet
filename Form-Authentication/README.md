
#### The ASP.NET Pipeline
* BeginRequest - Request(login page)
* AuthenticateRequest - Check cookie - Set Principal
* PostAuthenticateRequest
* AuthorizeRequest
* ExecuteHandler - Resource Rendering
* EndRequest - Redirect to login page - Response
