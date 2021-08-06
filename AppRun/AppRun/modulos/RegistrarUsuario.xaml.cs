using AppRun.clases;
using AppRun.Model;
using AppRun.services;
using AppRun.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppRun.modulos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarUsuario : ContentPage
    {
        List<ModelCountry> service;
        CountryService restService;
        public RegistrarUsuario()
        {
            InitializeComponent();
            BindingContext = new RegistrarViewModel();
            restService = new CountryService();
            PaisesLista();
           
            
        }
       public async void PaisesLista()
        {
            var region = "Americas";
            service = await restService.GetRepositoriesAsync(Constantes.url + region);
            foreach (var country in service)
            {
                pickerRegion.Items.Add(country.name);
            }
          

        }
        private async void NavToLogin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}