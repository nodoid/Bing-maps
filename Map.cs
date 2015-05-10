using Xamarin.Forms;
using System.Threading.Tasks;
using dev.virtualearth.net.webservices.v1.imagery;

namespace WCF
{
    public class Map : ContentPage
    {
        public Map()
        {
        }

        public Map(string uri)
        {
            var webview = new WebView()
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Source = uri
            };
            Content = webview;
        }


        async public static Task<string> GetMapUri(double latitude, double longitude, int zoom, string mapStyle, int width, int height)
        {
            var pins = new Pushpin[1];
            var pushpin = new Pushpin();
            pushpin.Location = new Location();
            pushpin.Location.Latitude = latitude;
            pushpin.Location.Longitude = longitude;
            pushpin.IconStyle = "2";
            pins[0] = pushpin;

            var mapUriRequest = new MapUriRequest
            {
                Credentials = new Credentials(),
                Pushpins = pins
            };
            mapUriRequest.Credentials.ApplicationId = App.Self.APIKEY;

            var mapUriOptions = new MapUriOptions
            {
                ZoomLevel = zoom,
                ImageSize = new SizeOfint()
            };

            switch (mapStyle.ToUpper())
            {
                case "HYBRID":
                    mapUriOptions.Style = MapStyle.AerialWithLabels;
                    break;
                case "ROAD":
                    mapUriOptions.Style = MapStyle.Road;
                    break;
                case "AERIAL":
                    mapUriOptions.Style = MapStyle.Aerial;
                    break;
                default:
                    mapUriOptions.Style = MapStyle.Road;
                    break;
            }

            mapUriOptions.ImageSize.Height = height;
            mapUriOptions.ImageSize.Width = width;

            mapUriRequest.Options = mapUriOptions;

            var imageryService = new ImageryServiceClient("BasicHttpBinding_IImageryService");
            MapUriResponse mapUriResponse = imageryService.GetMapUriAsync(mapUriRequest);

            return mapUriResponse.Uri;
        }
    }
}


