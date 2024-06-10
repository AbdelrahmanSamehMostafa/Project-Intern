using Newtonsoft.Json;

namespace HotelBookingSystem.Models
{
    public record Result
    {
        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }
    }
}