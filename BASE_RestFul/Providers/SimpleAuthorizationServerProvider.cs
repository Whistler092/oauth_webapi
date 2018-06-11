using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using BASE_RestFul.Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;

namespace BASE_RestFul.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            using (AuthRepo repo = new AuthRepo())
            {
                IdentityUser user = await repo.FindUser(context.UserName, context.Password);
                if (user == null)
                {
                    context.SetError("Invalid", "Invalid user credentials");
                    return;
                }
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("sub", context.UserName));
                identity.AddClaim(new Claim("role", "user"));
                context.Validated(identity);
            }
        }
    }
}