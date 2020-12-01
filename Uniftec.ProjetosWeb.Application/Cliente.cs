using Ftec.WebAPI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ftec.WebAPI.Application
{
    public class ClienteApplication
    {
        IClienteRepository clienteRepository;

        public ClienteApplicatio(IClienteRepository clienteRepository)
        {
            this.clienteRepository = clienteRepository;
        }
        public bool Autenticar(string email, string password)
        {
            var user = this.clienteRepository.Find(email.ToLower());
            if (user == null)
            {
                throw new ApplicationException("Usuario não encontrado");
            }

            if (!user.PassswordIsValid(password))
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

            var user = this.clienteRepository.Find(email);
            user.Password = newPassword;

            this.clienteRepository.Update(user);
        }
    }
}
