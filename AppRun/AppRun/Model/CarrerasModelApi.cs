using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppRun.Model
{
  
    public class CarrerasModelApi
    {
        [JsonProperty("idToken")]
        public int idToken { get; set; }
        [JsonProperty("carrera")]
        public object carrera { get; set; }
        [JsonProperty("fecha")]
        public object fecha { get; set; }
        [JsonProperty("distancia")]
        public int distancia { get; set; }
        [JsonProperty("fotos")]
        public int fotos { get; set; }
        [JsonProperty("tiempo")]
        public int tiempo { get; set; }
        [JsonProperty("estado")]
        public bool estado { get; set; }
        [JsonProperty("idUser")]
        public int idUser { get; set; }
    }


}
