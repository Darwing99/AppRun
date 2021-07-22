using AppRun.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppRun.modulos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarUsuario : ContentPage
    {
        public RegistrarUsuario()
        {
            InitializeComponent();
            BindingContext = new RegistrarViewModel();
        }
       
        private async void NavToLogin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}