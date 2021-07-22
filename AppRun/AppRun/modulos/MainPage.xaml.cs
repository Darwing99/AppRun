using AppRun.Firebase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppRun
{
    public partial class MainPage : ContentPage
    {

        FirebaseHelp firebase = new FirebaseHelp();
        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {

            base.OnAppearing();
            var allPersons = await firebase.GetAllPersons();
           /* lstPersons.ItemsSource = allPersons;*/
        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            /* await firebase.AddPerson(Convert.ToInt32(txtId.Text), txtName.Text,txtCorreo.Text,txtPassword.Text);
             txtId.Text = string.Empty;
             txtName.Text = string.Empty;
             await DisplayAlert("Success", "Usuario Guardado", "OK");
             var allPersons = await firebase.GetAllPersons();
             /*lstPersons.ItemsSource = allPersons;
            */
            await Navigation.PushAsync(new Login());
        }

        private async void BtnRetrive_Clicked(object sender, EventArgs e)
        {
            var person = await firebase.GetPerson(Convert.ToInt32(txtId.Text));
            if (person != null)
            {
                txtId.Text = person.UserId.ToString();
                txtName.Text = person.Name;
                txtCorreo.Text = person.correo;
                txtPassword.Text = person.password;
                await DisplayAlert("Success", "Person Retrive Successfully", "OK");

            }
            else
            {
                await DisplayAlert("Success", "No Person Available", "OK");
            }

        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        { /*
            await firebase.UpdatePerson(Convert.ToInt32(txtId.Text), txtName.Text,txtCorreo.Text,txtPassword.Text);
            txtId.Text = string.Empty;
            txtName.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            txtPassword.Text =string.Empty;
            await DisplayAlert("Success", "Person Updated Successfully", "OK");
            var allPersons = await firebase.GetAllPersons();
            lstPersons.ItemsSource = allPersons;*/
            await Navigation.PushAsync(new Login());
        }

        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
           /* await firebase.DeletePerson(Convert.ToInt32(txtId.Text));
            await DisplayAlert("Success", "Person Deleted Successfully", "OK");
            var allPersons = await firebase.GetAllPersons();
            lstPersons.ItemsSource = allPersons;*/
        }

        private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {

        }
    }
}
