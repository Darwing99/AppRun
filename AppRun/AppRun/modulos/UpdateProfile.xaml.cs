using AppRun.clases;
using AppRun.Model;
using AppRun.services;
using Firebase.Auth;
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
    public partial class UpdateProfile : ContentPage
    {
        List<UsuariosRest> service;
        string tokenfirebase;
        RestApiLogin restService;
        public UpdateProfile()
        {
            InitializeComponent();
            restService = new RestApiLogin();

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            service = await restService.GetRepositoriesAsync(Constantes.urlGet);
            var listaSeleccionada = service.Where(c => c.idToken.ToString().Contains(Preferences.Get("iduser", "")));
            if (listaSeleccionada != null)
            {
                tokenfirebase = listaSeleccionada.FirstOrDefault().tokenfirebase;
                id.Text = listaSeleccionada.FirstOrDefault().id.ToString();
                userupdate.Text = listaSeleccionada.FirstOrDefault().name;
                correoupdate.Text = listaSeleccionada.FirstOrDefault().correo;
                byte[] image = listaSeleccionada.FirstOrDefault().image;
                perfil.ImageSource = ImageSource.FromStream(() => new MemoryStream(image));
            }

        }


      /*  public async void Update()
        {

            if (string.IsNullOrEmpty(userupdate.Text))
            {
                return;
            }
            if (string.IsNullOrEmpty(newpassword.Text))
            {
                return;
            }
            if (string.IsNullOrEmpty(cnewpassword.Text))
            {
                return;
            }
            else
            {
                var authUpdate = new FirebaseAuthProvider(new FirebaseConfig(Constantes.ApiKey));
                var updateEmail = authUpdate.ChangeUserEmail(tokenfirebase, newEmail.Text);
                var updateContrasenia = authUpdate.ChangeUserPassword(tokenfirebase, newpassword.Text);
            }
         


        }*/
    }
}