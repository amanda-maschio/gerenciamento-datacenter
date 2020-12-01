using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uniftec.ProjetosWeb.Domain.Repository;

namespace Ftec.WebAPI.Application
{
    public class ClienteApplication
    {
        IClienteRepository clienteRepository;

        public ClienteApplication(IClienteRepository clienteRepository)
        {
            this.clienteRepository = clienteRepository;
        }
        public bool Autenticar(string email, string password)
        {
            var client = this.clienteRepository.Find(email.ToLower());
            if (client == null)
            {
                throw new ApplicationException("Usuario não encontrado");
            }

            if (!client.PasswordIsValid(password))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void TrocarSenha(string email, string newPassword)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ApplicationException("E-mail deve ser informado");
            }

            if (string.IsNullOrEmpty(newPassword))
            {
                throw new ApplicationException("Nova senha deve ser informada");
            }

            var client = this.clienteRepository.Find(email);
            client.Password = newPassword;

            this.clienteRepository.Update(client);
        }
    }
}
