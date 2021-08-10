using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Text.Json.Serialization;
using AppRun.modulos;
using System.Linq;

namespace AppRun
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
      

        public Home()
        {
            InitializeComponent();
           
        }

        public async void OnEnterAddressTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchPlacePage() { BindingContext = this.BindingContext }, false);

        }

        public void Handle_Stop_Clicked(object sender, EventArgs e)
        {
            //searchLayout.IsVisible = true;
            Time.IsVisible = false;
            distance.IsVisible = false;
            stopRouteButton.IsVisible = false;
            map.Polylines.Clear();

            map.Pins.Clear();

        }

        //Center map in actual location 
        protected override void OnAppearing()
        {
            base.OnAppearing();
            map.GetActualLocationCommand.Execute(null);


        }

        void OnCalculate(System.Object sender, System.EventArgs e)
        {

            //searchLayout.IsVisible = false;
            Time.IsVisible = true;
            distance.IsVisible = true;
            stopRouteButton.IsVisible = true;
            //distancia.Text = Preferences.Get("distancia", "Hola");
        }

        async void OnLocationError(System.Object sender, System.EventArgs e)
        {
            await DisplayAlert("Error", "Unable to get actual location", "Ok");
        }



    

}
}
