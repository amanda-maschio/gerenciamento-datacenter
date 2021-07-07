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
    public class ServidorRepository : IServidorRepository
    {
        private string stringConexao;

        public ServidorRepository(String stringConexao)
        {
            this.stringConexao = stringConexao;
        }

        public void Alterar(Servidor servidor)
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

                        //Alterar o Servidor
                        comando.CommandText = "UPDATE public.servidor " +
                                              " SET nome=@nome, enderecofisico=@enderecofisico, processador=@processador, sistemaoperacional=@sistemaoperacional, macaddress=@macaddress, ipaddress=@ipaddress " +
                                              " WHERE servidorid=@servidorid";

                        comando.Parameters.AddWithValue("servidorid", servidor.Id);
                        comando.Parameters.AddWithValue("nome", servidor.Nome);
                        comando.Parameters.AddWithValue("enderecofisico", servidor.EnderecoFisico);
                        comando.Parameters.AddWithValue("processador", servidor.Processador);
                        comando.Parameters.AddWithValue("sistemaoperacional", servidor.SistemaOperacional);
                        comando.Parameters.AddWithValue("macaddress", servidor.MacAddress);
                        comando.Parameters.AddWithValue("ipaddress", servidor.IpAddress);


                        //Executamos o comando
                        comando.ExecuteNonQuery();
                        comando.Parameters.Clear();

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
                using (var transacao = con.BeginTransaction()) {

                    try
                    {
                        NpgsqlCommand comando = new NpgsqlCommand();
                        comando.Connection = con;
                        comando.Transaction = transacao;

                        //Excluir o Servidor
                        comando.CommandText = "DELETE FROM public.servidor WHERE servidorid=@servidorid;";
                        comando.Parameters.AddWithValue("servidorid", id);

                        //Executamos o comando
                        comando.ExecuteNonQuery();

                        transacao.Commit();

                    }
                    catch(Exception e)
                    {
                        transacao.Rollback();
                        throw e;
                    }
                }

            }
        }

        public void Inserir(Servidor servidor)
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

                        //Inserir o Servidor
                        comando.CommandText = "INSERT INTO public.servidor" +
                                              "(servidorid, nome, enderecofisico, processador, sistemaoperacional, macaddress, ipaddress) " +
                                              "VALUES(@servidorid, @nome, @enderecofisico, @processador, @sistemaoperacional, @macaddress, @ipaddress); ";

                        comando.Parameters.AddWithValue("servidorid", servidor.Id);
                        comando.Parameters.AddWithValue("nome", servidor.Nome);
                        comando.Parameters.AddWithValue("enderecofisico", servidor.EnderecoFisico);
                        comando.Parameters.AddWithValue("processador", servidor.Processador);
                        comando.Parameters.AddWithValue("sistemaoperacional", servidor.SistemaOperacional);
                        comando.Parameters.AddWithValue("macaddress", servidor.MacAddress);
                        comando.Parameters.AddWithValue("ipaddress", servidor.IpAddress);

                        //Executamos o comando
                        comando.ExecuteNonQuery();
                        comando.Parameters.Clear();

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

        public Servidor Selecionar(Guid id)
        {
            Servidor serv = null;
            using (NpgsqlConnection con = new NpgsqlConnection(this.stringConexao))
            {
                con.Open();
                NpgsqlCommand comando = new NpgsqlCommand();
                comando.Connection = con;

                comando.CommandText = " SELECT * FROM servidor" +
                                      " WHERE servidor.servidorid = @servidorid";
                
                comando.Parameters.AddWithValue("servidorid", id);
                var leitor = comando.ExecuteReader();

                while (leitor.Read())
                {
                    serv = new Servidor()
                    {
                        Id = Guid.Parse(leitor["servidorid"].ToString()),
                        Nome = leitor["nome"].ToString(),
                        EnderecoFisico = leitor["enderecofisico"].ToString(),
                        Processador = leitor["processador"].ToString(),
                        SistemaOperacional = leitor["sistemaoperacional"].ToString(),
                        MacAddress = leitor["macaddress"].ToString(),
                        IpAddress = leitor["ipaddress"].ToString(),

                    };
                };

                return serv;
            }
        }

        public List<Servidor> SelecionarTodos()
        {
            List<Servidor> servidores = new List<Servidor>();
            using (NpgsqlConnection con = new NpgsqlConnection(this.stringConexao))
            {
                con.Open();
                NpgsqlCommand comando = new NpgsqlCommand();
                comando.Connection = con;

                comando.CommandText = "SELECT * FROM servidor";

                var leitor = comando.ExecuteReader();

                while (leitor.Read())
                {
                    servidores.Add( new Servidor()
                    {
                        Id = Guid.Parse(leitor["servidorid"].ToString()),
                        Nome = leitor["nome"].ToString(),
                        EnderecoFisico = leitor["enderecofisico"].ToString(),
                        Processador = leitor["processador"].ToString(),
                        SistemaOperacional = leitor["sistemaoperacional"].ToString(),
                        MacAddress = leitor["macaddress"].ToString(),
                        IpAddress = leitor["ipaddress"].ToString(),

                    });
                };

                return servidores;
            }
        }
    }
}
