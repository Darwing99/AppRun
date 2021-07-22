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
            var ExploreIIS = new FontImageSource()
            {
                FontFamily = "UserIcons",
                Glyph = "\U000f0434"
            };
            var PostIIS = new FontImageSource()
            {
                FontFamily = "UserIcons",
                Glyph = "\U000f0035"
            };
            var MyAdsIIS = new FontImageSource()
            {
                FontFamily = "UserIcons",
                Glyph = "\U000f0352"
            };
            var ProfileIIS = new FontImageSource()
            {
                FontFamily = "UserIcons",
                Glyph = "\U000f03d2"
            };


       
            tabpage.Children.Add(new NavigationPage(new Progreso()) { Title = "Progreso", IconImageSource = PostIIS });
            tabpage.Children.Add(new NavigationPage(new Home()) { Title = "Home", IconImageSource = MyAdsIIS });
            tabpage.Children.Add(new NavigationPage(new Settings()) { Title = "Settings", IconImageSource = ProfileIIS });
        }
    }
}