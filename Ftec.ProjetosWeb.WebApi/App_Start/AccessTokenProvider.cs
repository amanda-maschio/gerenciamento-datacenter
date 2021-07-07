using Microsoft.Owin.Security.OAuth;
using System.Threading.Tasks;

namespace Ftec.ProjetosWeb.WebApi.App_Start
{
    public class AccessTokenProvider : OAuthAuthorizationServerProvider
    {

        public AccessTokenProvider()
        {
            
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

        }
    }
}