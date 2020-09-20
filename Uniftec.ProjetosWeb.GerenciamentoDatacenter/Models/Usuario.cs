using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Uniftec.ProjetosWeb.GerenciamentoDatacenter.Models
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string PrimeiroNome { get; set; }

        public string SegundoNome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }
       
        public Usuario()
        {
            Id = Guid.NewGuid();
        }
        
    }
}