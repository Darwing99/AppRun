using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppRun.clases;
using AppRun.ViewModel;
using System.Text.Json.Serialization;


namespace AppRun
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        double dataLatitude, datalongitude;

        public Home()
        {
            InitializeComponent();
            UbicationPin();
           
        }
        List<Posicion> p;

        public async  void UbicationPin()
        {
       
        
                var location = CrossGeolocator.Current;
                location.DesiredAccuracy = 50;

                if (!location.IsGeolocationEnabled || !location.IsGeolocationAvailable)
                {

                    await DisplayAlert("Warning", " GPS no esta activo", "ok");

                }
                else
                {
                    if (!location.IsListening)
                    {
                    await location.StartListeningAsync(TimeSpan.FromSeconds(10), 1);


                    }
                    location.PositionChanged += (posicion, args) =>
                    {
                        var ubicacion = args.Position;
                        Application.Current.Properties["latitude"] = ubicacion.Latitude;
                        Application.Current.Properties["longitude"] = ubicacion.Longitude;

                        p = new List<Posicion>();

                        p.Add(new Posicion {latitud=ubicacion.Latitude,longitud=ubicacion.Longitude});

                        Pin pin = new Pin
                        {
                            Label = "Ubicacion actual",
                            Address = "Desconocida",
                            Position = new Position(ubicacion.Latitude, ubicacion.Longitude)
                        };
                    
                        m.Pins.Add(pin);
                        m.MoveToRegion(mapSpan: MapSpan.FromCenterAndRadius(new Position(ubicacion.Latitude, ubicacion.Longitude), Distance.FromKilometers(1)));

                        Polyline polyline = new Polyline
                        {
                            StrokeColor = Color.Red,
                            StrokeWidth = 12,


                        };

                        foreach (var datos in p)
                        {
                            polyline.Geopath.Add(new Position(datos.latitud,datos.longitud));
                        }


                        m.MapElements.Add(polyline);
                      
                    };


                }

              
        }

        protected  override void OnAppearing()
        {
       
            if (Application.Current.Properties.ContainsKey("latitude"))
            {
                dataLatitude = Convert.ToDouble(Application.Current.Properties["latitude"] as string);
              
            }
            if (Application.Current.Properties.ContainsKey("longitude"))
            {
               datalongitude = Convert.ToDouble(Application.Current.Properties["longitude"] as string);
            }
          //  p.Add(new Posicion { latitud = dataLatitude,longitud=datalongitude});




            /*
         Device.StartTimer(TimeSpan.FromSeconds(1), () => {
            var shouldTimerContinueWork = true;
             var location = CrossGeolocator.Current;
             location.DesiredAccuracy = 50;

             return shouldTimerContinueWork;
          });*/

            base.OnAppearing();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var data = p.ToArray().FirstOrDefault();
            double datosLatitude=data.latitud;
            double datosLongitude = data.longitud;
            int o= p.Count;

        }

    }
}