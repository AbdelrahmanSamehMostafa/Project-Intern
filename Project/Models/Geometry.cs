using Newtonsoft.Json;

namespace HotelBookingSystem.Models
{
    public record Geometry
    {
       [JsonProperty("location")]
        public Location Location { get; set; }
    }
}