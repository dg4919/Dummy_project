using Microsoft.Owin.Security.OAuth;
using MyProject.Context;
using MyProject.Data;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyProject.Api
{
    // Code for genrating Token after verify user from login()
    public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        // Microsoft.Owin..Host.SystemWeb > must Require :)
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            dynamic _user;
            string role = "user";

            using (IUserRepository userRepository = new UserRepository(new dbContext()))
            {
                var userName = context.UserName;
                var password = context.Password;

                _user = userRepository.get_userInfo(userName);
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Role, role));
            identity.AddClaim(new Claim(ClaimTypes.Name, _user.Id.ToString()));

            context.Validated(identity);
            return Task.FromResult<object>(null);
        }

    }
}