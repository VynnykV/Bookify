using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Bookify.Data.Models;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security;

namespace Bookify.Api.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            string role;
            OnlineUser user;

            using (AuthRepository _repo = new AuthRepository())
            {
                user = await _repo.FindUser(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", "The login or password is incorrect.");
                    return;
                }

                var passwordValid = await _repo.CheckPasswordAsync(user, context.Password);
                if (!passwordValid)
                {
                    context.SetError("invalid_grant", "The login or password is incorrect.");
                    return;
                }

                role = _repo.GetUserRole(user.Id);
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("userId", user.Id));
            identity.AddClaim(new Claim(ClaimTypes.Role, role));

            List<Claim> roles = identity.Claims.Where(c => c.Type == ClaimTypes.Role).ToList();
            AuthenticationProperties properties = CreateProperties(user.UserName, Newtonsoft.Json.JsonConvert.SerializeObject(roles.Select(x => x.Value)));

            AuthenticationTicket ticket = new AuthenticationTicket(identity, properties);
            context.Validated(ticket);

            context.Validated(identity);
        }

        public static AuthenticationProperties CreateProperties(string userName, string Roles)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName },
                { "roles" ,Roles}
            };
            return new AuthenticationProperties(data);
        }
    }
}