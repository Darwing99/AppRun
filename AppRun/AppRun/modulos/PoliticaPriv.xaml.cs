using AppRun.clases;
using AppRun.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppRun.modulos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PoliticaPriv : ContentPage
    {
        Uri uri;

        public PoliticaPriv()
        {
            InitializeComponent();
            BindingContext = new MainPageModel();
            BindingContext = new UpdateViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        void btnOpenBrowser_Clicked(object sender, System.EventArgs e)
        {
            uri = new Uri(entURL.Text);
            OpenBrowser(uri);
        }

        public async void OpenBrowser(Uri uri)
        {
            await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
    }
}