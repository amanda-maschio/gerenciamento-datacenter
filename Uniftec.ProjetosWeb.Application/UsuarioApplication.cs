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


        public Guid Inserir(Usuario usuario)
        {
            try
            {
                usuario.Id = Guid.NewGuid();
                usuarioRepository.Inserir(usuario);
                return usuario.Id;
            }
            catch(Exception e)
            {
                throw e;
            }

        }

        public List<Usuario> ProcurarTodos()
        {
            try
            {
                var listaUsuario = usuarioRepository.SelecionarTodos();
                return listaUsuario;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Guid Alterar(Usuario usuario)
        {
            try
            {
                usuarioRepository.Alterar(usuario);
                return usuario.Id;
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
                usuarioRepository.Excluir(id);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }

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
                var usuario = usuarioRepository.Selecionar(id);

                return usuario;
            }
            catch (Exception e)
            {
                throw e;
            }

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

        }
    }
}
