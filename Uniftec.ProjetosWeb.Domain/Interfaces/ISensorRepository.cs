using System;
using System.Collections.Generic;
using Uniftec.ProjetosWeb.Domain.Entities;

namespace Uniftec.ProjetosWeb.Domain.Repository
{
    public interface ISensorRepository
    {
        void Inserir(Sensor Sensor);

        void Excluir(Guid id);

        void Alterar(Sensor Sensor);

        Sensor Selecionar(Guid id);

        List<Sensor> SelecionarTodos();
    }
}
