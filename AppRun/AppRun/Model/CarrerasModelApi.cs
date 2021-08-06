using Newtonsoft.Json;
using System;


namespace AppRun.Model
{
  
    public class CarrerasModelApi
    {
        [JsonProperty("idToken")]
        public int idToken { get; set; }
        [JsonProperty("carrera")]
        public string carrera { get; set; }
        [JsonProperty("fecha")]
        public DateTime fecha { get; set; }
        [JsonProperty("distancia")]
        public double distancia { get; set; }
        [JsonProperty("fotos")]
        public byte[] fotos { get; set; }
        [JsonProperty("tiempo")]
        public double tiempo { get; set; }
        [JsonProperty("estado")]
        public bool estado { get; set; }
        [JsonProperty("idUser")]
        public int idUser { get; set; }
    }


}
