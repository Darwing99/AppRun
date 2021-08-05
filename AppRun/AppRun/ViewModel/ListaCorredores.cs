using AppRun.clases;
using AppRun.Model;
using AppRun.services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppRun.ViewModel
{
    public class ListaCorredores : BaseViewModel
    {
        private ObservableCollection<UsuariosRest> _corredores;
        List<UsuariosRest> userRest;
        RestApiLogin restApiLogin;
        public ObservableCollection<UsuariosRest> ModelCorredores
        {
            get { return _corredores; }
            set { _corredores = value; OnPropertyChanged(); }
        }

        private UsuariosRest _selectCorredor;

        public UsuariosRest SelectedCorredor
        {
            get { return _selectCorredor; }
            set { _selectCorredor = value; OnPropertyChanged(); }
        }
        public string buscarCorredor;
        public string BuscarCorredores
        {
            get { return this.buscarCorredor; }
            set { SetValue(ref this.buscarCorredor, value); }
        }

        public bool resfres;
        public bool Refresc
        {
            get { return this.resfres; }
            set { SetValue(ref this.resfres, value); }
        }

        public List<UsuariosRest> _listOfItems;
        public List<UsuariosRest> ListOfItems
        {
            get { return this._listOfItems; }
            set { SetValue(ref this._listOfItems, value); }
        }



        public ICommand BuscarCorredor
        {
            get
            {
                return new Command(() => ListCorredores());
            }
        }
        public ICommand infoCorredorCommand { private set; get; }

        public INavigation Navigation { get; set; }

        public  ListaCorredores(INavigation navigation)
        {
            Refresc = true;
            Navigation = navigation;
            infoCorredorCommand = new Command<Type>(async (pageType) => await infoCorredor(pageType));
          

            userRest = new List<UsuariosRest>();
            restApiLogin = new RestApiLogin();
            ModelCorredores = new ObservableCollection<UsuariosRest>();
            ListCorredores();
          
            
        }
        public async void ListCorredores()
        {
        
            userRest = await restApiLogin.GetRepositoriesAsync(Constantes.urlGet);
            if (userRest != null)
            {
                ListOfItems = userRest;

                Refresc = false;
            }
           


        }
        async Task infoCorredor(Type pageType)
        {
           /* if (SelectedCorredor != null)
            {
                var page = (Page)Activator.CreateInstance(pageType);

                page.BindingContext = new CorredorViewModel()
                {
                   
                };

                await Navigation.PushAsync(page);
            }
           */
        }

    }
}