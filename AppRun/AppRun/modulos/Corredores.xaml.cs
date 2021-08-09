using AppRun.clases;
using AppRun.Model;
using AppRun.services;

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
    public partial class Corredores : ContentPage
    {
        
        public Corredores()
        {
            InitializeComponent();

            BindingContext = new ViewModel.CorredoresViewModel();

        }
       
    }
}