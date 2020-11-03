using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniftec.ProjetosWeb.Domain.Entities
{
    public class Servidor
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string EnderecoFisico { get; set; }

        public string Processador { get; set; }

        public string SistemaOperacional { get; set; }

        public long MacAddress { get; set; }

        public string IpAddress { get; set; }

        public string Descricao { get; set; }

        public Servidor()
        {
            Id = Guid.NewGuid();
        }
    }
}
