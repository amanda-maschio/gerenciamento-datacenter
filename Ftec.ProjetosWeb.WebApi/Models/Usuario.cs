using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Ftec.ProjetosWeb.WebApi.Models
{
    public class Usuario
    {
        public Guid Id { get; set; }

        public string PrimeiroNome { get; set; }

        public string SegundoNome { get; set; }

        public string Funcao { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public string Token { get; set; }

        public List<Servidor> ListaServidores { get; set; }

        public Usuario()
        {
            Id = Guid.NewGuid();
            this.ListaServidores = new List<Servidor>();
        }

        public void GerarHashMd5()
        {
            MD5 md5Hash = MD5.Create();
            // Converter a String para array de bytes, que é como a biblioteca trabalha.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(Senha));

            // Cria-se um StringBuilder para recompôr a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop para formatar cada byte como uma String em hexadecimal
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            Senha = sBuilder.ToString();
        }

    }
}