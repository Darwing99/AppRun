using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppRun.Model
{
    public class ClasificacionModel
    {

        public int id { get; set; }
        public string name { get; set; }
        public string pais { get; set; }
        public double distancia { get; set; }
        public double tiempo { get; set; }
        public int idUser { get; set; }
        public double velocidad { get; set; }
        public double tiempohoras { get; set; }
        public double calorias { get; set; }
    }
}
