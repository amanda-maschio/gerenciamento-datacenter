using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Uniftec.ProjetosWeb.Domain.Entities
{
    public class Cliente
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
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