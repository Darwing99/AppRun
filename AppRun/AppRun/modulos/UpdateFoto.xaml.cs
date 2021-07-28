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
    public partial class UpdateFoto : ContentPage
    {
       
        public UpdateFoto()
        {
            InitializeComponent();
            BindingContext = new UpdateViewModel();

        }
       
    }
}