using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace PingPong
{
    class Partit
    {
        [JsonIgnore]
        String p { get; set; }
        String jugador1 { get; set; }
        String jugador2 { get; set; }
        String punts1 { get; set; }
        String punts2 { get; set; }
    }
}
