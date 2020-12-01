using Ftec.WebAPI.Domain.Entities;
using Ftec.WebAPI.Domain.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uniftec.ProjetosWeb.Domain.Entities;

namespace Ftec.WebAPI.Infra.Repository
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
                            CommandText = @"DELETE FROM usuarios WHERE Id=@Id"
                        };
                        cmd.Parameters.AddWithValue("Id", id);
                        cmd.ExecuteNonQuery();
                        //commit na transação
                        trans.Commit();
                        return true;

                    }
                    catch (Exception ex)
                    {
                        //rollback da transação
                        trans.Rollback();
                        throw ex;
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
                cmd.CommandText = @"SELECT * FROM Usuarios WHERE Id=@Id";
                cmd.Parameters.AddWithValue("Id", id);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    cliente = new Cliente
                    {
                        Id = Guid.Parse(reader["id"].ToString()),
                        Password = reader["password"].ToString(),
                        UserName = reader["username"].ToString()
                    };
                }
                reader.Close();

                return cliente;
            }
        }

        public Cliente Find(string email)
        {
            Cliente cliente = null;

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();

                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"SELECT * FROM Usuarios WHERE username=@username";
                cmd.Parameters.AddWithValue("username", email);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    cliente = new Cliente();
                    cliente.Id = Guid.Parse(reader["id"].ToString());
                    cliente.Password = reader["password"].ToString();
                    cliente.UserName = reader["username"].ToString();
                }
                reader.Close();

                return cliente;
            }
        }

        public List<Cliente> FindAll()
        {
            List<Cliente> usuarios = new List<Cliente>();

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();

                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"SELECT * FROM Usuarios";
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Cliente cliente = new Cliente();
                    cliente.Id = Guid.Parse(reader["id"].ToString());
                    cliente.Password = reader["password"].ToString();
                    cliente.UserName = reader["username"].ToString();

                    cliente.Add(cliente);
                }
                return cliente;
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
                            CommandText = @"INSERT Into Usuarios (id, username, password) values(@id, @username, @password)"
                        };
                        cmd.Parameters.AddWithValue("id", usuario.Id);
                        cmd.Parameters.AddWithValue("username", usuario.UserName);
                        cmd.Parameters.AddWithValue("password", usuario.Password);
                        cmd.ExecuteNonQuery();

                        //commit na transação
                        trans.Commit();
                        return cliente.Id;

                    }
                    catch (Exception ex)
                    {
                        //rollback da transação
                        trans.Rollback();
                        throw ex;
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
                        cmd.CommandText = @"UPDATE Usuarios SET username=@username, password=@password WHERE Id=@id";
                        cmd.Parameters.AddWithValue("id", cliente.Id);
                        cmd.Parameters.AddWithValue("username", cliente.UserName);
                        cmd.Parameters.AddWithValue("password", cliente.Password);
                        cmd.ExecuteNonQuery();

                        //commit na transação
                        trans.Commit();
                        return cliente.Id;

                    }
                    catch (Exception ex)
                    {
                        //rollback da transação
                        trans.Rollback();
                        throw ex;
                    }
                }
            }
        }
    }
}
