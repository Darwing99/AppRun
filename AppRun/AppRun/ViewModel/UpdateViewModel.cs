using AppRun.clases;
using AppRun.Model;
using AppRun.services;
using Firebase.Auth;
using Newtonsoft.Json;
using Plugin.FilePicker;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppRun.ViewModel
{
    public class UpdateViewModel: BaseViewModel
    {
        List<UsuariosRest> service;
        RestApiLogin restService;
        public UpdateViewModel()
        {
            restService = new RestApiLogin();
            idTokenPreferencias = Preferences.Get("iduser", "");
            nombrePreferencias = Preferences.Get("nombre", "");
            emailPrefencencias = Preferences.Get("correo", "");
            Camarabtn = "";
         
            DatosInformacionUsuario();

           
        }
        public bool barprogreso;
        public bool Progress
        {
            get { return this.barprogreso; }
            set { SetValue(ref this.barprogreso, value); }
        }
        public bool runningprogreso;
        public bool RunningProgress
        {
            get { return this.runningprogreso; }
            set { SetValue(ref this.runningprogreso, value); }
        }


        //datos de informacion traidas de preferencias y Api rest
        public string passwordactual;
        public string PasswordActual
        {
            get { return this.passwordactual; }
            set { SetValue(ref this.passwordactual, value); }
        }
        public string id;
        public string Id
        {
            get { return this.id; }
            set { SetValue(ref this.id, value); }
        }
        public string idtoken;
        public string IdToken
        {
            get { return this.idtoken; }
            set { SetValue(ref this.idtoken, value); }
        }
        public string idtokenfirebase;
        public string IdTokenFirebase
        {
            get { return this.idtokenfirebase; }
            set { SetValue(ref this.idtokenfirebase, value); }
        }
        public string idPreferencias;
        public string IdPreferencias
        {
            get { return this.idPreferencias; }
            set { SetValue(ref this.idPreferencias, value); }
        }
        public string idTokenPreferencias;

        public string IdTokenPreferencias
        {
            get { return this.idTokenPreferencias; }
            set { SetValue(ref this.idTokenPreferencias, value); }
        }
        public string emailPrefencencias;
        public string EmailPreferencias
        {
            get { return this.emailPrefencencias; }
            set { SetValue(ref this.emailPrefencencias, value); }
        }
        public DateTime fecha;
        public DateTime Fecha
        {
            get { return this.fecha; }
            set { SetValue(ref this.fecha, value); }
        }

        public string direccion;
        public string Direccion
        {
            get { return this.direccion; }
            set { SetValue(ref this.direccion, value); }
        }

        public string nombrePreferencias;
        public string NombrePreferencias
        {
            get { return this.nombrePreferencias; }
            set { SetValue(ref this.nombrePreferencias, value); }
        }
        public ImageSource perfilRest;
        public ImageSource PerfilRest
        {
            get { return this.perfilRest; }
            set { SetValue(ref this.perfilRest, value); }
        }
        public byte[] perfilbyte;
        public byte[] PerfilByte
        {
            get { return this.perfilbyte; }
            set { SetValue(ref this.perfilbyte, value); }
        }
        public ICommand InformacionUsuario { private get; set; }

        public async void DatosInformacionUsuario()
        {
            if (FotoNuevaByte != null)
           
            {
                return;
            }
            else
            {
               
                service = await restService.GetRepositoriesAsync(Constantes.urlGet);
                if (service != null)
                {
                    var listaSeleccionada = service.Where(c => c.idToken.ToString().Contains(idTokenPreferencias.ToString()));

                    if (listaSeleccionada != null)
                    {
                        id = listaSeleccionada.FirstOrDefault().id.ToString();
                        idtoken = listaSeleccionada.FirstOrDefault().idToken;
                        passwordactual = listaSeleccionada.FirstOrDefault().password;
                        fecha = listaSeleccionada.FirstOrDefault().fecha;
                        perfilbyte = listaSeleccionada.FirstOrDefault().image;
                        PerfilRest = ImageSource.FromStream(() => new MemoryStream(perfilbyte));
                        if (Camarabtn.IsEmpty)
                        {
                            PerfilRest = ImageSource.FromStream(() => new MemoryStream(perfilbyte));
                            return;
                        }
                        else
                        {
                            PerfilRest = Camarabtn;
                        }

                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "No se pudo actualizar la foto", "Ok");
                    }
                }
            
                
              
            }
          
        }
       

        //fin de informacion de usuario

       
        public string email;
        public string password;
        public string name;
        public string confpassword;
        //public byte[] miperfil;
   
        public ImageSource camera;
        public ImageSource Camarabtn
        {
            get { return this.camera; }
            set { SetValue(ref this.camera, value); }
        }

        public string EmailUpdate
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }

        public string PasswordUpdate
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }

        public string NameUpdate
        {
            get { return this.name; }
            set { SetValue(ref this.name, value); }
        }

        public string conPasswordUpdate
        {
            get { return this.confpassword; }
            set { SetValue(ref this.confpassword, value); }
        }
        public byte[] fotonuevabyte;
        public byte[] FotoNuevaByte
        {
            get { return this.fotonuevabyte; }
            set { SetValue(ref this.fotonuevabyte, value); }
        }
        public ICommand updateNombre
        {
            get
            {
                return new Command(() => updateNombreUsuario());
            }
        }
        public ICommand ActualizarPerfil
        {
            get{ return new Command(() => ActualizarFoto()); }
        }
        public ICommand updateEmail
        {
            get
            {
                return new Command(() => updateEmailUsuario());
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
                case "Cámara":
                    Camara();

                    break;
                case "Seleccionar de la galería":
                    selectFile();
                    break;
            }
        }

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
                Name = "perfil.jpg",
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

                PerfilRest = ImageSource.FromStream(() => file.GetStream());

                using (MemoryStream memory = new MemoryStream())
                {

                    Stream stream = file.GetStream();
                    stream.CopyTo(memory);
                    fotonuevabyte = memory.ToArray();
                }
            }
           
        }
        public async Task PickAndShowFile(string[] fileTypes)
        {

            var file = await CrossFilePicker.Current.PickFile(fileTypes);

            if (file != null)
            {

                PerfilRest = file.FileName;

                if (file.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase)
                       || file.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
                {
                    PerfilRest = ImageSource.FromStream(() =>
                    {
                        return file.GetStream();
                    });

                    using (MemoryStream memory = new MemoryStream())
                    {

                        Stream stream = file.GetStream();
                        stream.CopyTo(memory);
                        fotonuevabyte = memory.ToArray();
                    }

                }

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("File", "Seleccione un perfil nuevo", "Ok");
            }





        }

        //Modelo para actualizar Perfil

        public async void ActualizarFoto()
        {


            if (FotoNuevaByte == null  ) { await App.Current.MainPage.DisplayAlert("File","Seleccione un perfil nuevo","Ok"); return; }
            if (Id == null) return;
                this.RunningProgress = true;
                this.Progress = true;

                var user = new UsuariosRest
                {
                    id = Convert.ToInt32(Id),
                    idToken = IdToken,
                    tokenfirebase = IdTokenFirebase,
                    correo = EmailPreferencias,
                    name = NombrePreferencias,
                    fecha = Fecha,
                    estado = true,
                    password = PasswordActual,
                    image = FotoNuevaByte,
                    direccion = Direccion,

                };


                var client = new HttpClient();
                var json = JsonConvert.SerializeObject(user);
                var contentJSON = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(Constantes.urlPost, contentJSON);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    await App.Current.MainPage.DisplayAlert("Datos", "Se Actualizo El Perfil", "OK");
                    this.RunningProgress = false;
                    this.Progress = false;
                
                }
         

        }


        //actualizar nombre de usuario
        public async void updateNombreUsuario()
        {
           
            if (string.IsNullOrEmpty(NameUpdate)){

                await App.Current.MainPage.DisplayAlert("Alerta", "Campo nombre esta vacio", "OK");
                return;
            }
            if (string.IsNullOrEmpty(conPasswordUpdate))
            {

                await App.Current.MainPage.DisplayAlert("Alerta", "Campo contraseña esta vacio", "OK");
                return;
            }

            if (Id == null) return;
            if (conPasswordUpdate == PasswordActual && Id!="")
            {
                this.RunningProgress = true;
                this.Progress = true;
                var user = new UsuariosRest
                {
                    id = Convert.ToInt32(Id),
                    idToken = IdToken,
                    tokenfirebase = IdTokenFirebase,
                    correo = EmailPreferencias,
                    name = NameUpdate,
                    fecha = Fecha,
                    estado = true,
                    password = conPasswordUpdate,
                    image = PerfilByte,
                    direccion = Direccion,

                };


                var client = new HttpClient();
                var json = JsonConvert.SerializeObject(user);
                var contentJSON = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(Constantes.urlPost, contentJSON);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    await App.Current.MainPage.DisplayAlert("Datos", "Se actualizo el nombre", "OK");
                    this.RunningProgress = false;
                    this.Progress = false;
                    Preferences.Set("nombre", NameUpdate);

                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Datos", "La contraseña es incorrecta", "OK");
            }
           
        }
        public async void updateEmailUsuario()
        {


            var authUpdate = new FirebaseAuthProvider(new FirebaseConfig(Constantes.ApiKey));
           
            // var updateContrasenia = authUpdate.ChangeUserPassword(tokenfirebase, newpassword.Text);
            if (string.IsNullOrEmpty(EmailUpdate))
            {

                await App.Current.MainPage.DisplayAlert("Alerta", "Campo nombre esta vacio", "OK");
                return;
            }
            if (string.IsNullOrEmpty(conPasswordUpdate))
            {

                await App.Current.MainPage.DisplayAlert("Alerta", "Campo contraseña esta vacio", "OK");
                return;
            }

            if (Id == null) return;
            if (conPasswordUpdate == PasswordActual && Id != "")
            {
                string token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjFiYjk2MDVjMzZlOThlMzAxMTdhNjk1MTc1NjkzODY4MzAyMDJiMmQiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL3NlY3VyZXRva2VuLmdvb2dsZS5jb20vYXBwLXJ1bm5pbmctMzE3MDAzIiwiYXVkIjoiYXBwLXJ1bm5pbmctMzE3MDAzIiwiYXV0aF90aW1lIjoxNjI3MzUzNzUwLCJ1c2VyX2lkIjoiU1VDaEN5NzQ5SmFIYUVjcnUwNEFpYzBvZXE5MyIsInN1YiI6IlNVQ2hDeTc0OUphSGFFY3J1MDRBaWMwb2VxOTMiLCJpYXQiOjE2MjczNTM3NTAsImV4cCI6MTYyNzM1NzM1MCwiZW1haWwiOiJkYXJ3aW5nMDEwMDAxMDBAZ21haWwuY29tIiwiZW1haWxfdmVyaWZpZWQiOmZhbHNlLCJmaXJlYmFzZSI6eyJpZGVudGl0aWVzIjp7ImVtYWlsIjpbImRhcndpbmcwMTAwMDEwMEBnbWFpbC5jb20iXX0sInNpZ25faW5fcHJvdmlkZXIiOiJwYXNzd29yZCJ9fQ.cIkq16pn1gdimejS6k8tpZUmrAEbH331mJVwV9UK-YTs6jYTNO_0xaOzs5SG_eqI3-7iFEUDnXeyuTivBkdzntHpVFr9ZhmU7Hcp-ymCHM0r9OqZ0fFJGefCP301XsZh7IW5ORc4-xxWOSI3OhEtyRvKTn-xiUp71tLFVj-OVBeI0lE9jVX52AYPAkucqw-eJvqSgBwImejXnnTHxBnL9yCnVDQBlVfbQ78wQjMCtbXAtcEDLh_gXmHecWkcEj4ybZSY0rilJoxSwXKl-WUkrML8eWB1AK1d_d6a8Pp4-Cn863de1jxp4ClbXOGNmW87QyTTIRNzdG1XUEvOYfneKg";
                var updateEmail = authUpdate.ChangeUserEmail(token, EmailUpdate);
                if (updateEmail.IsCompleted)
                {
                    this.RunningProgress = true;
                    this.Progress = true;
                    var user = new UsuariosRest
                    {
                        id = Convert.ToInt32(Id),
                        idToken = IdToken,
                        tokenfirebase = IdTokenFirebase,
                        correo = EmailUpdate,
                        name = NameUpdate,
                        fecha = Fecha,
                        estado = true,
                        password = PasswordActual,
                        image = PerfilByte,
                        direccion = Direccion,

                    };


                    var client = new HttpClient();
                    var json = JsonConvert.SerializeObject(user);
                    var contentJSON = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(Constantes.urlPost, contentJSON);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        await App.Current.MainPage.DisplayAlert("Datos", "Se actualizo el nombre", "OK");
                        this.RunningProgress = false;
                        this.Progress = false;
                        Preferences.Set("nombre", NameUpdate);

                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Datos", "No se pudo actualizar", "OK");
                }
               
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Datos", "La contraseña es incorrecta", "OK");
            }
        }

    }
}
