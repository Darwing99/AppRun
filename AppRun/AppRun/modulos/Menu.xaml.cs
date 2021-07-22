using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppRun
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : ContentPage
    {
        public ListView ListView;

        public Menu()
        {
            InitializeComponent();

            BindingContext = new InicioFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        class InicioFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MenuItem> MenuItems { get; set; }

            public InicioFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<MenuItem>(new[]
                {
                    new MenuItem { Id = 0, Title = "Home", icono="home.png", TargetType=typeof(TabHome)},
                    new MenuItem { Id = 1, Title = "Corredores",icono="run.png", TargetType=typeof(Corredores) },
                    new MenuItem { Id = 2, Title = "Progreso",icono="progreso.png", TargetType=typeof(Progreso) },
                    new MenuItem { Id = 3, Title = "Estadisticas",icono="estadisticas.png",TargetType=typeof(Estadisticas) },
                    new MenuItem { Id = 4, Title = "Clasificacion",icono="clasificacion.png",TargetType=typeof(Clasificacion) },
                   
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Settings());

        }
    }
}