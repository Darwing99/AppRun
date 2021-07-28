using System;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppRun
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inicio : FlyoutPage
    {
        public Inicio()
        {
            InitializeComponent();
            FlyoutPage.ListView.ItemSelected += ListView_ItemSelected;
            NavigationPage.SetHasNavigationBar(this, false);
            bool estado=Preferences.Get("Rememberme",true);
            if (!estado)
            {
                Navigation.PopAsync();
            }
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MenuItem;
            if (item == null)
                return;
        
                var page = (Page)Activator.CreateInstance(item.TargetType);
                page.Title = item.Title;

                Detail = new NavigationPage(page);
                IsPresented = false;

                FlyoutPage.ListView.SelectedItem = null;
            
            
           
        }
    }
}