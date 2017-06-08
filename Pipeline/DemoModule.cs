using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;

namespace iac_aspnet
{
    public class DemoModule : IHttpModule
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += context_BeginRequest;
            context.AuthenticateRequest += context_AuthenticateRequest;
            context.PostAuthenticateRequest += context_PostAuthenticateRequest;
            context.AuthorizeRequest += context_AuthorizeRequest;
            context.EndRequest += context_EndRequest;
        }

        private void context_BeginRequest(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void context_AuthenticateRequest(object sender, EventArgs e)
        {
            Trace.WriteLine("AuthenticateRequest");
            //authenticate a credential(if present)
            //Thread.CurrentPrincipal = authUser;
            //HttpContext.Current.User = authUser;
        }

        void context_PostAuthenticateRequest(object sender, EventArgs e)
        {
            Trace.WriteLine("PostAuthenticateRequest");
            var user = Thread.CurrentPrincipal;
            user = HttpContext.Current.User;
            //user = User;
            user = ClaimsPrincipal.Current;
        }

        void context_AuthorizeRequest(object sender, EventArgs e)
        {
            Trace.WriteLine("AuthorizeRequest");
        }

        void context_EndRequest(object sender, EventArgs e)
        {
            Trace.WriteLine("EndRequest");
        }
    }
}
