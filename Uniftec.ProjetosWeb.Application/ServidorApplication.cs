using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uniftec.ProjetosWeb.Domain.Entities;
using Uniftec.ProjetosWeb.Domain.Repository;

namespace Uniftec.ProjetosWeb.Application
{
    public class ServidorApplication
    {
        private IServidorRepository servidorRepository;
        
        public ServidorApplication(IServidorRepository servidorRepository)
        {
            this.servidorRepository = servidorRepository;
        }


        public Guid Inserir(Servidor servidor)
        {
            try
            {
                servidor.Id = Guid.NewGuid();
                servidorRepository.Inserir(servidor);
                return servidor.Id;
            }
            catch(Exception e)
            {
                throw e;
            }

        }

        public List<Servidor> ProcurarTodos()
        {
            try
            {
                var listaServidores = servidorRepository.SelecionarTodos();
                return listaServidores;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Guid Alterar(Servidor servidor)
        {
            try
            {
                servidorRepository.Alterar(servidor);
                return servidor.Id;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public bool Excluir(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    throw new ApplicationException("O ID deve ser informado!");
                    //Regra de negócio a partir do repositorio
                }

                servidorRepository.Excluir(id);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public Servidor Procurar(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    throw new ApplicationException("O ID deve ser informado!");
                    //Regra de negócio a partir do repositorio
                }

                var servidores = servidorRepository.Selecionar(id);
                return servidores;

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public List<Servidor> ProcurarTodos(Guid id)
        {
            try
            {
                var servidores = servidorRepository.SelecionarTodos();

                return servidores;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
