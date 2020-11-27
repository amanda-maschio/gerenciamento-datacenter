using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ftec.ProjetosWeb.WebApi.Models
{
    public class Sensor
    {
        public Guid Id { get; set; }

        public float Temperatura { get; set; }

        public float Pressao { get; set; }

        public float Altitude { get; set; }

        public float Umidade { get; set; }

        public DateTime Data { get; set; }

        public Sensor()
        {
            Id = Guid.NewGuid();
        }
    }
}