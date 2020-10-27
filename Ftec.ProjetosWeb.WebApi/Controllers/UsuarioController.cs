using Ftec.ProjetosWeb.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ftec.ProjetosWeb.WebApi.Controllers
{
    public class UsuarioController : ApiController
    {
        public HttpResponseMessage Get()
        {
            List<Usuario> usuarios = new List<Usuario>();

            try
            {

                usuarios.Add(new Usuario()
                {
                    PrimeiroNome = "Will",
                    SegundoNome = "da Silva",
                    Funcao = "Gerente",
                    Servidores = "CX",
                    Email = "dasilvawilliam58@gmail.com",
                    Senha = "123456"

                });

                return Request.CreateResponse(HttpStatusCode.OK, usuarios);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

    }
}
