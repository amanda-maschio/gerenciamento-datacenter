using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Uniftec.ProjetosWeb.GerenciamentoDatacenter.Models
{
    public class Cliente
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }

        public Cliente()
        {
            Id = Guid.NewGuid();
        }

        public bool PasswordIsValid(string password)
        {
            return (password == Password);
        }
    }
}