using AppRun.clases;
using AppRun.Model;
using AppRun.services;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace AppRun.ViewModel
{
    public class ViewModelMap : INotifyPropertyChanged
    {
        public ICommand CalculateRouteCommand { get; set; }
        public ICommand UpdatePositionCommand { get; set; }

        public ICommand LoadRouteCommand { get; set; }
        public ICommand StopRouteCommand { get; set; }
        IGoogleMapsApiService googleMapsApi = new GoogleMapsApiService();
      
        public bool HasRouteRunning { get; set; }
        string _distancia;
        public string DistanciaPoints
        {
            get
            {
                return _distancia;
            }
            set
            {
                _distancia = value;
            

            }
        }

        double distanciaF;
        public double Distancia
        {
            get
            {
                return distanciaF;
            }
            set
            {
                distanciaF = value;


            }
        }
        double velocidad;
        public double Velocidad
        {
            get
            {
                return velocidad;
            }
            set
            {
                velocidad = value;


            }
        }

        double tiempoSegundos;
        public double TiempoSegundos
        {
            get
            {
                return tiempoSegundos;
            }
            set
            {
                tiempoSegundos = value;


            }
        }
        string _originLatitud;
        string _originLongitud;
        string _originLatitudActual;
        public string OriginLatitud
        {
            get
            {
                return _originLatitudActual;
            }
            set
            {
                _originLatitudActual = value;
               
            }
        }
        string _originLongitudActual;
        public string OriginLongitud
        {
            get
            {
                return _originLongitudActual;
            }
            set
            {
                _originLongitudActual = value;

            }
        }



        string _destinationLatitud;
        string _destinationLongitud;

        Stopwatch Stopwatch = new Stopwatch();
        private Timer time = new Timer();

        private DateTime fecha;
        public DateTime Fecha
        {
            get { return fecha; }
            set
            {
                fecha = value;

            }
        }
    

        private string horas;
        public string Horas
        {
            get { return horas; }
            set { horas = value;
             
            }
        }
        private string minutos;
        public string Minutos
        {
            get { return minutos; }
            set
            {
                minutos = value;
               
            }
        }
        private string segundos;
        public string Segundos
        {
            get { return segundos; }
            set
            {
                segundos = value;
               
            }
        }
        GooglePlaceAutoCompletePrediction _placeSelected;
        public  GooglePlaceAutoCompletePrediction PlaceSelected
        {
            get
            {
                return _placeSelected;
            }
            set
            {
                _placeSelected = value;
                if (_placeSelected != null)
                {
                    GetPlaceDetailCommand.Execute(_placeSelected);
                     
                }
            }
        }
        public ICommand FocusOriginCommand { get; set; }
        public ICommand GetPlacesCommand { get; set; }
        public ICommand GetPlaceDetailCommand { get; set; }

        public ObservableCollection<GooglePlaceAutoCompletePrediction> Places { get; set; }
        public ObservableCollection<GooglePlaceAutoCompletePrediction> RecentPlaces { get; set; } = new ObservableCollection<GooglePlaceAutoCompletePrediction>();

        public bool ShowRecentPlaces { get; set; }
        bool _isPickupFocused = true;

        string _ubicacionActual;
        public string UbicacionActual
        {
            get
            {
                return _ubicacionActual;
            }
            set
            {
                _ubicacionActual = value;
                if (!string.IsNullOrEmpty(_ubicacionActual))
                {
                    _isPickupFocused = true;
                    GetPlacesCommand.Execute(_ubicacionActual);
                }
            }
        }

        string _originText;
        public string OriginText
        {
            get
            {
                return _originText;
            }
            set
            {
                _originText = value;
                if (!string.IsNullOrEmpty(_originText))
                {
                    _isPickupFocused = false;
                    GetPlacesCommand.Execute(_originText);
                }
            }
        }


        string _nombre;
        public string NombreCarrera
        {
            get
            {
                return _nombre;
            }
            set
            {
                _nombre = value;
             
            }
        }
        public ICommand GetLocationNameCommand { get; set; }
        public bool IsRouteNotRunning
        {
            get
            {
                return !HasRouteRunning;
            }
        }


        public ViewModelMap()
        {

            LoadRouteCommand = new Command(async () => await LoadRoute());
            StopRouteCommand = new Command(StopRoute);
            GetPlacesCommand = new Command<string>(async (param) => await GetPlacesByName(param));
            GetPlaceDetailCommand = new Command<GooglePlaceAutoCompletePrediction>(async (param) => await GetPlacesDetail(param));
            GetLocationNameCommand = new Command<Position>(async (param) => await GetLocationName(param));
        }

        public async Task LoadRoute()
        {
            Position position=new Position();
            try
            {

                var googleDirection = await googleMapsApi.GetDirections(_originLatitudActual, _originLongitudActual, _destinationLatitud, _destinationLongitud);


                if (googleDirection.Routes != null && googleDirection.Routes.Count > 0)
                {
                    var positions = Enumerable.ToList(PolylineHelper.Decode(googleDirection.Routes.First().OverviewPolyline.Points));
                    CalculateRouteCommand.Execute(positions);

                    HasRouteRunning = true;
                    var placemarks = await Geocoding.GetPlacemarksAsync(position.Latitude, position.Longitude);
                    var distance = googleDirection.Routes.First().Legs.First().Distance.Text;
                    var distancef = googleDirection.Routes.First().Legs.First().Distance.Value;
                    DistanciaPoints = distance.ToString();
                    Distancia = distancef/1000;
                    Stopwatch.Reset();
                    Stopwatch.Start();
                    Horas = Stopwatch.Elapsed.Hours.ToString();
                    Minutos = Stopwatch.Elapsed.Minutes.ToString();
                    Segundos = Stopwatch.Elapsed.Seconds.ToString();
                    NombreCarrera = OriginText;
                    Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                    {
                        GetPosition();
                        Horas = Stopwatch.Elapsed.Hours.ToString();
                        Minutos = Stopwatch.Elapsed.Minutes.ToString();
                        Segundos = Stopwatch.Elapsed.Seconds.ToString();
                        return true;
                    });


                }
                else
                {
                    await App.Current.MainPage.DisplayAlert(":(", "No route found", "Ok");
                }


            }
            catch (Exception e)
            {

                await App.Current.MainPage.DisplayAlert(":(", "Error" + e.Message, "Ok");
            }

        }



       public async void Calculos()
        {

            //// a velocidad de 8km/h (2.22222m/s) en 35 minutos(2100 segundos) se queman 235 calorias equivale a 0.0010582m/s:s=235 ==>(1m/s:s=222075.222 calorias)
            ///

            
            var DateAndTime = DateTime.Now;
            var Date = DateAndTime.Date.ToString("dd-MM-yyyy");
            Fecha =Convert.ToDateTime(Date);
            TiempoSegundos = Convert.ToDouble(Horas) * 3600 + Convert.ToDouble(Minutos) * 60 + Convert.ToDouble(Segundos);
            double velocidadenmetrosporsegundo = distanciaF / tiempoSegundos;
            double calorias = velocidadenmetrosporsegundo/TiempoSegundos*(222075.222);
            string user = Preferences.Get("id", "");
            Velocidad = Distancia/TiempoSegundos;
            var carreras = new CarrerasModelApi
            {

                idToken = 0,
                idUser =Convert.ToInt32(user),
                tiempo=Math.Round(TiempoSegundos,3),
                distancia= Math.Round(Distancia,3),
                carrera = NombreCarrera,
                calorias= Math.Round(calorias,3),
                fecha = Fecha,
                velocidad= Math.Round(velocidadenmetrosporsegundo,3),
                tiempohoras= Math.Round(tiempoSegundos / 3600,3),
                fotos=null,
                estado = true
               

            };


            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(carreras);
            var contentJSON = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(Constantes.urlPostCarreras, contentJSON);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                await App.Current.MainPage.DisplayAlert("Registro", "Rutina Guardada", "OK");
                CleanFields();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Registro", "Error al guardar Rutina", "OK");
            }
        }

        public void StopRoute()
        {
            HasRouteRunning = false;
            Stopwatch.Stop();
            Calculos();
        }

        public async Task GetPlacesByName(string placeText)
        {
            var places = await googleMapsApi.GetPlaces(placeText);
            var placeResult = places.AutoCompletePlaces;
            if (placeResult != null && placeResult.Count > 0)
            {
                Places = new ObservableCollection<GooglePlaceAutoCompletePrediction>(placeResult);
            }

            ShowRecentPlaces = (placeResult == null || placeResult.Count == 0);
        }

        public async Task GetPlacesDetail(GooglePlaceAutoCompletePrediction placeA)
        {
            var place = await googleMapsApi.GetPlaceDetails(placeA.PlaceId);

            if (place != null)
            {


                if (_isPickupFocused)
                {
                    UbicacionActual = place.Name;
                    _originLatitud = $"{place.Latitude}";
                    _originLongitud = $"{place.Longitude}";
                    _isPickupFocused = false;
                    FocusOriginCommand.Execute(null);
                }
                else
                {
                    _destinationLatitud = $"{place.Latitude}";
                    _destinationLongitud = $"{place.Longitude}";

                    RecentPlaces.Add(placeA);

                    if (_originLatitud == _destinationLatitud && _originLongitud == _destinationLongitud)
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "Ruta invalida", "Ok");
                    }
                    else
                    {

                        await Application.Current.MainPage.Navigation.PopModalAsync(false);
                        
                        LoadRouteCommand.Execute(null);
                       

                    }
                  
                }
            }
        }
    
        void CleanFields()
        {
            UbicacionActual = OriginText = string.Empty;
            ShowRecentPlaces = true;
            PlaceSelected = null;
        }

        public void GetPosition()
        {
            var position= new Command<Position>(async (param) => await GetLocationName(param));
            UpdatePositionCommand.Execute(position);
        }

        //Get place 
        public async Task GetLocationName(Position position)
        {
            try
            {
                var placemarks = await Geocoding.GetPlacemarksAsync(position.Latitude, position.Longitude);
                var placemark = placemarks?.FirstOrDefault();
                if (placemark != null)
                {
                    //posicion inicial de mi ubicacion 
                    UbicacionActual = placemark.FeatureName;
                    var lat = placemark.Location.Latitude;
                    var lgt = placemark.Location.Longitude;
                    _originLatitudActual = lat.ToString();
                    _originLongitudActual = lgt.ToString();
                  
                }
                else
                {
                    UbicacionActual = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
