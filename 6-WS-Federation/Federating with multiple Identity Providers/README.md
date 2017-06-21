### Federating with multiple Identity Providers(like google, facebook...)
#### Federation with external parties is implemented using a Resource-STS/Federation Gateway
* application code can stay the same
* all the hard work happens at the token service
* workflow
```th
user->own identity provider(facebook, google...)->get token then send to R-STS->authenticate pass return trust token to user->request to app with the trust token
```
* using Home Realm Discovery (HRD)
