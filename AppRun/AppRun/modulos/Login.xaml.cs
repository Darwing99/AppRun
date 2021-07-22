using AppRun.ViewModel;
using AppRun.modulos;
using AppRun.services;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace AppRun
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        private readonly IGoogleManager _googleManager;
        GoogleUser GoogleUser = new GoogleUser();
        public bool IsLogedIn { get; set; }
        IFirebaseAuthentication auth;

        public Login()
        {   
            
             BindingContext = new LoginViewModel();
            _googleManager = DependencyService.Get<IGoogleManager>();
          
            InitializeComponent();
            Device.SetFlags(new string[] { "MediaElement_Experimental" });
         
            
        }

     

        private void GoogleLogout()
        {
            _googleManager.Logout();
            IsLogedIn = false;
        }
        private async void registro_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrarUsuario());
        }

        private async void inicio_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Inicio());
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            _googleManager.Login(OnLoginComplete);
        }
        private void GoogleAuth()
        {
            _googleManager.Login(OnLoginComplete);
        }

        private void OnLoginComplete(GoogleUser googleUser, string message)
        {


            if (googleUser != null)
            {
                GoogleUser = googleUser;

                Preferences.Set("id", GoogleUser.id);
                Preferences.Set("correo", GoogleUser.Email);
                Preferences.Set("nombre", GoogleUser.Name);
                Preferences.Set("foto", GoogleUser.Picture.ToString());
                email.Text = GoogleUser.Email;
                IsLogedIn = true;
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Message", message, "Ok");
            }
        }
    }
}