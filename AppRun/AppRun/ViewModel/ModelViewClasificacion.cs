using AppRun.clases;
using AppRun.Model;
using AppRun.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppRun.ViewModel
{
   public class ModelViewClasificacion: BaseViewModel
    {
        List<UsuariosRest> usuarios;
        RestApiLogin restUser;
        List<CarrerasModelApi> carrerasRest;
        RestApiCarreras restApi;
    public ModelViewClasificacion()
    {
        Refresc = true;
            carrerasRest = new List<CarrerasModelApi>();
            usuarios = new List<UsuariosRest>();
            restUser = new RestApiLogin();
            restApi = new RestApiCarreras();
            Clasificacion = new List<ClasificacionModel>();
            Corredores();

    }


    public string buscarCorredor;
    public string BuscarCorredores
    {
        get { return this.buscarCorredor; }
        set { SetValue(ref this.buscarCorredor, value); }
    }

    public bool resfres;
    public bool Refresc
    {
        get { return this.resfres; }
        set { SetValue(ref this.resfres, value); }
    }

    private List<CarrerasModelApi> _list;
    public List<CarrerasModelApi> ListItems
    {
        get { return this._list; }
        set
        {
            if (_list != value)
            {
                SetValue(ref this._list, value);
            }
        }
    }



        private List<UsuariosRest> _listOfItems;
        public List<UsuariosRest> ListOfItems
        {
            get { return this._listOfItems; }
            set
            {
                if (_listOfItems != value)
                {
                    SetValue(ref this._listOfItems, value);
                }
            }
        }
        private List<ClasificacionModel> clasificacion;
        public List<ClasificacionModel> Clasificacion
        {
            get { return this.clasificacion; }
            set
            {
                if (clasificacion != value)
                {
                    SetValue(ref this.clasificacion, value);
                }
            }
        }


        public ICommand BuscarCorredor
    {
        get
        {
            return new Command(() => Corredores());
        }
    }
    public async void Corredores()
    {
        Refresc = true;
      
                usuarios = await restUser.GetRepositoriesAsync(Constantes.urlGet);
                var user = usuarios;
               
                carrerasRest = await restApi.GetRepositoriesAsync(Constantes.urlGetCarreras);
                var filtros = carrerasRest.Where(c => c.idUser.ToString().ToLower().Contains(user.FirstOrDefault().ToString().ToLower()));

                    
         
                    var query = from item in filtros where filtros.First().idUser.ToString().Contains(user.First().id.ToString())
                                group item by new { 
                                    calorias = item.calorias, 
                                    name = usuarios.First(). name, 
                                    tiempo = item.tiempohoras, 
                                    velocidad = item.velocidad, 
                                    distancia = item.distancia 
                                } into g select  new
                                {
                                    Key = g.Key,
                                    calorias = g.Sum(x => x.calorias),
                                    distancia = g.Sum(x => x.distancia),
                                    velocidad = g.Sum(x => x.velocidad),
                                    tiempohoras = g.Sum(x => x.tiempohoras),
                                };


                        foreach (var datos in query)
                        {
                                clasificacion.Add(new ClasificacionModel
                                {
                                    id = filtros.First().idUser,
                                    name = usuarios.First().name,
                                    calorias = datos.calorias,
                                    velocidad = datos.velocidad,
                                    distancia = datos.distancia,
                                    tiempohoras = datos.tiempohoras
                                });

                        }
                
            


            Refresc = false;
        
        
    }



}
}
