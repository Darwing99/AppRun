using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;
using AppRun.services;

namespace AppRun
{
    public partial class App : Application
    {
        IFirebaseAuthentication auth;
        public App()
        {
            InitializeComponent();
          // { BarBackgroundColor = Color.OrangeRed, BarTextColor = Color.Black };
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
