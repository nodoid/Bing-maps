using System;

using Xamarin.Forms;

namespace WCF
{
    public class App : Application
    {
        public static App Self { get; private set; }

        public readonly string APIKEY = "Aslv-q1IU77X818-ueMA8nC9Bjm9BDtUyf1IUMZiGFhEZnIP_uNyEk8DpOWX4InO";

        public App()
        {
            App.Self = this;
            // The root page of your application
            MainPage = new ContentPage
            {
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children =
                    {
                        new Label
                        {
                            XAlign = TextAlignment.Center,
                            Text = "Welcome to Xamarin Forms!"
                        }
                    }
                }
            };
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

