using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uniftec.ProjetosWeb.Domain.Entities;
using Uniftec.ProjetosWeb.Domain.Repository;

namespace Uniftec.ProjetosWeb.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private string connectionString;

        public ClienteRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool Delete(Guid id)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                //Inicia a transação
                using (var trans = con.BeginTransaction())
                {
                    try
                    {
                        NpgsqlCommand cmd = new NpgsqlCommand
                        {
                            Connection = con,
                            Transaction = trans,
                            CommandText = @"DELETE FROM cliente WHERE Id=@Id"
                        };
                        cmd.Parameters.AddWithValue("Id", id);
                        cmd.ExecuteNonQuery();
                        //commit na transação
                        trans.Commit();
                        return true;

                    }
                    catch (Exception e)
                    {
                        //rollback da transação
                        trans.Rollback();
                        throw e;
                    }
                }
            }
        }

        public Cliente Find(Guid id)
        {
            Cliente cliente = null;

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();

                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"SELECT * FROM cliente WHERE Id=@Id";
                cmd.Parameters.AddWithValue("Id", id);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    cliente = new Cliente
                    {
                        Id = Guid.Parse(reader["id"].ToString()),
                        Password = reader["password"].ToString(),
                        Username = reader["username"].ToString()
                    };
                }
                reader.Close();

                return cliente;
            }
        }

        public Cliente Find(string username)
        {
            Cliente cliente = null;

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();

                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"SELECT * FROM cliente WHERE username=@username";
                cmd.Parameters.AddWithValue("username", username);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    cliente = new Cliente();
                    cliente.Id = Guid.Parse(reader["id"].ToString());
                    cliente.Password = reader["password"].ToString();
                    cliente.Username = reader["username"].ToString();
                }
                reader.Close();

                return cliente;
            }
        }

        public List<Cliente> FindAll()
        {
            List<Cliente> clientes = new List<Cliente>();

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();

                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"SELECT * FROM cliente";
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Cliente cliente = new Cliente();
                    cliente.Id = Guid.Parse(reader["id"].ToString());
                    cliente.Password = reader["password"].ToString();
                    cliente.Username = reader["username"].ToString();

                    clientes.Add(cliente);
                }
                return clientes;
            }

        }

        public Guid Insert(Cliente cliente)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                //Inicia a transação
                using (var trans = con.BeginTransaction())
                {
                    try
                    {
                        NpgsqlCommand cmd = new NpgsqlCommand
                        {
                            Connection = con,
                            Transaction = trans,
                            CommandText = @"INSERT into cliente (id, username, password) values(@id, @username, @password)"
                        };
                        cmd.Parameters.AddWithValue("id", cliente.Id);
                        cmd.Parameters.AddWithValue("username", cliente.Username);
                        cmd.Parameters.AddWithValue("password", cliente.Password);
                        cmd.ExecuteNonQuery();

                        //commit na transação
                        trans.Commit();
                        return cliente.Id;

                    }
                    catch (Exception e)
                    {
                        //rollback da transação
                        trans.Rollback();
                        throw e;
                    }
                }
            }
        }

        public Guid Update(Cliente cliente)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                //Inicia a transação
                using (var trans = con.BeginTransaction())
                {
                    try
                    {
                        NpgsqlCommand cmd = new NpgsqlCommand();
                        cmd.Connection = con;
                        cmd.Transaction = trans;
                        cmd.CommandText = @"UPDATE cliente SET username=@username, password=@password WHERE Id=@id";
                        cmd.Parameters.AddWithValue("id", cliente.Id);
                        cmd.Parameters.AddWithValue("username", cliente.Username);
                        cmd.Parameters.AddWithValue("password", cliente.Password);
                        cmd.ExecuteNonQuery();

                        //commit na transação
                        trans.Commit();
                        return cliente.Id;

                    }
                    catch (Exception e)
                    {
                        //rollback da transação
                        trans.Rollback();
                        throw e;
                    }
                }
            }
        }
    }
}
