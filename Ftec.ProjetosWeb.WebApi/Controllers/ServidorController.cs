using Ftec.ProjetosWeb.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ftec.ProjetosWeb.WebApi.Controllers
{
    public class ServidorController : ApiController
    {
        public HttpResponseMessage Get()
        {
            List<Servidor> servidores = new List<Servidor>();

            try
            {

                servidores.Add(new Servidor()
                {
                    Nome = "Servidor Uniftec CX",
                    EnderecoFisico = "Rua Gustavo Rahmos Sehbe, 107",
                    Processador = "Intel Xeon Silver 4208 2.1G, 8C/16T, 9.6GT/s, 11M Cache, Turbo, HT (85W) DDR4-2400",
                    SistemaOperacional = "Windows Server 2012",
                    MacAddress = 00-14-22-01-23-45,
                    IpAddress = "172.16.0.100",
                    Descricao = "Descrição breve das características do servidor"

                });

                return Request.CreateResponse(HttpStatusCode.OK, servidores);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}
