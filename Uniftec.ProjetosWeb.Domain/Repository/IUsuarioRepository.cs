using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uniftec.ProjetosWeb.Domain.Entities;

namespace Uniftec.ProjetosWeb.Domain.Repository
{
    public interface IUsuarioRepository
    {
        void Inserir(Usuario usuarios);
        void Excluir(Guid id);
        void Alterar(Usuario usuarios);
        Usuario Selecionar(Guid id);

        List<Usuario> SelecionarTodos();
        void Alterar(object usuarios);
    }
}
