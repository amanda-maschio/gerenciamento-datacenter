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
    /// API responsável por fazer a manutenção de servidores
    /// </summary>
    public class ServidorController : ApiController
    {
        /// <summary>
        /// Este método retorna uma listagem de todos os servidores
        /// </summary>
        /// <returns>Nao possui retorno</returns>
        public HttpResponseMessage Get()
        {
            try
            {
                List<Servidor> ServidoresModel = new List<Servidor>();
                ServidorRepository servidorRepository = new ServidorRepository(ConfigurationManager.ConnectionStrings["conexao"].ToString());
                ServidorApplication servidorApplication = new ServidorApplication(servidorRepository);

                List<Uniftec.ProjetosWeb.Domain.Entities.Servidor> servidores = servidorApplication.ProcurarTodos();

                //Realizar o adapter entre a entidade e o modelo de dados do dominio
                foreach (var serv in servidores)
                {
                    ServidoresModel.Add(new Servidor()
                    {
                        Id = serv.Id,
                        Nome = serv.Nome,
                        EnderecoFisico = serv.EnderecoFisico,
                        Processador = serv.Processador,
                        SistemaOperacional = serv.SistemaOperacional,
                        MacAddress = serv.MacAddress,
                        IpAddress = serv.IpAddress,

                        Sensor = new Sensor()
                        {
                            Id = serv.Sensor.Id,
                            Temperatura = serv.Sensor.Temperatura,
                            Pressao = serv.Sensor.Pressao,
                            Altitude = serv.Sensor.Altitude,
                            Umidade = serv.Sensor.Umidade,
                            Data = serv.Sensor.Data,
                            PontoOrvalho = serv.Sensor.PontoOrvalho
                        }
                    });
                }
                return Request.CreateResponse(HttpStatusCode.OK, ServidoresModel);
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
        /// Este método retorna um servidor a partir do seu ID
        /// </summary>
        /// <param name="id">Id relativo a chave de busca para o servidor</param>
        /// <returns>Retorna um servidor</returns>
        public HttpResponseMessage Get(Guid id)
        {
            try
            {

                Servidor servidorModel = null;
                ServidorRepository servidorRepository = new ServidorRepository(ConfigurationManager.ConnectionStrings["conexao"].ToString());
                ServidorApplication servidorApplication = new ServidorApplication(servidorRepository);

                Uniftec.ProjetosWeb.Domain.Entities.Servidor servidor = servidorApplication.Procurar(id);

                //Realizar o adapter entre a entidade e o modelo de dados do dominio
                if (servidor != null)
                {
                    servidorModel = new Servidor()
                    {
                        Id = servidor.Id,
                        Nome = servidor.Nome,
                        EnderecoFisico = servidor.EnderecoFisico,
                        Processador = servidor.Processador,
                        SistemaOperacional = servidor.SistemaOperacional,
                        MacAddress = servidor.MacAddress,
                        IpAddress = servidor.IpAddress,

                        Sensor = new Sensor()
                        {
                            Id = servidor.Sensor.Id,
                            Temperatura = servidor.Sensor.Temperatura,
                            Pressao = servidor.Sensor.Pressao,
                            Altitude = servidor.Sensor.Altitude,
                            Umidade = servidor.Sensor.Umidade,
                            Data = servidor.Sensor.Data,
                            PontoOrvalho = servidor.Sensor.PontoOrvalho
                        }
                    };

                    return Request.CreateResponse(HttpStatusCode.OK, servidorModel);
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

        public HttpResponseMessage Post([FromBody] Servidor servidor)
        {
            try
            {
                //Inclusão do servidor na base de dados
                //Essa inclusao retorna um ID
                //Id retornado para o requisitante do serviço
                ServidorRepository servidorRepository = new ServidorRepository(ConfigurationManager.ConnectionStrings["conexao"].ToString());
                ServidorApplication servidorApplication = new ServidorApplication(servidorRepository);

                //Converter o model para uma entidade de dominio
                Uniftec.ProjetosWeb.Domain.Entities.Servidor servidorDomain = new Uniftec.ProjetosWeb.Domain.Entities.Servidor()
                {
                    Id = servidor.Id,
                    Nome = servidor.Nome,
                    EnderecoFisico = servidor.EnderecoFisico,
                    Processador = servidor.Processador,
                    SistemaOperacional = servidor.SistemaOperacional,
                    MacAddress = servidor.MacAddress,
                    IpAddress = servidor.IpAddress,

                    Sensor = new Uniftec.ProjetosWeb.Domain.Entities.Sensor()
                    {
                        Id = servidor.Sensor.Id,
                        Temperatura = servidor.Sensor.Temperatura,
                        Pressao = servidor.Sensor.Pressao,
                        Altitude = servidor.Sensor.Altitude,
                        Umidade = servidor.Sensor.Umidade,
                        Data = servidor.Sensor.Data,
                        PontoOrvalho = servidor.Sensor.PontoOrvalho
                    }
                };

                servidorApplication.Inserir(servidorDomain);

                return Request.CreateErrorResponse(HttpStatusCode.OK, Convert.ToString(servidorDomain.Id));
            }
            catch(Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        public HttpResponseMessage Put(Guid id, [FromBody] Servidor servidor)
        {
            try
            {
                //Alterar o servidor na base de dados
                //Essa alteracao retorna um ID
                //Id retornado para o requisitante do serviço
                ServidorRepository servidorRepository = new ServidorRepository(ConfigurationManager.ConnectionStrings["conexao"].ToString());
                ServidorApplication servidorApplication = new ServidorApplication(servidorRepository);

                //Converter o model para uma entidade de dominio
                Uniftec.ProjetosWeb.Domain.Entities.Servidor servidorDomain = new Uniftec.ProjetosWeb.Domain.Entities.Servidor()
                {
                    Id = servidor.Id,
                    Nome = servidor.Nome,
                    EnderecoFisico = servidor.EnderecoFisico,
                    Processador = servidor.Processador,
                    SistemaOperacional = servidor.SistemaOperacional,
                    MacAddress = servidor.MacAddress,
                    IpAddress = servidor.IpAddress,

                    Sensor = new Uniftec.ProjetosWeb.Domain.Entities.Sensor()
                    {
                        Id = servidor.Sensor.Id,
                        Temperatura = servidor.Sensor.Temperatura,
                        Pressao = servidor.Sensor.Pressao,
                        Altitude = servidor.Sensor.Altitude,
                        Umidade = servidor.Sensor.Umidade,
                        Data = servidor.Sensor.Data,
                        PontoOrvalho = servidor.Sensor.PontoOrvalho
                    }
                };

                servidorApplication.Alterar(servidorDomain);

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
                //Excluir o servidor na base de dados
                //Essa exclusão retorna verdadeiro ou falso
                ServidorRepository servidorRepository = new ServidorRepository(ConfigurationManager.ConnectionStrings["conexao"].ToString());
                ServidorApplication servidorApplication = new ServidorApplication(servidorRepository);

                var retorno = servidorApplication.Excluir(id);

                return Request.CreateErrorResponse(HttpStatusCode.OK, Convert.ToString(retorno));
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
            
        }
    }
}