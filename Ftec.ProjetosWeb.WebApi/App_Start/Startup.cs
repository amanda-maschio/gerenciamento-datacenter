using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace Ftec.WebAPI.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                  name: "DefaultApi",
                  routeTemplate: "api/{controller}/{id}",
                  defaults: new { id = RouteParameter.Optional }
             );

            app.UseCors(CorsOptions.AllowAll);

            ActivateTokenGenerator(app);

            app.UseWebApi(config);
        }

        private void ActivateTokenGenerator(IAppBuilder app)
        {
            var opcoesConfiguracaoToken = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(1),
                Provider = new AccessTokenProvider()
            };

            app.UseOAuthAuthorizationServer(opcoesConfiguracaoToken);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}