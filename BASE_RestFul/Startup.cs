using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System;
using Microsoft.Owin.Security.OAuth;
using BASE_RestFul.Providers;

[assembly: OwinStartup(typeof(BASE_RestFul.Startup))]

namespace BASE_RestFul
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            ConfigureOauth(app);

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll); //Access from Any Domain
            app.UseWebApi(config);
        }

        private void ConfigureOauth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions oauthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(5),
                Provider = new SimpleAuthorizationServerProvider()
            };

            //Token Generation

            app.UseOAuthAuthorizationServer(oauthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}