using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;



namespace AppRun
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabHome :TabbedPage
    {
        public TabHome()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
          
            var PostIIS = new FontImageSource()
            {
                FontFamily = "UserIcons",
                Glyph = "\U000F0CBD"
            };
            var MyAdsIIS = new FontImageSource()
            {
                FontFamily = "UserIcons",
                Glyph = "\U000f0352"
            };
            var ProfileIIS = new FontImageSource()
            {
                FontFamily = "UserIcons",
                Glyph = "\U000F07CC",
                
            };


       
            tabpage.Children.Add(new NavigationPage(new Progreso()) { Title = "Progreso", IconImageSource = PostIIS });
            tabpage.Children.Add(new NavigationPage(new Home()) { Title = "Iniciar", IconImageSource = MyAdsIIS });
            tabpage.Children.Add(new NavigationPage(new Clasificacion()) { Title = "Clasificación", IconImageSource = ProfileIIS });
        }
    }
}