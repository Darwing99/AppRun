using AppRun.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppRun
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : ContentPage
    {
        public ListView ListView;
        ViewCell lastCell;
        public Menu()
        {
            InitializeComponent();
            BindingContext = new UpdateViewModel();
            BindingContext = new InicioFlyoutViewModel();
            userlogueado.Text = Preferences.Get("nombre", "Anónino");
            ListView = MenuItemsListView;
        }

        class InicioFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MenuItem> MenuItems { get; set; }

            public InicioFlyoutViewModel()
            {
                var Home = new FontImageSource()
                {
                    FontFamily = "UserIcons",
                    Glyph = "\U000F02DC",
                    Color = Color.OrangeRed
                };
                var Run = new FontImageSource()
                {
                    FontFamily = "UserIcons",
                    Glyph = "\U000F070E",
                    Color = Color.OrangeRed
                };
                var progress = new FontImageSource()
                {
                    FontFamily = "UserIcons",
                     Glyph = "\U000F0996",
                     Color = Color.OrangeRed
                };
                var Estadistica = new FontImageSource()
                {
                    FontFamily = "UserIcons",
                    Glyph = "\U000F0EA2",
                    Color = Color.OrangeRed
                };
                var IconClasificacion = new FontImageSource()
                {
                    FontFamily = "UserIcons",
                    Glyph = "\U000F039A",
                    Color=Color.OrangeRed

                };
                var IconPerfil = new FontImageSource()
                {
                    FontFamily = "UserIcons",
                    Glyph = "\U000F0004",
                    Color = Color.OrangeRed,
                    

                };
                MenuItems = new ObservableCollection<MenuItem>(new[]
                {
                    new MenuItem { Id = 0, Title = "Inicio", icono=Home, TargetType=typeof(TabHome)},
                    new MenuItem { Id = 1, Title = "Corredores",icono=Run, TargetType=typeof(Corredores) },
                    new MenuItem { Id = 2, Title = "Progreso",icono=progress, TargetType=typeof(Progreso) },
                    new MenuItem { Id = 3, Title = "Estadisticas",icono=Estadistica,TargetType=typeof(Estadisticas) },
                    new MenuItem { Id = 4, Title = "Clasificación",icono=IconClasificacion,TargetType=typeof(Clasificacion) },
                    new MenuItem { Id = 5, Title = "Perfil",icono=IconPerfil,TargetType=typeof(Settings) },
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
        private void ViewCell_Tapped(object sender, System.EventArgs e)
        {
            if (lastCell != null)
                lastCell.View.BackgroundColor = Color.Transparent;
            var viewCell = (ViewCell)sender;
            if (viewCell.View != null)
            {
                viewCell.View.BackgroundColor = Color.OrangeRed;
                lastCell = viewCell;
            }
        }
    }
}