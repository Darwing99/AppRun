using AppRun.Model;
using AppRun.services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppRun.ViewModel
{
    class RegistrarCarreraViewModelBaseViewModel: BaseViewModel
    {
        List<CarrerasModelApi> userRest;
        RestApiCarreras restApi;
        public RegistrarCarreraViewModelBaseViewModel()
        {
            Refresc = true;
            userRest = new List<CarrerasModelApi>();
            RestApiCarreras restApi;
              new RestApiCarreras();

        }


        public string _carrera;
        public string Carrera
        {
            get { return this._carrera; }
            set { SetValue(ref this._carrera, value); }
        }
        public DateTime _tiempo;
        public DateTime Tiempo
        {
            get { return this._tiempo; }
            set { SetValue(ref this._tiempo, value); }
        }
        public DateTime _fecha;
        public DateTime Fecha
        {
            get { return this._fecha; }
            set { SetValue(ref this._fecha, value); }
        }
        public double _distancia;
        public double Distancia
        {
            get { return this._distancia; }
            set { SetValue(ref this._distancia, value); }
        }

        
        public string _idUser;
        public string IdUser
        {
            get { return this._idUser; }
            set { SetValue(ref this._idUser, value); }
        }

        public bool resfres;
        public bool Refresc
        {
            get { return this.resfres; }
            set { SetValue(ref this.resfres, value); }
        }

       



        public ICommand RegistrarCarrera
        {
            get
            {
                return new Command(() => GuardarCarrera());
            }
        }
      
        public async void GuardarCarrera()
        {

        }


    }
}
