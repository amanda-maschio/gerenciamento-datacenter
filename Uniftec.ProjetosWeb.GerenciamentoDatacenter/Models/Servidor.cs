using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Uniftec.ProjetosWeb.GerenciamentoDatacenter.Models
{
    public class Servidor
    {
        public Guid Id { get; set; }
        
        public string Nome { get; set; }

        public string EnderecoFisico { get; set; }

        public string Processador { get; set; }

        public string SistemaOperacional { get; set; }

        public long MacAddress { get; set; }

        public long IpAddress { get; set; }

        public string Descricao { get; set; }

        public Temperatura Temperatura { get; set; }

        public Umidade Umidade { get; set; }

        public PontoOrvalho PontoOrvalho { get; set; }


        public Servidor()
        {
            Id = Guid.NewGuid();
        }
    }
}