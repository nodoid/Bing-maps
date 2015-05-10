using System;

using Xamarin.Forms;
using System.Threading.Tasks;
using dev.virtualearth.net.webservices.v1.geocode;

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
            Location loc = null;

            var lblAddress = new Label()
            {
                Text = "Enter the address",
                TextColor = Color.Black
            };
            var enterText = new Entry()
            {
                Placeholder = "Address"
            };
            var btnFind = new Button()
            {
                Text = "Find locaction"
            };
            btnFind.Clicked += async delegate
            {
                if (!string.IsNullOrEmpty(enterText.Text))
                    loc = await GeocodeAddress(enterText.Text);

                var uri = await Map.GetMapUri(loc.Latitude, loc.Longitude, 2, "HYBRID", 300, 300);

                await Navigation.PushAsync(new Map(uri));
            };
           
            Content = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(20, 20),
                Children = { lblAddress, enterText, btnFind }
            };
        }

        async Task<Location> GeocodeAddress(string address)
        {
            var geocodeRequest = new GeocodeRequest();
            geocodeRequest.Credentials = new Credentials();
            geocodeRequest.Credentials.ApplicationId = App.Self.APIKEY;
            geocodeRequest.Query = address;

            var filters = new ConfidenceFilter[1];
            filters[0] = new ConfidenceFilter();
            filters[0].MinimumConfidence = Confidence.High;

            var geocodeOptions = new GeocodeOptions
            {
                Filters = filters
            };
            geocodeRequest.Options = geocodeOptions;

            var geocodeService = new GeocodeServiceClient("BasicHttpBinding_IGeocodeService");
            geocodeService.GeocodeAsync(geocodeRequest);

            if (geocodeResponse.Results.Length > 0)
            if (geocodeResponse.Results[0].Locations.Length > 0)
                return geocodeResponse.Results[0].Locations[0];
            else
                return null;
        }


    }
}


