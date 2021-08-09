using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AppRun.clases;
using Plugin.FilePicker;
using AppRun.Firebase;
using AppRun.Model;

using Firebase.Auth;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using AppRun.services;
using System.Threading;

namespace AppRun.ViewModel
{
    class RegistrarViewModel : BaseViewModel
    {
      
      
        public RegistrarViewModel()
        {
          
            this.IsEnabledTxt = true;
            Camarabtn = "camera.png";
          
        }

        FirebaseHelp firebase = new FirebaseHelp();

        public string email;
        public string password;
        public string name;
        public string confpassword;
        public string _paisSeleccionado;
        public byte[] miperfil;
        public bool isRunning;
        public bool isVisible;
        public bool isEnabled;
        public ImageSource camera;

        public string paisSeleccionado
        {
            get { return this._paisSeleccionado; }
            set { SetValue(ref this._paisSeleccionado, value); }
        }


        public ImageSource Camarabtn
        {
            get { return this.camera; }
            set { SetValue(ref this.camera, value); }
        }

        public string EmailTxt
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }

        public string PasswordTxt
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }
        public byte[] MiPerfil
        {
            get { return this.miperfil; }
            set { SetValue(ref this.miperfil, value); }
        }
        public string NameTxt
        {
            get { return this.name; }
            set { SetValue(ref this.name, value); }
        }

        public string conPasswordTxt
        {
            get { return this.confpassword; }
            set { SetValue(ref this.confpassword, value); }
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


        public ICommand RegisterCommand
        {
            get
            {
                return new Command(() => validarDatos());
            }
        }
        public async void validarDatos()
        {
            this.IsVisibleTxt = true;
            this.IsRunningTxt = true;
            if (string.IsNullOrEmpty(this.email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Alert",
                    "Escribe tu Email.",
                    "Ok");
                return;
            }

            if (string.IsNullOrEmpty(this.password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Alert",
                    "Escribe tu Contraseña.",
                    "Ok");
                return;
            }
            if (string.IsNullOrEmpty(this.name))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Alert",
                    "Escribe tu nombre.",
                    "ok");
                return;
            }
            if (string.IsNullOrEmpty(this.confpassword))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Alert",
                    "Confirmar tu contraseña.",
                    "ok");
                return;
            }
            if (this.confpassword != this.password)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Alert",
                    "Las contraseñas son distintas",
                    "Ok!");
                return;

            }

            try
            {

                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Constantes.ApiKey));
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(EmailTxt.ToString(), PasswordTxt.ToString());
                string gettoken = auth.FirebaseToken;
              
                this.IsEnabledTxt = false;

                string idtoken = auth.FirebaseToken;

                var user = new UsuariosRest
                {
                    id = 0,
                    idToken = auth.User.LocalId,
                    tokenfirebase = idtoken,
                    correo = auth.User.Email,
                    pais=paisSeleccionado.ToString(),
                    name = NameTxt.ToString(),
                    fecha = auth.Created.Date,
                    estado = true,
                    password = PasswordTxt.ToString(),
                    image = MiPerfil,
                    direccion = "",

                };


                var client = new HttpClient();
                var json = JsonConvert.SerializeObject(user);
                var contentJSON = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(Constantes.urlPost, contentJSON);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    await App.Current.MainPage.DisplayAlert("Registro", "Se creo tu cuenta", "OK");
                    this.IsRunningTxt = false;
                    await Application.Current.MainPage.Navigation.PushAsync(new Login());
                }
                else
                {
                    this.IsRunningTxt = false;
                    await App.Current.MainPage.DisplayAlert("Registro", "No se pudo crear la cuenta", "OK");
                    var authdelete = new FirebaseAuthProvider(new FirebaseConfig(Constantes.ApiKey));
                    var delete = authdelete.DeleteUserAsync(idtoken);
                    idtoken = "";
                }

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Registro", "Este usuario ya está registrado", "OK");
            }


        }

        public ICommand CameraCommand
        {
            get
            {
                return new Command(() => TomarSeleccionarFoto());
            }
        }
        async void TomarSeleccionarFoto()
        {
            string action = await App.Current.MainPage.DisplayActionSheet("Perfil de Usuario", "Cancel", null, "Cámara", "Seleccionar de la galería");
            switch (action)
            {
                case "Cámara": Camara();
                   
                    break;
                case "Seleccionar de la galería": selectFile();
                    break;
            }
        }
        //paises 

      
        public async void selectFile()
        {
            string[] fileTypes = null;

            if (Device.RuntimePlatform == Device.Android)
            {
                fileTypes = new string[] { "image/png", "image/jpeg", "image/jpg" };
            }
            await PickAndShowFile(fileTypes);
        }

        public async void Camara()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("No Camera", "Camara no disponible", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "AppRun",
                Name = "perfil",
                SaveToAlbum = true,
                CompressionQuality = 75,
                CustomPhotoSize = 50,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 2000,
                DefaultCamera = CameraDevice.Front
            });

            if (file == null)
                return;



            if (file != null)
            {

                Camarabtn = ImageSource.FromStream(() => file.GetStream());

                using (MemoryStream memory = new MemoryStream())
                {

                    Stream stream = file.GetStream();
                    stream.CopyTo(memory);
                    miperfil = memory.ToArray();
                }
            }

        }
        public async Task PickAndShowFile(string[] fileTypes)
        {

            var file = await CrossFilePicker.Current.PickFile(fileTypes);

            if (file != null)
            {

                Camarabtn = file.FileName;

                if (file.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase)
                       || file.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase) || file.FileName.EndsWith("jpeg", StringComparison.OrdinalIgnoreCase))
                {
                    Camarabtn = ImageSource.FromStream(() =>
                    {
                        return file.GetStream();
                    });

                    using (MemoryStream memory = new MemoryStream())
                    {

                        Stream stream = file.GetStream();
                        stream.CopyTo(memory);
                        miperfil = memory.ToArray();
                    }

                }

            }



        }

        
    }
}
