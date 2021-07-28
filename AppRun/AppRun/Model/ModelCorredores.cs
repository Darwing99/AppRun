using System;

using Xamarin.Forms;

namespace AppRun.Model
{
    public class ModelCorredores
    {
      public ModelCorredores()
        {

        }
        public int id { get; set; }
    
        public string name { get; set; }
  
        public string idToken { get; set; }

        public string tokenfirebase { get; set; }

        public string correo { get; set; }
     
        public DateTime fecha { get; set; }
   
        public byte[] image { get; set; }
        public ImageSource foto { get; set; }
        public string direccion { get; set; }

        public string password { get; set; }

        public bool estado { get; set; }
    }
}
