using System;

using Xamarin.Forms;
using System.Threading.Tasks;
using dev.virtualearth.net.webservices.v1.geocode;
using System.ServiceModel;

namespace WCF
{
    public class Geocode : ContentPage
    {
        public Geocode()
        {
            if (Device.OS == TargetPlatform.Android)
                BackgroundColor = Color.White;
            else
                Padding = new Thickness(0, 20, 0, 0);
            CreateUI();
        }

        void CreateUI()
        {
            var lblAddress = new Label()
            {
                Text = "Enter the address",
                TextColor = Color.Black,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            var enterText = new Entry()
            {
                Placeholder = "Address",
                TextColor = Color.Blue,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            var btnFind = new Button()
            {
                Text = "Find locaction",
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            btnFind.Clicked += async delegate
            {
                if (!string.IsNullOrEmpty(enterText.Text))
                    GeocodeAddress(enterText.Text);
            };
           
            Content = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Padding = new Thickness(20, 20),
                Children = { lblAddress, enterText, btnFind }
            };
        }

        void GeocodeAddress(string address)
        {
            var geocodeRequest = new GeocodeRequest
            {
                Credentials = new Credentials
                {
                    ApplicationId = App.Self.APIKEY,
                },
                Query = address,
                Address = new Address()
            };

            var filters = new ConfidenceFilter[1];
            filters[0] = new ConfidenceFilter();
            filters[0].MinimumConfidence = Confidence.High;

            var geocodeOptions = new GeocodeOptions
            {
                Filters = filters
            };
            geocodeRequest.Options = geocodeOptions;

            var geocodeService = new GeocodeServiceClient("BasicHttpBinding_IGeocodeService");

            var myLoc = new Location();

            geocodeService.GeocodeCompleted += async (object sender, GeocodeCompletedEventArgs e) =>
            {
                if (e.Error == null)
                {
                    var res = e.Result;
                    if (e.Result.Results.Length > 0)
                    if (e.Result.Results[0].Locations.Length > 0)
                    {
                        myLoc = e.Result.Results[0].Locations[0];
                        var uri = await Map.GetMapUri(myLoc.Latitude, myLoc.Longitude, 2, "HYBRID", 300, 300);

                        await Navigation.PushAsync(new Map(uri));
                    }

                }

            };

            geocodeService.GeocodeAsync(geocodeRequest);
        }
    }
}


