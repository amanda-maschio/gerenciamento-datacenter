using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uniftec.ProjetosWeb.Domain.Entities;
using Uniftec.ProjetosWeb.Domain.Repository;

namespace Uniftec.ProjetosWeb.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private string stringConexao;

        public UsuarioRepository(String stringConexao)
        {
            //this.stringConexao = "Server=127.0.0.1; Port=5432; Database=gerdatacenter; User Id=postgres; Password=070397";
            this.stringConexao = stringConexao;
        }

        public void Alterar(Usuario usuario)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(this.stringConexao))
            {
                con.Open();
                NpgsqlCommand comando = new NpgsqlCommand();
                comando.Connection = con;

                //Alterar o Usuario
                comando.CommandText = "UPDATE public.usuario " +
                    "SET primeironome=@primeironome, segundonome=@segundonome, funcao=@funcao, servidores=@servidores, email=@email, senha=@senha" +
                    "WHERE id=@id;";

                comando.Parameters.AddWithValue("id", usuario.Id);
                comando.Parameters.AddWithValue("primeironome", usuario.PrimeiroNome);
                comando.Parameters.AddWithValue("segundonome", usuario.SegundoNome);
                comando.Parameters.AddWithValue("funcao", usuario.Funcao);
                comando.Parameters.AddWithValue("servidores", usuario.Servidores);
                comando.Parameters.AddWithValue("email", usuario.Email);
                comando.Parameters.AddWithValue("senha", usuario.Senha);

                //Executamos o comando
                comando.ExecuteNonQuery();
            }
        }

        public void Excluir(Guid id)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(this.stringConexao))
            {
                con.Open();
                NpgsqlCommand comando = new NpgsqlCommand();
                comando.Connection = con;

                //Excluir o Usuario
                comando.CommandText = "DELETE FROM public.usuario WHERE id=@id;";
                comando.Parameters.AddWithValue("id", id);

                //Executamos o comando
                comando.ExecuteNonQuery();
            }
        }

        public void Inserir(Usuario usuario)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(this.stringConexao))
            {
                con.Open();
                NpgsqlCommand comando = new NpgsqlCommand();
                comando.Connection = con;

                //Inserir o Usuario
                comando.CommandText = "INSERT INTO public.usuario " +
                                      "(id, primeironome, segundonome, funcao, servidores, email, senha)" +
                                      "VALUES(@id, @primeironome, @segundonome, @funcao, @servidores, @email, @senha);";

                comando.Parameters.AddWithValue("id", usuario.Id);
                comando.Parameters.AddWithValue("primeironome", usuario.PrimeiroNome);
                comando.Parameters.AddWithValue("segundonome", usuario.SegundoNome);
                comando.Parameters.AddWithValue("funcao", usuario.Funcao);
                comando.Parameters.AddWithValue("servidores", usuario.Servidores);
                comando.Parameters.AddWithValue("email", usuario.Email);
                comando.Parameters.AddWithValue("senha", usuario.Senha);

                //Executamos o comando
                comando.ExecuteNonQuery();
            }
        }

        public Usuario Selecionar(Guid id)
        {
            Usuario usu = null;
            using (NpgsqlConnection con = new NpgsqlConnection(this.stringConexao))
            {
                con.Open();
                NpgsqlCommand comando = new NpgsqlCommand();
                comando.Connection = con;

                comando.CommandText = "SELECT usuario.id , usuario.primeironome , usuario.segundonome , " +
                                      "usuario.funcao , usuario.servidores , usuario.email , usuario.senha " +
                                      "FROM usuario " +
                                      "WHERE usuario.id = @id ";

                comando.Parameters.AddWithValue("id", id);
                var leitor = comando.ExecuteReader();

                while (leitor.Read())
                {
                    usu = new Usuario()
                    {
                        PrimeiroNome = leitor["primeironome"].ToString(),
                        SegundoNome = leitor["segundonome"].ToString(),
                        Funcao = leitor["funcao"].ToString(),
                        Servidores = leitor["servidores"].ToString(),
                        Email = leitor["email"].ToString(),
                        Senha = leitor["senha"].ToString(),
                        Id = Guid.Parse(leitor["id"].ToString()),
                      
                    };
                };

                return usu;
            }
        }

        public List<Usuario> SelecionarTodos()
        {
            List<Usuario> usuarios = new List<Usuario>();

            using (NpgsqlConnection con = new NpgsqlConnection(this.stringConexao))
            {
                con.Open();
                NpgsqlCommand comando = new NpgsqlCommand();
                comando.Connection = con;

                comando.CommandText = "SELECT usuario.id , usuario.primeironome , usuario.segundonome , " +
                                      "usuario.funcao , usuario.servidores , usuario.email , usuario.senha , " +
                                      "FROM usuario ";

                var leitor = comando.ExecuteReader();

                while (leitor.Read())
                {
                    usuarios.Add( new Usuario()
                        {
                            PrimeiroNome = leitor["primeironome"].ToString(),
                            SegundoNome = leitor["segundonome"].ToString(),
                            Funcao = leitor["funcao"].ToString(),
                            Servidores = leitor["servidores"].ToString(),
                            Email = leitor["email"].ToString(),
                            Senha = leitor["senha"].ToString(),
                            Id = Guid.Parse(leitor["id"].ToString()),

                        });
                };

                return usuarios;
            }
        }
    }
}
