using AppRun.clases;
using AppRun.Model;
using AppRun.services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppRun.ViewModel
{
    class CorredoresViewModel: BaseViewModel
    {
        List<UsuariosRest> userRest;
        RestApiLogin restApiLogin;
        public CorredoresViewModel()
        {
            Refresc = true;
            userRest = new List<UsuariosRest>();
            restApiLogin = new RestApiLogin();
            

            Corredores("");
            
        }


        public string buscarCorredor;
        public string BuscarCorredores
        {
            get { return this.buscarCorredor; }
            set { SetValue(ref this.buscarCorredor,value); }
        }

        public bool resfres;
        public bool Refresc
        {
            get { return this.resfres; }
            set { SetValue(ref this.resfres,value); }
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

    

        public ICommand BuscarCorredor
        {
            get
            {
                return new Command(() => Corredores(BuscarCorredores));
            }
        }
        public async void Corredores(string buscar)
        {
            Refresc = true;
            buscar = BuscarCorredores;
            if (string.IsNullOrEmpty(buscar))
            {
                userRest = await restApiLogin.GetRepositoriesAsync(Constantes.urlGet);
                ListOfItems = userRest;
                Refresc = false;
            }
            else
            {
                userRest = await restApiLogin.GetRepositoriesAsync(Constantes.urlGet);
                var corredores =  userRest.Where(c=>c.name.ToString().Contains(buscar.ToString()));
                ListOfItems = corredores.ToList();
                Refresc = false;
            }

            
        }



    }
}
