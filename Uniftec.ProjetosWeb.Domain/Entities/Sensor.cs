using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Uniftec.ProjetosWeb.Domain.Entities
{
    public class Sensor
    {
        public Guid Id { get; set; }

        public float Temperatura { get; set; }

        public float Pressao { get; set; }

        public float Altitude { get; set; }

        public float Umidade { get; set; }

        public DateTime Data { get; set; }

        public float PontoOrvalho { get; set; }

        public string MacAddressServidor { get; set; }

        public Sensor()
        {
            Id = Guid.NewGuid();
        }

        public void CalculoPontoOrvalho()
        {
            //Fórmula ponto de orvalho: ((UR/100)^(1/8)) * (112+(0,9*T)) + (0,1*T) - 112
            PontoOrvalho = (float)(Math.Pow((Umidade / 100), (1 / 8)) * (112 + (0.9 * Temperatura)) + (0.1 * Temperatura) - 112);
        }
    }
}