using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;


namespace PingPong
{
    class Player
    {
        [JsonIgnore]
        public string id { get; set; }

        public String nom { get; set; }
        public String fotoPath { get; set; }
        public int partitsJugats { get; set; }
        public int punts { get; set; }

        public Player(String nom, String foto)
        {
            this.nom = nom;
            this.fotoPath = foto;
        }
        
    }

}

