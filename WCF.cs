using System;

using Xamarin.Forms;

namespace WCF
{
    public class App : Application
    {
        public static App Self { get; private set; }

        public readonly string APIKEY = "INSERT_YOUR_KEY";

        public App()
        {
            App.Self = this;
            // The root page of your application
            MainPage = new Geocode();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

