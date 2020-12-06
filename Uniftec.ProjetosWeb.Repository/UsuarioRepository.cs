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
                using (var transacao = con.BeginTransaction())
                {
                    try
                    {
                        NpgsqlCommand comando = new NpgsqlCommand();
                        comando.Connection = con;
                        comando.Transaction = transacao;

                        //Deletar os UsuarioServidor
                        comando.CommandText = "DELETE FROM public.usuario_servidor WHERE idusuario=@idusuario";
                        comando.Parameters.AddWithValue("idusuario", usuario.Id);
                        comando.ExecuteNonQuery();

                        //Alterar o Usuario
                        comando.CommandText = "UPDATE public.usuario " +
                                              "SET primeironome=@primeironome, segundonome=@segundonome, funcao=@funcao, servidores=@servidores, email=@email, senha=@senha" +
                                              "WHERE id=@id";

                        comando.Parameters.AddWithValue("id", usuario.Id);
                        comando.Parameters.AddWithValue("primeironome", usuario.PrimeiroNome);
                        comando.Parameters.AddWithValue("segundonome", usuario.SegundoNome);
                        comando.Parameters.AddWithValue("funcao", usuario.Funcao);
                        comando.Parameters.AddWithValue("servidores", usuario.Servidores);
                        comando.Parameters.AddWithValue("email", usuario.Email);
                        comando.Parameters.AddWithValue("senha", usuario.Senha);

                        //Executamos o comando
                        comando.ExecuteNonQuery();

                        //Inserir os UsuarioServidor
                        foreach (var serv in usuario.ListaServidores)
                        {
                            comando.CommandText = "INSERT INTO public.usuario_servidor " +
                                                   " (idusuario, idservidor, id) " +
                                                   " VALUES(@idusuario, @idservidor, @id)";

                            comando.Parameters.AddWithValue("id", Guid.NewGuid());
                            comando.Parameters.AddWithValue("idusuario", usuario.Id);
                            comando.Parameters.AddWithValue("idservidor", serv.Id);

                            comando.ExecuteNonQuery();
                        }

                        transacao.Commit();
                    }
                    catch (Exception e)
                    {
                        transacao.Rollback();
                        throw e;
                    }
                }
            }
        }

        public void Excluir(Guid id)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(this.stringConexao))
            {
                con.Open();
                using (var transacao = con.BeginTransaction())
                {
                    try
                    {
                        NpgsqlCommand comando = new NpgsqlCommand();
                        comando.Connection = con;
                        comando.Transaction = transacao;

                        //Excluir o Usuario
                        comando.CommandText = "DELETE FROM public.usuario WHERE id=@id;";
                        comando.Parameters.AddWithValue("id", id);

                        //Executamos o comando
                        comando.ExecuteNonQuery();

                        //Excluir o UsuarioServidor
                        comando.CommandText = "DELETE FROM public.usuario_servidor WHERE idusuario=@idusuario";
                        comando.Parameters.AddWithValue("idusuario", id);
                        comando.ExecuteNonQuery();

                        transacao.Commit();
                    }
                    catch (Exception e)
                    {
                        transacao.Rollback();
                        throw e;
                    }
                }
            }
        }

        public void Inserir(Usuario usuario)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(this.stringConexao))
            {
                con.Open();

                using (var transacao = con.BeginTransaction())
                {
                    try
                    {
                        NpgsqlCommand comando = new NpgsqlCommand();
                        comando.Connection = con;
                        comando.Transaction = transacao;

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

                        foreach (var serv in usuario.ListaServidores)
                        {
                            //Inserir o UsuarioServidor
                            comando.CommandText = "INSERT INTO public.usuario_servidor " +
                                                   " (idusuario, idservidor, id) " +
                                                   " VALUES(@idusuario, @idservidor, @id)";

                            comando.Parameters.AddWithValue("id", Guid.NewGuid());
                            comando.Parameters.AddWithValue("idusuario", usuario.Id);
                            comando.Parameters.AddWithValue("idservidor", serv.Id);

                            comando.ExecuteNonQuery();
                        }

                        transacao.Commit();
                    }
                    catch (Exception e)
                    {
                        transacao.Rollback();
                        throw e;
                    }
                }
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

                comando.CommandText = "SELECT * " +
                                      "FROM usuario u " +
                                      "INNER JOIN usuario_servidor us ON u.id = us.idusuario " +
                                      "INNER JOIN servidor s ON us.idservidor = s.id " +
                                      "WHERE u.id = @id ";

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

                comando.CommandText = "SELECT * " +
                                      "FROM usuario u " +
                                      "LEFT JOIN usuario_servidor us ON u.id = us.idusuario " +
                                      "LEFT JOIN servidor s ON us.idservidor = s.id ";

                var leitor = comando.ExecuteReader();
                //leitor.Close();

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
