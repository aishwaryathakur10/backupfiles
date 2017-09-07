
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Token
{
    public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        TokenDbEntities db = new TokenDbEntities();
         public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated(); 
        }

         public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            var re= db.TokenTables.FirstOrDefault(p => p.username == context.UserName && p.password == context.Password);

            if (context.UserName == re.username && context.Password == re.password)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, re.role));
               
                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid_grant", "Provided username and password is incorrect");
                return;
            }
    }
}
    }