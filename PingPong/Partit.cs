using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace PingPong
{
    class Partit
    {
        [JsonIgnore]
        public String id { get; set; }

        public String jugador1 { get; set; }
        public String nom1 { get; set; }
        public String jugador2 { get; set; }
        public String nom2 { get; set; }
        public int punts1 { get; set; }
        public int punts2 { get; set; }
        public String nGuanyador { get; set; }
        public String idGuanyador { get; set; }

        public Partit(String jugador1, String nom1, String jugador2, String nom2)
        {
            this.jugador1 = jugador1;
            this.jugador2 = jugador2;
            this.nom1 = nom1;
            this.nom2 = nom2;
        }

        public void setGanador()
        {
            if(punts1 > punts2)
            {
                this.idGuanyador = jugador1;
                this.nGuanyador = nom1;
            }else if (punts1 < punts2)
            {
                this.idGuanyador = jugador2;
                this.nGuanyador = nom2;
            }
            else
            {
                this.idGuanyador = "empat";
                this.nGuanyador = "";
            }
        }
    }
}
