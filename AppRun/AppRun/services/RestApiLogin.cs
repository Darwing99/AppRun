using AppRun.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppRun.services
{
    class RestApiLogin { 
    HttpClient cliente;

    public RestApiLogin()
    {
        cliente = new HttpClient();

  
    }

    public async Task<List<UsuariosRest>> GetRepositoriesAsync(string url)
    {
        List<UsuariosRest> lista = null;
        try
        {
            HttpResponseMessage respuesta = await cliente.GetAsync(url);
            if (respuesta.IsSuccessStatusCode)
            {
                string informacion = await respuesta.Content.ReadAsStringAsync();
                lista = JsonConvert.DeserializeObject<List<UsuariosRest>>(informacion);

            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error", ex.Message);
        }

        return lista;
    }



        public async Task<List<UsuariosRest>> DeleteTodoItemAsync(string url)
        {
            List<UsuariosRest> lista = null;


            HttpResponseMessage response = await cliente.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
            {

            }
            return lista;
        }

       
        public async Task PostUser(UsuariosRest user, string url)
        {
       
            var json = JsonConvert.SerializeObject(user);
            var contentJSON = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await cliente.PostAsync(url, contentJSON);
           
        }

    }
    
       
}


