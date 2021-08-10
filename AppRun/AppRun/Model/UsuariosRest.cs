using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppRun.Model
{
     public class UsuariosRest
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("idToken")]
        public string idToken { get; set; }
        [JsonProperty("tokenfirebase")]
        public string tokenfirebase { get; set; }
        [JsonProperty("correo")]
        public string correo { get; set; }
        [JsonProperty("pais")]
        public string pais { get; set; }
        [JsonProperty("fecha")]
        public DateTime fecha { get; set; }
        [JsonProperty("image")]
        public byte[] image { get; set; }
        [JsonProperty("direccion")]
        public string direccion { get; set; }
       
        [JsonProperty("password")]
        public string password { get; set; }
        [JsonProperty("estado")]
        public bool estado { get; set; }
    }
}
