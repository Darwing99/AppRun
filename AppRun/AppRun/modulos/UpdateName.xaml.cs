using AppRun.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppRun.modulos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateName : ContentPage
    {
       
        public UpdateName()
        {
            InitializeComponent();
          
            BindingContext = new UpdateViewModel();
        }
       

    }
}