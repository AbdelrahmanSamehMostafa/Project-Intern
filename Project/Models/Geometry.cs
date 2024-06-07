using Newtonsoft.Json;

namespace HotelBookingSystem.Models
{
    public class Geometry
    {
       [JsonProperty("location")]
        public Location Location { get; set; }
    }
}