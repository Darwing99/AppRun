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
     class CountryService
    {
        HttpClient cliente;

        public CountryService()
        {
            cliente = new HttpClient();


        }

        public async Task<List<ModelCountry>> GetRepositoriesAsync(string url)
        {
            List<ModelCountry> lista = null;
            try
            {
                HttpResponseMessage respuesta = await cliente.GetAsync(url);
                if (respuesta.IsSuccessStatusCode)
                {
                    string informacion = await respuesta.Content.ReadAsStringAsync();
                    lista = JsonConvert.DeserializeObject<List<ModelCountry>>(informacion);

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error", ex.Message);
            }

            return lista;
        }
    }
}
