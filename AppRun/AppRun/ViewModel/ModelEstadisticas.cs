using AppRun.clases;
using AppRun.Model;
using AppRun.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;

namespace AppRun.ViewModel
{
    public class ModelEstadisticas: BaseViewModel
    {
        List<CarrerasModelApi> carrerasRest;
        RestApiCarreras restApi;
        public string carrera;
        public DateTime fecha;
        public double distancia;
        public double tiempo;
        public double velocidad;
        public double calorias;
        public bool isRunning;
        public bool isVisible;
        public bool isEnabled;


        public bool resfres;
        public bool Refresc
        {
            get { return this.resfres; }
            set { SetValue(ref this.resfres, value); }
        }

        public ModelEstadisticas()
        {
            carrerasRest = new List<CarrerasModelApi>();
            restApi = new RestApiCarreras();
            Refresc = true;
            Corredores();
        }

        public double Velocidad
        {
            get { return this.velocidad; }
            set { SetValue(ref this.velocidad, value); }
        }


    
        public DateTime Fecha
        {
            get { return this.fecha; }
            set { SetValue(ref this.fecha, value); }
        }

        public string Carrera
        {
            get { return this.carrera; }
            set { SetValue(ref this.carrera, value); }
        }
       
        public double Distancia
        {
            get { return this.distancia; }
            set { SetValue(ref this.distancia, value); }
        }

        public double Tiempo
        {
            get { return this.tiempo; }
            set { SetValue(ref this.tiempo, value); }
        }
        public double Calorias
        {
            get { return this.calorias; }
            set { SetValue(ref this.calorias, value); }
        }

        public bool IsVisibleTxt
        {
            get { return this.isVisible; }
            set { SetValue(ref this.isVisible, value); }
        }

        public bool IsEnabledTxt
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }

        public bool IsRunningTxt
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        private List<CarrerasModelApi> lista;
        public List<CarrerasModelApi> Carreras
        {
            get { return this.lista; }
            set
            {
                if (lista != value)
                {
                    SetValue(ref this.lista, value);
                }
            }
        }

        public async void Corredores()
        {
            Refresc = true;
          
        
                carrerasRest = await restApi.GetRepositoriesAsync(Constantes.urlGetCarreras);
                var data = carrerasRest.Where(c=>c.idUser.ToString().Contains(Preferences.Get("id","").ToString()));
                if (data!=null)
                {
                    Carreras = data.ToList();
                    Refresc = false;
                }
                  Refresc = false;

                }
         }

       
           


   }
    

