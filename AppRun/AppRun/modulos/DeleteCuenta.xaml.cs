using AppRun.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppRun.modulos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeleteCuenta : ContentPage
    {
        public DeleteCuenta()
        {
            InitializeComponent();
            BindingContext = new UpdateViewModel();
        }
    }
}