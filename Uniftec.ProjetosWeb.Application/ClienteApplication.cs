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
        public bool Autenticar(string username, string password)
        {
            var client = this.clienteRepository.Find(username.ToLower());
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

        public void TrocarSenha(string username, string newPassword)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ApplicationException("E-mail deve ser informado");
            }

            if (string.IsNullOrEmpty(newPassword))
            {
                throw new ApplicationException("Nova senha deve ser informada");
            }

            var client = this.clienteRepository.Find(username);
            client.Password = newPassword;

            this.clienteRepository.Update(client);
        }
    }
}
