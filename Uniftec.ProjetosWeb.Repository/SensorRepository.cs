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
    public class SensorRepository : ISensorRepository
    {
        private string stringConexao;

        public SensorRepository(String stringConexao)
        {
            this.stringConexao = stringConexao;
        }

        public void Alterar(Sensor sensor)
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

                        //Alterar o Sensor
                        comando.CommandText = "UPDATE public.sensor " +
                                              "SET sensorid=@sensorid, temperatura=@temperatura, pressao=@pressao, altitude=@altitude, umidade=@umidade, data=@data, pontoorvalho=@pontoorvalho, macaddress_servidor=@macaddress_servidor " +
                                              "WHERE sensorid=@sensorid;";

                        comando.Parameters.AddWithValue("sensorid", sensor.Id);
                        comando.Parameters.AddWithValue("temperatura", sensor.Temperatura);
                        comando.Parameters.AddWithValue("pressao", sensor.Pressao);
                        comando.Parameters.AddWithValue("altitude", sensor.Altitude);
                        comando.Parameters.AddWithValue("umidade", sensor.Umidade);
                        comando.Parameters.AddWithValue("data", sensor.Data);
                        comando.Parameters.AddWithValue("pontoorvalho", sensor.PontoOrvalho);
                        comando.Parameters.AddWithValue("macaddress_servidor", sensor.MacAddressServidor);

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

                        //Excluir o Sensor
                        comando.CommandText = "DELETE FROM public.sensor WHERE sensorid=@sensorid;";
                        comando.Parameters.AddWithValue("sensorid", id);

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

        public void Inserir(Sensor sensor)
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

                        //Inserir o Sensor
                        comando.CommandText = "INSERT INTO public.sensor " +
                                               " (sensorid, temperatura, pressao, altitude, umidade, data, pontoorvalho, macaddress_servidor) " +
                                               " VALUES(@sensorid, @temperatura, @pressao, @altitude, @umidade, @data, @pontoorvalho, @macaddress_servidor);";

                        comando.Parameters.AddWithValue("sensorid", sensor.Id);
                        comando.Parameters.AddWithValue("temperatura", sensor.Temperatura);
                        comando.Parameters.AddWithValue("pressao", sensor.Pressao);
                        comando.Parameters.AddWithValue("altitude", sensor.Altitude);
                        comando.Parameters.AddWithValue("umidade", sensor.Umidade);
                        comando.Parameters.AddWithValue("data", sensor.Data);
                        comando.Parameters.AddWithValue("pontoorvalho", sensor.PontoOrvalho);
                        comando.Parameters.AddWithValue("macaddress_servidor", sensor.MacAddressServidor);

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

        public Sensor Selecionar(Guid id)
        {
            Sensor sen = null;
            using (NpgsqlConnection con = new NpgsqlConnection(this.stringConexao))
            {
                con.Open();
                NpgsqlCommand comando = new NpgsqlCommand();
                comando.Connection = con;

                comando.CommandText = "SELECT * FROM sensor " +
                                      "WHERE sensor.sensorid = @sensorid";
                
                comando.Parameters.AddWithValue("sensorid", id);
                var leitor = comando.ExecuteReader();

                while (leitor.Read())
                {
                    sen = new Sensor()
                    {
                        Id = Guid.Parse(leitor["sensorid"].ToString()),
                        Temperatura = float.Parse(leitor["temperatura"].ToString()),
                        Pressao = float.Parse(leitor["pressao"].ToString()),
                        Altitude = float.Parse(leitor["altitude"].ToString()),
                        Umidade = float.Parse(leitor["umidade"].ToString()),
                        Data = Convert.ToDateTime(leitor["data"]),
                        PontoOrvalho = float.Parse(leitor["pontoorvalho"].ToString()),
                        MacAddressServidor = leitor["macaddress_servidor"].ToString()
                    };
                };

                return sen;
            }
        }

        public List<Sensor> SelecionarTodos()
        {
            List<Sensor> sensores = new List<Sensor>();
            using (NpgsqlConnection con = new NpgsqlConnection(this.stringConexao))
            {
                con.Open();
                NpgsqlCommand comando = new NpgsqlCommand();
                comando.Connection = con;

                comando.CommandText = "SELECT * FROM sensor";

                var leitor = comando.ExecuteReader();

                while (leitor.Read())
                {
                    sensores.Add( new Sensor()
                    {
                        Id = Guid.Parse(leitor["sensorid"].ToString()),
                        Temperatura = float.Parse(leitor["temperatura"].ToString()),
                        Pressao = float.Parse(leitor["pressao"].ToString()),
                        Altitude = float.Parse(leitor["altitude"].ToString()),
                        Umidade = float.Parse(leitor["umidade"].ToString()),
                        Data = Convert.ToDateTime(leitor["data"]),
                        PontoOrvalho = float.Parse(leitor["pontoorvalho"].ToString()),
                        MacAddressServidor = leitor["macaddress_servidor"].ToString()
                    });
                };

                return sensores;
            }
        }
    }
}
