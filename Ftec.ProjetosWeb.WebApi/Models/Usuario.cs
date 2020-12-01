using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ftec.ProjetosWeb.WebApi.Models
{
    public class Usuario
    {
        public Guid Id { get; set; }

        public string PrimeiroNome { get; set; }

        public string SegundoNome { get; set; }

        public string Funcao { get; set; }

        public string Servidores { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public List<Servidor> ListaServidores { get; set; }

        public Usuario()
        {
            Id = Guid.NewGuid();
            this.ListaServidores = new List<Servidor>();
        }

        public bool SenhaIsValid(string senha)
        {
            return (senha == Senha);
        }
    }
}