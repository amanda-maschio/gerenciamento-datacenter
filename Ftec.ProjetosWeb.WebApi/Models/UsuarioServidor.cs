using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ftec.ProjetosWeb.WebApi.Models
{
    public class UsuarioServidor
    {
        public Guid Id { get; set; }

        public Usuario Usuario { get; set; }

        public Servidor Servidor { get; set; }

        public UsuarioServidor()
        {
            Id = Guid.NewGuid();
            this.Usuario = new Usuario();
            this.Servidor = new Servidor();
        }
    }
}