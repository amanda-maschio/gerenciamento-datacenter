using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Uniftec.ProjetosWeb.GerenciamentoDatacenter.Models;

namespace Uniftec.ProjetosWeb.GerenciamentoDatacenter
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start()
        {
            //Executado sempre que uma sessao da aplicacao é iniciada
            Usuario usuario1 = new Usuario()
            {
                PrimeiroNome = "Joao",
                SegundoNome = "da Silva",
                Funcao = "S",
                Servidores = "1",
                Email = "joaodasilva@hotmail.com",
                Senha = "estaeminhasenha"
            };

            Usuario usuario2 = new Usuario()
            {

                PrimeiroNome = "Maria",
                SegundoNome = "de Souza",
                Funcao = "G",
                Servidores = "2",
                Email = "mariadesouza@hotmail.com",
                Senha = "minhasenhaestae"
            };

            List<Usuario> usuarios = new List<Usuario>();
            usuarios.Add(usuario1);
            usuarios.Add(usuario2);

            Session["usuarios"] = usuarios;
        }
    }
}
