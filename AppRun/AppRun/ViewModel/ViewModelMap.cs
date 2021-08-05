using AppRun.clases;
using AppRun.Model;
using AppRun.services;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
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

        GooglePlaceAutoCompletePrediction _placeSelected;
        public GooglePlaceAutoCompletePrediction PlaceSelected
        {
            get
            {
                return _placeSelected;
            }
            set
            {
                _placeSelected = value;
                if (_placeSelected != null)
                    GetPlaceDetailCommand.Execute(_placeSelected);
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

            var positionIndex = 1;
            try
            {

                var googleDirection = await googleMapsApi.GetDirections(_originLatitudActual, _originLongitudActual, _destinationLatitud, _destinationLongitud);


                if (googleDirection.Routes != null && googleDirection.Routes.Count > 0)
                {
                    var positions = (Enumerable.ToList(PolylineHelper.Decode(googleDirection.Routes.First().OverviewPolyline.Points)));
                    CalculateRouteCommand.Execute(positions);

                    HasRouteRunning = true;

                    var distance = ((googleDirection.Routes.First().Legs.First().Distance.Text));
                    _distancia = distance.ToString();


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






        public void StopRoute()
        {
            HasRouteRunning = false;
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
                        LoadRouteCommand.Execute(null);
                        await App.Current.MainPage.Navigation.PopAsync(false);
                        CleanFields();
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
                    return;
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
