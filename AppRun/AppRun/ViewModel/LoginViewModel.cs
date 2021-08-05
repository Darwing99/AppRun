using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AppRun.clases;
using AppRun.Model;
using AppRun.services;
using Firebase.Auth;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppRun.ViewModel
{
    
        public class LoginViewModel : BaseViewModel
        {
        private readonly IGoogleManager _googleManager;
        GoogleUser GoogleUser = new GoogleUser();
        public bool IsLogedIn { get; set; }
        IFirebaseAuthentication aut;

        List<UsuariosRest> service;
        RestApiLogin restService;
        public LoginViewModel()
        {
            this.IsEnabledTxt = true;
            
            restService = new RestApiLogin();
            _googleManager  = DependencyService.Get<IGoogleManager>();
        }
        
        public string email;
            public string password;
            public bool isRunning;
            public bool isVisible;
            public bool isEnabled;
        
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

            public bool IsRunningTxt
            {
                get { return this.isRunning; }
                set { SetValue(ref this.isRunning, value); }
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

        //switch toogle
        public bool Rememberme
        {
            get => Preferences.Get(nameof(Rememberme), false);
            set
            {
                Preferences.Set("Rememberme", value);
                OnPropertyChanged(nameof(Rememberme));

            }


        }

        public ICommand LoginCommand
        {
            get
            {
                return new Command(() => LoginMethod());
            }
        }
        public ICommand LoginCommandGoogle
        {
            get
            {
                return new Command(() => LoginGoogle());
            }
        }
        public ICommand forgotPassword
        {
            get
            {
                return new Command(() => ResetPasswordEmail());
            }
        }

        //Autenticacion con google

        public async void LoginGoogle()
        {

            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Constantes.ApiKey));
           
            try
            {
                
                var auth = await authProvider.SignInWithGoogleIdTokenAsync("666");
                var content = await auth.GetFreshAuthAsync();
                var serializedcontnet = JsonConvert.SerializeObject(content);
                string correo = content.User.Email.ToString();
                string iduser = content.User.LocalId.ToString();
                
                Preferences.Set("correo", correo);
                Preferences.Set("iduser", iduser);
              
                this.IsVisibleTxt = true;
                this.IsRunningTxt = true;

                await Application.Current.MainPage.Navigation.PushAsync(new Inicio());

            }
            catch (Exception ex)
            {
                var message = ex.Message;

                await App.Current.MainPage.DisplayAlert("Alert", "Cuenta no existe"+ ex.Message.ToString(), "OK");
            }

            this.IsEnabledTxt = false;

            await Task.Delay(20);

            this.IsRunningTxt = false;
            this.IsVisibleTxt = false;
            this.IsEnabledTxt = true;

        }

        //autenticacion con Gmail
        public async void LoginMethod()
            {
                if (string.IsNullOrEmpty(this.email))
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Alert",
                        "Escriba su email.",
                        "ok");
                    return;
                }
                if (string.IsNullOrEmpty(this.password))
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        "Escriba su contraseña.",
                        "Ok");
                    return;
                }



                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Constantes.ApiKey));
                try
                {
               
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(EmailTxt.ToString(), PasswordTxt.ToString());
                    var content = await auth.GetFreshAuthAsync();
                    var serializedcontnet = JsonConvert.SerializeObject(content);

                    string token = content.FirebaseToken;
                    string correo = content.User.Email.ToString();
                    string iduser = content.User.LocalId.ToString();
                    service = await restService.GetRepositoriesAsync(Constantes.urlGet);
                    var listaSeleccionada = service.Where(c => c.idToken.ToString().Contains(iduser.ToString()));
                  
                    if (listaSeleccionada != null)
                    {
              
                        byte[] image = listaSeleccionada.FirstOrDefault().image;
                        Preferences.Set("id", listaSeleccionada.FirstOrDefault().id.ToString());
                     
                        Preferences.Set("nombre", listaSeleccionada.FirstOrDefault().name);
                }
                    Preferences.Set("correo", correo);
                    Preferences.Set("iduser", iduser);
                  
                    this.IsVisibleTxt = true;
                    this.IsRunningTxt = true;
                    await Application.Current.MainPage.Navigation.PushAsync(new Inicio());

            }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "Usuario o contraseña invalida", "OK");
                }

                this.IsEnabledTxt = false;

                await Task.Delay(20);

                this.IsRunningTxt = false;
                this.IsVisibleTxt = false;
                this.IsEnabledTxt = true;

            }
     
            public async void ResetPasswordEmail(){
                var authReset = new FirebaseAuthProvider(new FirebaseConfig(Constantes.ApiKey));
                try
                {

                if (string.IsNullOrEmpty(EmailTxt.ToString()))
                {
                    await App.Current.MainPage.DisplayAlert("Alerta", "Escriba un Email","OK");
                    return;
                }
                else
                {
                    var auth = authReset.SendPasswordResetEmailAsync(EmailTxt.ToString());
                   
                    string result = await App.Current.MainPage.DisplayPromptAsync("Codigo de recuperacion", "Cual es el Codigo?");
                  
                }

          

            }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await App.Current.MainPage.DisplayAlert("Alert", "Invalid email or password", "OK");
                }

            }






        //Login Con Google

        public ICommand AuthCommandGoogle
        {
            get
            {
                return new Command(() => GoogleAuth());
            }
        }

        private async void OnLoginComplete(GoogleUser googleUser, string message)
        {
           

            if (googleUser != null)
            {
                GoogleUser = googleUser;
               
                string correo = GoogleUser.Email;
                string iduser = GoogleUser.id;
               
                Preferences.Set("correo", correo);
                Preferences.Set("iduser", iduser);
                Preferences.Set("nombre", GoogleUser.Name);
                Preferences.Set("foto", GoogleUser.Picture.ToString());
                LoginGoogle();
                await Application.Current.MainPage.Navigation.PushAsync(new Inicio());
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Message", message, "Ok");
            }
        }

      
        private void GoogleAuth()
        {
            _googleManager.Login(OnLoginComplete);
           
        }

    }
}
