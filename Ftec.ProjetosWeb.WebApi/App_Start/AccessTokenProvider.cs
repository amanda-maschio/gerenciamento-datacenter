using Ftec.WebAPI.Application;
using Ftec.WebAPI.Infra.Repository;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Uniftec.ProjetosWeb.Domain.Repository;

namespace Ftec.ProjetosWeb.WebApi.App_Start
{
    public class AccessTokenProvider : OAuthAuthorizationServerProvider
    {
        private ClienteApplication clienteApplication;
        private IClienteRepository clienteRepository;

        public AccessTokenProvider()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexao"].ToString();
            clienteRepository = new ClienteRepository(connectionString);
            clienteApplication = new ClienteApplication(clienteRepository);
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            if (clienteApplication.Autenticar(context.UserName, context.Password))
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("sub", context.UserName));
                identity.AddClaim(new Claim("role", "user"));

                context.Validated(identity);
            }
            else
            {
                context.SetError("Acesso Inválido", "Usuario ou senha são inválidos");
                return;
            }
        }
    }
}