using AppRun.clases;
using AppRun.Model;
using AppRun.services;
using AppRun.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppRun.modulos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateCorreo : ContentPage
    {
       
        public UpdateCorreo()
        {
            InitializeComponent();
            BindingContext = new UpdateViewModel();

        }
     
    }
}