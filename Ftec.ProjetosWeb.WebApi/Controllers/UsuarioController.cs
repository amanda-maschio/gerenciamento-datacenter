using Ftec.ProjetosWeb.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Uniftec.ProjetosWeb.Application;
using Uniftec.ProjetosWeb.Repository;

namespace Ftec.ProjetosWeb.WebApi.Controllers
{
    public class UsuarioController : ApiController
    {
        public HttpResponseMessage Get()
        {
            try {
                List<Usuario> UsuariosModel = new List<Usuario>();
            UsuarioRepository usuarioRepository = new UsuarioRepository();
            UsuarioApplication usuarioApplication = new UsuarioApplication(usuarioRepository);

            List<Uniftec.ProjetosWeb.Domain.Entities.Usuario> usuarios = usuarioApplication.ProcurarTodos();

                //Realizar o adapter entre a entidade e o modelo de dados do dominio
                foreach (var user in usuarios)
                {
                    UsuariosModel.Add(new Usuario()
                    {
                        PrimeiroNome = user.PrimeiroNome,
                        SegundoNome = user.SegundoNome,
                        Funcao = user.Funcao,
                        Servidores = user.Servidores,
                        Email = user.Email,
                        Senha = user.Senha
                    });
                }
                return Request.CreateResponse(HttpStatusCode.OK, UsuariosModel);
            }
            catch (ApplicationException ap)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ap);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

    }
}
