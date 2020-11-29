using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uniftec.ProjetosWeb.Domain.Entities;

namespace Uniftec.ProjetosWeb.Domain.Repository
{
    public interface IServidorRepository
    {
        void Inserir(Servidor servidor);

        void Excluir(Guid id);

        void Alterar(Servidor servidor);

        Servidor Selecionar(Guid id);

        List<Servidor> SelecionarTodos();
    }
}
