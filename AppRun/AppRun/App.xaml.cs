using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using AppRun.services;



namespace AppRun
{
    
    public partial class App : Application
    {
        IFirebaseAuthentication auth;
        public App()
        {
            InitializeComponent();
        
            if (Preferences.Get("Rememberme",true)!=false && (Preferences.Get("correo", "") != "") && (Preferences.Get("iduser", "") != ""))
            {
              
                MainPage = new NavigationPage(new Inicio());
               //{ BarBackgroundColor = Color.OrangeRed, BarTextColor = Color.Black };
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
