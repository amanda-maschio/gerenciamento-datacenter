using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniftec.ProjetosWeb.Domain.Interfaces
{
    class IClienteRepository
    {
        private Guid Insert(Cliente cliente);

        Cliente Find(Guid ID);

        Cliente Find(string email);

        List<Cliente> FindAll();

        Guid Update(Cliente usuario);

        bool Delete(Guid id);
    }
}
