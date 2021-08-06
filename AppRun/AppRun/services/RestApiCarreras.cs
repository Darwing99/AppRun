using AppRun.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppRun.services
{
    public class RestApiCarreras
    {


        HttpClient cliente;

        public RestApiCarreras()
        {
            cliente = new HttpClient();
        }
        public async Task<List<CarrerasModelApi>> GetRepositoriesAsync(string url)
        {
            List<CarrerasModelApi> lista = null;
            try
            {
                HttpResponseMessage respuesta = await cliente.GetAsync(url);
                if (respuesta.IsSuccessStatusCode)
                {
                    string informacion = await respuesta.Content.ReadAsStringAsync();
                    lista = JsonConvert.DeserializeObject<List<CarrerasModelApi>>(informacion);

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error", ex.Message);
            }

            return lista;
        }



        public async Task<List<CarrerasModelApi>> DeleteTodoItemAsync(string url)
        {
            List<CarrerasModelApi> lista = null;


            HttpResponseMessage response = await cliente.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
            {

            }
            return lista;
        }


     

    }



}
