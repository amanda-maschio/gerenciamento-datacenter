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
                        comando.Parameters.Clear();

                        //Alterar o Usuario
                        comando.CommandText = "UPDATE public.usuario " +
                                              "SET primeironome=@primeironome, segundonome=@segundonome, funcao=@funcao, servidores=@servidores, email=@email, senha=@senha " +
                                              "WHERE usuarioid=@usuarioid";

                        comando.Parameters.AddWithValue("usuarioid", usuario.Id);
                        comando.Parameters.AddWithValue("primeironome", usuario.PrimeiroNome);
                        comando.Parameters.AddWithValue("segundonome", usuario.SegundoNome);
                        comando.Parameters.AddWithValue("funcao", usuario.Funcao);
                        comando.Parameters.AddWithValue("servidores", usuario.Servidores);
                        comando.Parameters.AddWithValue("email", usuario.Email);
                        comando.Parameters.AddWithValue("senha", usuario.Senha);

                        //Executamos o comando
                        comando.ExecuteNonQuery();
                        comando.Parameters.Clear();

                        //Inserir os UsuarioServidor
                        foreach (var serv in usuario.ListaServidores)
                        {
                            comando.CommandText = "INSERT INTO public.usuario_servidor " +
                                                   " (idusuario, idservidor, usuarioservidorid) " +
                                                   " VALUES(@idusuario, @idservidor, @usuarioservidorid)";

                            comando.Parameters.AddWithValue("usuarioservidorid", Guid.NewGuid());
                            comando.Parameters.AddWithValue("idusuario", usuario.Id);
                            comando.Parameters.AddWithValue("idservidor", serv.Id);

                            comando.ExecuteNonQuery();
                            comando.Parameters.Clear();
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

                        //Excluir o UsuarioServidor
                        comando.CommandText = "DELETE FROM public.usuario_servidor WHERE idusuario=@idusuario";
                        comando.Parameters.AddWithValue("idusuario", id);
                        comando.ExecuteNonQuery();
                        comando.Parameters.Clear();

                        //Excluir o Usuario
                        comando.CommandText = "DELETE FROM public.usuario WHERE usuarioid=@usuarioid;";
                        comando.Parameters.AddWithValue("usuarioid", id);

                        //Executamos o comando
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
                                              "(usuarioid, primeironome, segundonome, funcao, servidores, email, senha)" +
                                              "VALUES(@usuarioid, @primeironome, @segundonome, @funcao, @servidores, @email, @senha);";

                        comando.Parameters.AddWithValue("usuarioid", usuario.Id);
                        comando.Parameters.AddWithValue("primeironome", usuario.PrimeiroNome);
                        comando.Parameters.AddWithValue("segundonome", usuario.SegundoNome);
                        comando.Parameters.AddWithValue("funcao", usuario.Funcao);
                        comando.Parameters.AddWithValue("servidores", usuario.Servidores);
                        comando.Parameters.AddWithValue("email", usuario.Email);
                        comando.Parameters.AddWithValue("senha", usuario.Senha);
                        
                        //Executamos o comando
                        comando.ExecuteNonQuery();
                        comando.Parameters.Clear();

                        foreach (var serv in usuario.ListaServidores)
                        {
                            //Inserir o UsuarioServidor
                            comando.CommandText = "INSERT INTO public.usuario_servidor " +
                                                   " (idusuario, idservidor, usuarioservidorid) " +
                                                   " VALUES(@idusuario, @idservidor, @usuarioservidorid)";

                            comando.Parameters.AddWithValue("usuarioservidorid", Guid.NewGuid());
                            comando.Parameters.AddWithValue("idusuario", usuario.Id);
                            comando.Parameters.AddWithValue("idservidor", serv.Id);

                            comando.ExecuteNonQuery();
                            comando.Parameters.Clear();
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
                                      "WHERE u.usuarioid = @usuarioid ";

                comando.Parameters.AddWithValue("usuarioid", id);
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
                        Id = Guid.Parse(leitor["usuarioid"].ToString()),
                        
                    };
                };

                leitor.Close();

                comando.CommandText = "SELECT * " +
                                      "FROM usuario u " +
                                      "INNER JOIN usuario_servidor us ON u.usuarioid = us.idusuario " +
                                      "INNER JOIN servidor s ON us.idservidor = s.servidorid " +
                                      "INNER JOIN sensor se ON s.servidorid = se.servidorid " +
                                      "WHERE u.usuarioid = @usuarioid ";

                comando.Parameters.AddWithValue("usuarioid", id);
                leitor = comando.ExecuteReader();

                while (leitor.Read())
                {
                    usu.ListaServidores.Add(new Servidor()
                    {
                        Id = Guid.Parse(leitor["servidorid"].ToString()),
                        Nome = leitor["nome"].ToString(),
                        EnderecoFisico = leitor["enderecofisico"].ToString(),
                        Processador = leitor["processador"].ToString(),
                        SistemaOperacional = leitor["sistemaoperacional"].ToString(),
                        MacAddress = leitor["macaddress"].ToString(),
                        IpAddress = leitor["ipaddress"].ToString(),

                        Sensor = new Sensor()
                        {
                            Id = Guid.Parse(leitor["sensorid"].ToString()),
                            Temperatura = float.Parse(leitor["temperatura"].ToString()),
                            Pressao = float.Parse(leitor["pressao"].ToString()),
                            Altitude = float.Parse(leitor["altitude"].ToString()),
                            Umidade = float.Parse(leitor["umidade"].ToString()),
                            Data = Convert.ToDateTime(leitor["data"]),
                            PontoOrvalho = float.Parse(leitor["pontoorvalho"].ToString()),
                        }
                    });
                }
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
                                      "FROM usuario u ";

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
                            Id = Guid.Parse(leitor["usuarioid"].ToString()),

                    });

                };

                leitor.Close();

                foreach (var usuario in usuarios)
                {
                    comando.CommandText = "SELECT * " +
                                      "FROM usuario u " +
                                      "INNER JOIN usuario_servidor us ON u.usuarioid = us.idusuario " +
                                      "INNER JOIN servidor s ON us.idservidor = s.servidorid " +
                                      "INNER JOIN sensor se ON s.servidorid = se.servidorid " +
                                      "WHERE u.usuarioid = @usuarioid ";
                
                    comando.Parameters.AddWithValue("usuarioid", usuario.Id);
                    leitor = comando.ExecuteReader();
                
                    while (leitor.Read())
                    {
                        usuario.ListaServidores.Add(new Servidor()
                        {
                            Id = Guid.Parse(leitor["servidorid"].ToString()),
                            Nome = leitor["nome"].ToString(),
                            EnderecoFisico = leitor["enderecofisico"].ToString(),
                            Processador = leitor["processador"].ToString(),
                            SistemaOperacional = leitor["sistemaoperacional"].ToString(),
                            MacAddress = leitor["macaddress"].ToString(),
                            IpAddress = leitor["ipaddress"].ToString(),

                            Sensor = new Sensor()
                            {
                                Id = Guid.Parse(leitor["sensorid"].ToString()),
                                Temperatura = float.Parse(leitor["temperatura"].ToString()),
                                Pressao = float.Parse(leitor["pressao"].ToString()),
                                Altitude = float.Parse(leitor["altitude"].ToString()),
                                Umidade = float.Parse(leitor["umidade"].ToString()),
                                Data = Convert.ToDateTime(leitor["data"]),
                                PontoOrvalho = float.Parse(leitor["pontoorvalho"].ToString()),
                            }
                        });
                    }
                    leitor.Close();
                }
                return usuarios;
            }
        }
    }
}
