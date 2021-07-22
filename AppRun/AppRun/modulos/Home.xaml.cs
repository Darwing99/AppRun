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
         Device.StartTimer(TimeSpan.FromSeconds(1), () => {
            var shouldTimerContinueWork = true;
           
          
            return shouldTimerContinueWork;
          });

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