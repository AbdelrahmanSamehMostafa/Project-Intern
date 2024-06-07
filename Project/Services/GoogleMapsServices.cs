using HotelBookingSystem.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HotelBookingSystem.Services
{
    public class GoogleMapsServices
    {
        private readonly string _apiKey;

        public GoogleMapsServices(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<(double Latitude, double Longitude)> GetCoordinatesAsync(string address)
        {
            string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(address)}&key={_apiKey}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var geoResponse = JsonConvert.DeserializeObject<GeoResponse>(responseBody);

                if (geoResponse.Status == "OK")
                {
                    var location = geoResponse.Results[0].Geometry.Location;
                    return (location.Latitude, location.Longitude);
                }
                else
                {
                    throw new Exception("Unable to return latitude and longitude.");
                }
            }
        }

        public async Task<(string route, string duration)> GetRouteAsync(double originLat, double originLng, double destLat, double destLng)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = $"https://maps.googleapis.com/maps/api/directions/json?origin={originLat},{originLng}&destination={destLat},{destLng}&key={_apiKey}";
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(responseBody);

                var route = json["routes"]?[0]?["overview_polyline"]?["points"]?.ToString();
                var duration = json["routes"]?[0]?["legs"]?[0]?["duration"]?["text"]?.ToString();

                return (route, duration);
            }
        }

    }
}