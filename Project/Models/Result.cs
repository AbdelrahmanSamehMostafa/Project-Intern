using Newtonsoft.Json;

namespace HotelBookingSystem.Models
{
    public class Result
    {
        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }
    }
}