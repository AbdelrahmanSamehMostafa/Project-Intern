using Newtonsoft.Json;

namespace HotelBookingSystem.Models
{
    public record Location
    {
        [JsonProperty("lat")]
        public double Latitude { get; set; }

    
        [JsonProperty("lng")]
        public double Longitude { get; set; }
    }
}