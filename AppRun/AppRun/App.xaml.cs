using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using AppRun.services;
using AppRun.clases;




namespace AppRun
{
    
    public partial class App : Application
    {
        IFirebaseAuthentication auth;
        public App()
        {
            InitializeComponent();
            GoogleMapsApiService.Initialize(Constantes.GoogleMapsApiKey);
            if (Preferences.Get("Rememberme",true)!=false && (Preferences.Get("correo", "") != "") && (Preferences.Get("iduser", "") != ""))
            {
              
                MainPage = new NavigationPage(new Inicio());
              
              
             
            }
            else
            {
                MainPage = new NavigationPage(new Login());
            }
           
            auth = DependencyService.Get<IFirebaseAuthentication>();
        }

        protected override void OnStart()
        {
         

        }
        
        protected override void OnSleep()
        {
          
        }

        protected override void OnResume()
        {
          
        }
        
    }
}
