using AppRun.clases;
using AppRun.Model;
using AppRun.modulos;
using AppRun.services;
using AppRun.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppRun
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
      
        public Settings()
        {
            InitializeComponent();
            BindingContext = new UpdateViewModel();
           
        }
      
        
       
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
        
            await Navigation.PushAsync(new UpdateCorreo());
          
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            
            await Navigation.PushAsync(new UpdateName());
            
        }

        private async void resetPassword_Tapped(object sender, EventArgs e)
        { 
            
            await Navigation.PushAsync(new ResetPassword());
           
        }

        private async void resetFoto_Clicked(object sender, EventArgs e)
        { 
           
            await Navigation.PushAsync(new UpdateFoto());
           
        }

        private async void cerrar_Clicked(object sender, EventArgs e)
        {
            Preferences.Clear();
           
            await Navigation.PushAsync(new Login(),true);
          
            NavigationPage.SetHasNavigationBar(this, false);
        }
        
    }
}