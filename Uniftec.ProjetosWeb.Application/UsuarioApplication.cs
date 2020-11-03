using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uniftec.ProjetosWeb.Domain.Entities;
using Uniftec.ProjetosWeb.Domain.Repository;

namespace Uniftec.ProjetosWeb.Application
{
    public class UsuarioApplication
    {
        private IUsuarioRepository usuarioRepository;
        
        public UsuarioApplication(IUsuarioRepository usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }


        public Guid Inserir(Usuario usuarios)
        {
            try
            {
             
                usuarios.Id = Guid.NewGuid();
                usuarioRepository.Inserir(usuarios);
                return usuarios.Id;
            }
            catch(Exception e)
            {
                throw e;
            }

            throw new NotImplementedException();
        }

        public List<Usuario> ProcurarTodos()
        {
            throw new NotImplementedException();
        }

        public Guid Alterar(Usuario usuarios)
        {
            try
            {

                usuarioRepository.Alterar(usuarios);
                return usuarios.Id;
            }
            catch (Exception e)
            {
                throw e;
            }

            throw new NotImplementedException();
        }

        public Exception Excluir(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    throw new ApplicationException("O ID deve ser informado!");
                        //Regra de negócio a partir do repositorio
                }
                usuarioRepository.Excluir(id);
                
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
            throw new NotImplementedException();
        }

        public Usuario Procurar(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    throw new ApplicationException("O ID deve ser informado!");
                    //Regra de negócio a partir do repositorio
                }
                var usuarios = usuarioRepository.Selecionar(id);

                return usuarios;
            }
            catch (Exception e)
            {
                throw e;
            }

            throw new NotImplementedException();
        }

        public List<Usuario> ProcurarTodos(Guid id)
        {
            try
            {
                var usuarios = usuarioRepository.SelecionarTodos();

                return usuarios;
            }
            catch (Exception e)
            {
                throw e;
            }
            throw new NotImplementedException();
        }
    }
}
