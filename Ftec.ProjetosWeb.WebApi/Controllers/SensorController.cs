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
    /// API responsável por fazer a manutenção de Sensores
    /// </summary>
    public class SensorController : ApiController
    {
        /// <summary>
        /// Este método retorna uma listagem de todos os Sensores
        /// </summary>
        /// <returns>Nao possui retorno</returns>
        public HttpResponseMessage Get()
        {
            try
            {
                List<Sensor> SensoresModel = new List<Sensor>();
                SensorRepository SensorRepository = new SensorRepository(ConfigurationManager.ConnectionStrings["conexao"].ToString());
                SensorApplication SensorApplication = new SensorApplication(SensorRepository);

                List<Uniftec.ProjetosWeb.Domain.Entities.Sensor> Sensores = SensorApplication.ProcurarTodos();

                //Realizar o adapter entre a entidade e o modelo de dados do dominio
                foreach (var sen in Sensores)
                {
                    SensoresModel.Add(new Sensor()
                    {
                        Id = sen.Id,
                        Temperatura = sen.Temperatura,
                        Pressao = sen.Pressao,
                        Altitude = sen.Altitude,
                        Umidade = sen.Umidade,
                        Data = sen.Data,
                        PontoOrvalho = sen.PontoOrvalho,
                        MacAddressServidor = sen.MacAddressServidor

                    });
                }
                return Request.CreateResponse(HttpStatusCode.OK, SensoresModel);
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
        /// Este método retorna um Sensor a partir do seu ID
        /// </summary>
        /// <param name="id">Id relativo a chave de busca para o Sensor</param>
        /// <returns>Retorna um Sensor</returns>
        public HttpResponseMessage Get(Guid id)
        {
            try
            {

                Sensor SensorModel = null;
                SensorRepository SensorRepository = new SensorRepository(ConfigurationManager.ConnectionStrings["conexao"].ToString());
                SensorApplication SensorApplication = new SensorApplication(SensorRepository);

                Uniftec.ProjetosWeb.Domain.Entities.Sensor Sensor = SensorApplication.Procurar(id);

                //Realizar o adapter entre a entidade e o modelo de dados do dominio
                if (Sensor != null)
                {
                    SensorModel = new Sensor()
                    {
                        Id = Sensor.Id,
                        Temperatura = Sensor.Temperatura,
                        Pressao = Sensor.Pressao,
                        Altitude = Sensor.Altitude,
                        Umidade = Sensor.Umidade,
                        Data = Sensor.Data,
                        PontoOrvalho = Sensor.PontoOrvalho,
                        MacAddressServidor = Sensor.MacAddressServidor
                    };

                    return Request.CreateResponse(HttpStatusCode.OK, SensorModel);
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

        public HttpResponseMessage Post([FromBody] Sensor Sensor)
        {
            try
            {
                //Inclusão do Sensor na base de dados
                //Essa inclusao retorna um ID
                //Id retornado para o requisitante do serviço
                SensorRepository SensorRepository = new SensorRepository(ConfigurationManager.ConnectionStrings["conexao"].ToString());
                SensorApplication SensorApplication = new SensorApplication(SensorRepository);

                //Converter o model para uma entidade de dominio
                Uniftec.ProjetosWeb.Domain.Entities.Sensor SensorDomain = new Uniftec.ProjetosWeb.Domain.Entities.Sensor()
                {
                    Id = Sensor.Id,
                    Temperatura = Sensor.Temperatura,
                    Pressao = Sensor.Pressao,
                    Altitude = Sensor.Altitude,
                    Umidade = Sensor.Umidade,
                    Data = Sensor.Data,
                    PontoOrvalho = Sensor.PontoOrvalho,
                    MacAddressServidor = Sensor.MacAddressServidor
                };

                SensorApplication.Inserir(SensorDomain);

                return Request.CreateErrorResponse(HttpStatusCode.OK, Convert.ToString(SensorDomain.Id));
            }
            catch(Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        public HttpResponseMessage Put(Guid id, [FromBody] Sensor Sensor)
        {
            try
            {
                //Alterar o Sensor na base de dados
                //Essa alteracao retorna um ID
                //Id retornado para o requisitante do serviço
                SensorRepository SensorRepository = new SensorRepository(ConfigurationManager.ConnectionStrings["conexao"].ToString());
                SensorApplication SensorApplication = new SensorApplication(SensorRepository);

                //Converter o model para uma entidade de dominio
                Uniftec.ProjetosWeb.Domain.Entities.Sensor SensorDomain = new Uniftec.ProjetosWeb.Domain.Entities.Sensor()
                {
                    Id = Sensor.Id,
                    Temperatura = Sensor.Temperatura,
                    Pressao = Sensor.Pressao,
                    Altitude = Sensor.Altitude,
                    Umidade = Sensor.Umidade,
                    Data = Sensor.Data,
                    PontoOrvalho = Sensor.PontoOrvalho,
                    MacAddressServidor = Sensor.MacAddressServidor
                };

                SensorApplication.Alterar(SensorDomain);

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
                //Excluir o Sensor na base de dados
                //Essa exclusão retorna verdadeiro ou falso
                SensorRepository SensorRepository = new SensorRepository(ConfigurationManager.ConnectionStrings["conexao"].ToString());
                SensorApplication SensorApplication = new SensorApplication(SensorRepository);

                var retorno = SensorApplication.Excluir(id);

                return Request.CreateErrorResponse(HttpStatusCode.OK, Convert.ToString(retorno));
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
            
        }
    }
}