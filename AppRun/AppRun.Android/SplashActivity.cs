using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRun.Droid
{
    [Activity(Label = "RunForce", Icon = "@mipmap/icon", Theme = "@style/SplashTheme", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            // Create your application here
        }

        protected override async void OnResume()
        {

            base.OnResume();
            await SimulateStartup();

        }

        async Task SimulateStartup()
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
            StartActivity(new Intent(ApplicationContext, typeof(MainActivity)));
        }
    }
}