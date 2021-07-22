using AppRun.clases;
using AppRun.Model;
using AppRun.services;
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
        List<UsuariosRest> service;
        RestApiLogin restService;
        public Settings()
        {
            InitializeComponent();
            restService = new RestApiLogin();
           

        }
      
        
        private async void updateProfile_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UpdateProfile());
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            service = await restService.GetRepositoriesAsync(Constantes.urlGet);
            var listaSeleccionada = service.Where(c => c.idToken.ToString().Contains(Preferences.Get("iduser", "")));
            if (listaSeleccionada != null)
            {
                idusuario.Text =listaSeleccionada.FirstOrDefault().id.ToString();
                nombre.Text = listaSeleccionada.FirstOrDefault().name;
                correo.Text = listaSeleccionada.FirstOrDefault().correo;
                byte[] image = listaSeleccionada.FirstOrDefault().image;
                perfil.ImageSource = ImageSource.FromStream(() => new MemoryStream(image));
            }

        }
        private async void cerrar_Clicked(object sender, EventArgs e)
        {
            Preferences.Clear();
            await Navigation.PushAsync(new Login(),false);
        }
    }
}