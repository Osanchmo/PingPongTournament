using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace PingPong
{
    class jugador
    {
        [JsonIgnore]
        string id { get; set; }
        String nom { get; set; }
        String fotoPath { get; set; }
        int partitsJugats { get; set; }
        int punts { get; set; }
    }
    public jugador(String nom, String foto)
    {
        Nom = nom;
        Foto = foto;
    }
}
