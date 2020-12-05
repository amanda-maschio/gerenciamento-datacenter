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
                                              " SET nome=@nome, enderecofisico=@enderecofisico, processador=@processador, sistemaoperacional=@sistemaoperacional, macaddress=@macaddress, ipaddress=@ipaddress, descricao=@descricao " +
                                              " WHERE id=@id";

                        comando.Parameters.AddWithValue("id", servidor.Id);
                        comando.Parameters.AddWithValue("nome", servidor.Nome);
                        comando.Parameters.AddWithValue("enderecofisico", servidor.EnderecoFisico);
                        comando.Parameters.AddWithValue("processador", servidor.Processador);
                        comando.Parameters.AddWithValue("sistemaoperacional", servidor.SistemaOperacional);
                        comando.Parameters.AddWithValue("macaddress", servidor.MacAddress);
                        comando.Parameters.AddWithValue("ipaddress", servidor.IpAddress);
                        comando.Parameters.AddWithValue("descricao", servidor.Descricao);

                        //Executamos o comando
                        comando.ExecuteNonQuery();

                        //Alterar o Sensor
                        comando.CommandText = "UPDATE public.sensor " +
                                               " SET temperatura=@temperatura, pressao=@pressao, altitude=@altitude, umidade=@umidade, data=@data, pontoorvalho=@pontoorvalho " +
                                               " WHERE servidorid=@servidorid";

                        comando.Parameters.AddWithValue("servidorid", servidor.Id);
                        comando.Parameters.AddWithValue("temperatura", servidor.Sensor.Temperatura);
                        comando.Parameters.AddWithValue("pressao", servidor.Sensor.Pressao);
                        comando.Parameters.AddWithValue("altitude", servidor.Sensor.Altitude);
                        comando.Parameters.AddWithValue("umidade", servidor.Sensor.Umidade);
                        comando.Parameters.AddWithValue("data", servidor.Sensor.Data);
                        comando.Parameters.AddWithValue("pontoorvalho", servidor.Sensor.PontoOrvalho);

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
                        comando.CommandText = "DELETE FROM public.servidor WHERE id=@id;";
                        comando.Parameters.AddWithValue("id", id);

                        //Executamos o comando
                        comando.ExecuteNonQuery();

                        //Excluir o Sensor
                        comando.CommandText = "DELETE FROM public.sensor WHERE servidorid=@servidorid";
                        comando.Parameters.AddWithValue("servidorid", id);
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
                        comando.CommandText = "INSERT INTO public.servidor " +
                                              "(id, nome, enderecofisico, processador, sistemaoperacional, macaddress, ipaddress, descricao)" +
                                              "VALUES(@id, @nome, @enderecofisico, @processador, @sistemaoperacional, @macaddress, @ipaddress, @descricao);";

                        comando.Parameters.AddWithValue("id", servidor.Id);
                        comando.Parameters.AddWithValue("nome", servidor.Nome);
                        comando.Parameters.AddWithValue("enderecofisico", servidor.EnderecoFisico);
                        comando.Parameters.AddWithValue("processador", servidor.Processador);
                        comando.Parameters.AddWithValue("sistemaoperacional", servidor.SistemaOperacional);
                        comando.Parameters.AddWithValue("macaddress", servidor.MacAddress);
                        comando.Parameters.AddWithValue("ipaddress", servidor.IpAddress);
                        comando.Parameters.AddWithValue("descricao", servidor.Descricao);

                        //Executamos o comando
                        comando.ExecuteNonQuery();

                        //Inserir o Sensor
                        comando.CommandText = "INSERT INTO public.sensor " +
                                               " (id, temperatura, pressao, altitude, umidade, data, servidorid, pontoorvalho) " +
                                               " VALUES(@id, @temperatura, @pressao, @altitude, @umidade, @data, @servidorid, @pontoorvalho)";

                        comando.Parameters.AddWithValue("id", servidor.Sensor.Id);
                        comando.Parameters.AddWithValue("servidorid", servidor.Id);
                        comando.Parameters.AddWithValue("temperatura", servidor.Sensor.Temperatura);
                        comando.Parameters.AddWithValue("pressao", servidor.Sensor.Pressao);
                        comando.Parameters.AddWithValue("altitude", servidor.Sensor.Altitude);
                        comando.Parameters.AddWithValue("umidade", servidor.Sensor.Umidade);
                        comando.Parameters.AddWithValue("data", servidor.Sensor.Data);
                        comando.Parameters.AddWithValue("pontoorvalho", servidor.Sensor.PontoOrvalho);

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

        public Servidor Selecionar(Guid id)
        {
            Servidor serv = null;
            using (NpgsqlConnection con = new NpgsqlConnection(this.stringConexao))
            {
                con.Open();
                NpgsqlCommand comando = new NpgsqlCommand();
                comando.Connection = con;

                comando.CommandText = "select servidor.id , servidor.nome , servidor.enderecofisico , " +
                                      " servidor.processador, servidor.sistemaoperacional , servidor.macaddress , " +
                                      " servidor.ipaddress , servidor.descricao , sensor.id as sensorid , " +
                                      " sensor.temperatura , sensor.pressao , sensor.altitude , sensor.umidade , sensor.data , sensor.pontoorvalho " +
                                      " from servidor, sensor " +
                                      " where sensor.servidorid = servidor.id " +
                                      " and servidor.id = @id";
                
                comando.Parameters.AddWithValue("id", id);
                var leitor = comando.ExecuteReader();

                while (leitor.Read())
                {
                    serv = new Servidor()
                    {
                        Id = Guid.Parse(leitor["id"].ToString()),
                        Nome = leitor["nome"].ToString(),
                        EnderecoFisico = leitor["enderecofisico"].ToString(),
                        Processador = leitor["processador"].ToString(),
                        SistemaOperacional = leitor["sistemaoperacional"].ToString(),
                        MacAddress = leitor["macaddress"].ToString(),
                        IpAddress = leitor["ipaddress"].ToString(),
                        Descricao = leitor["descricao"].ToString(),

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

                comando.CommandText = "select servidor.id , servidor.nome , servidor.enderecofisico , " +
                                      " servidor.processador, servidor.sistemaoperacional , servidor.macaddress , " +
                                      " servidor.ipaddress , servidor.descricao , sensor.id as sensorid , " +
                                      " sensor.temperatura , sensor.pressao , sensor.altitude , sensor.umidade , sensor.data , sensor.pontoorvalho " +
                                      " from servidor, sensor " +
                                      " where sensor.servidorid = servidor.id ";

                var leitor = comando.ExecuteReader();

                while (leitor.Read())
                {
                    servidores.Add( new Servidor()
                    {
                        Id = Guid.Parse(leitor["id"].ToString()),
                        Nome = leitor["nome"].ToString(),
                        EnderecoFisico = leitor["enderecofisico"].ToString(),
                        Processador = leitor["processador"].ToString(),
                        SistemaOperacional = leitor["sistemaoperacional"].ToString(),
                        MacAddress = leitor["macaddress"].ToString(),
                        IpAddress = leitor["ipaddress"].ToString(),
                        Descricao = leitor["descricao"].ToString(),

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
                };

                return servidores;
            }
        }
    }
}
