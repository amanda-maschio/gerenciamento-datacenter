using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uniftec.ProjetosWeb.Domain.Entities;

namespace Uniftec.ProjetosWeb.Domain.Repository
{
    public interface IClienteRepository
    {
        Guid Insert(Cliente cliente);

        Cliente Find(Guid ID);

        Cliente Find(string username);

        List<Cliente> FindAll();

        Guid Update(Cliente cliente);

        bool Delete(Guid id);
    }
}
