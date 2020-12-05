using Ftec.ProjetosWeb.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Uniftec.ProjetosWeb.Application;
using Uniftec.ProjetosWeb.Repository;

namespace Ftec.ProjetosWeb.WebApi.Controllers
{
    /// <summary>
    /// API responsável por fazer a manutenção de usuarios
    /// </summary>
    public class UsuarioController : ApiController
    {
        /// <summary>
        /// Este método retorna uma listagem de todos os usuarios
        /// </summary>
        /// <returns>Nao possui retorno</returns>
       
        //[Authorize] //para esse metodo ser processado ele precisa ser autenticado

        public HttpResponseMessage Get()
        {
            try
            {
                List<Usuario> UsuariosModel = new List<Usuario>();
                UsuarioRepository usuarioRepository = new UsuarioRepository(ConfigurationManager.ConnectionStrings["conexao"].ToString());
                UsuarioApplication usuarioApplication = new UsuarioApplication(usuarioRepository);

                List<Uniftec.ProjetosWeb.Domain.Entities.Usuario> usuarios = usuarioApplication.ProcurarTodos();

                //Realizar o adapter entre a entidade e o modelo de dados do dominio
                foreach (var user in usuarios)
                {
                    UsuariosModel.Add(new Usuario()
                    {
                        Id = user.Id,
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

        /// <summary>
        /// Este método retorna um usuario a partir do seu ID
        /// </summary>
        /// <param name="id">Id relativo a chave de busca para o usuario</param>
        /// <returns>Retorna um usuario</returns>
        public HttpResponseMessage Get(Guid id)
        {
            try
            {
                Usuario usuarioModel = null;
                UsuarioRepository usuarioRepository = new UsuarioRepository(ConfigurationManager.ConnectionStrings["conexao"].ToString());
                UsuarioApplication usuarioApplication = new UsuarioApplication(usuarioRepository);

                Uniftec.ProjetosWeb.Domain.Entities.Usuario usuario = usuarioApplication.Procurar(id);

                //Realizar o adapter entre a entidade e o modelo de dados do dominio
                if (usuario != null)
                {
                    usuarioModel = new Usuario()
                    {
                        Id = usuario.Id,
                        PrimeiroNome = usuario.PrimeiroNome,
                        SegundoNome = usuario.SegundoNome,
                        Funcao = usuario.Funcao,
                        Servidores = usuario.Servidores,
                        Email = usuario.Email,
                        Senha = usuario.Senha
                    };

                    return Request.CreateResponse(HttpStatusCode.OK, usuarioModel);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        public HttpResponseMessage Post([FromBody] Usuario usuario)
        {
            try
            {
                //Inclusão do usuario na base de dados
                //Essa inclusao retorna um ID
                //Id retornado para o requisitante do serviço
                UsuarioRepository usuarioRepository = new UsuarioRepository(ConfigurationManager.ConnectionStrings["conexao"].ToString());
                UsuarioApplication usuarioApplication = new UsuarioApplication(usuarioRepository);

                //Converter o model para uma entidade de dominio
                Uniftec.ProjetosWeb.Domain.Entities.Usuario usuarioDomain = new Uniftec.ProjetosWeb.Domain.Entities.Usuario()
                {
                    Id = usuario.Id,
                    PrimeiroNome = usuario.PrimeiroNome,
                    SegundoNome = usuario.SegundoNome,
                    Funcao = usuario.Funcao,
                    Servidores = usuario.Servidores,
                    Email = usuario.Email,
                    Senha = usuario.Senha,
  
                };

                foreach (var servidor in usuario.ListaServidores)
                {
                    usuarioDomain.ListaServidores.Add(new Uniftec.ProjetosWeb.Domain.Entities.Servidor()
                    {
                        Id = servidor.Id,
                        Nome = servidor.Nome,
                        EnderecoFisico = servidor.EnderecoFisico,
                        Processador = servidor.Processador,
                        SistemaOperacional = servidor.SistemaOperacional,
                        MacAddress = servidor.MacAddress,
                        IpAddress = servidor.IpAddress,
                        Descricao = servidor.Descricao,
                        Sensor =
                        {
                            Id = servidor.Sensor.Id,
                            Temperatura = servidor.Sensor.Temperatura,
                            Pressao = servidor.Sensor.Pressao,
                            Altitude = servidor.Sensor.Altitude,
                            Umidade = servidor.Sensor.Umidade,
                            Data = servidor.Sensor.Data,
                            PontoOrvalho = servidor.Sensor.PontoOrvalho
                        }

                    });

                }
              
                usuarioApplication.Inserir(usuarioDomain);

                return Request.CreateErrorResponse(HttpStatusCode.OK, Convert.ToString(usuarioDomain.Id));
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        public HttpResponseMessage Put(Guid id, [FromBody] Usuario usuario)
        {
            try
            {
                //Alterar o usuario na base de dados
                //Essa alteracao retorna um ID
                //Id retornado para o requisitante do serviço
                UsuarioRepository usuarioRepository = new UsuarioRepository(ConfigurationManager.ConnectionStrings["conexao"].ToString());
                UsuarioApplication usuarioApplication = new UsuarioApplication(usuarioRepository);

                //Converter o model para uma entidade de dominio
                Uniftec.ProjetosWeb.Domain.Entities.Usuario usuarioDomain = new Uniftec.ProjetosWeb.Domain.Entities.Usuario()
                {
                    Id = id,
                    PrimeiroNome = usuario.PrimeiroNome,
                    SegundoNome = usuario.SegundoNome,
                    Funcao = usuario.Funcao,
                    Servidores = usuario.Servidores,
                    Email = usuario.Email,
                    Senha = usuario.Senha
                };

                usuarioApplication.Alterar(usuarioDomain);

                return Request.CreateErrorResponse(HttpStatusCode.OK, Convert.ToString(id));
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }

        }

        public HttpResponseMessage Delete(Guid id)
        {
            try
            {
                //Excluir o usuario na base de dados
                //Essa exclusão retorna verdadeiro ou falso
                UsuarioRepository usuarioRepository = new UsuarioRepository(ConfigurationManager.ConnectionStrings["conexao"].ToString());
                UsuarioApplication usuarioApplication = new UsuarioApplication(usuarioRepository);

                var retorno = usuarioApplication.Excluir(id);

                return Request.CreateErrorResponse(HttpStatusCode.OK, Convert.ToString(retorno));
            }
            catch(Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}
