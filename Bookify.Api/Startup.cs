using System;
using System.Web.Http;
using Bookify.Api.Providers;
using Bookify.Api.UserRoles;
using Bookify.Data;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Swashbuckle.Application;

[assembly: OwinStartup(typeof(Bookify.Api.Startup))]
namespace Bookify.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);

            ConfigureSwagger(config);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            CreateRoles.Execute();
            Seed.Execute();
        }

        private void ConfigureSwagger(HttpConfiguration config)
        {
            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "Bookify.Api");
            })
            .EnableSwaggerUi();
        }
    }
}