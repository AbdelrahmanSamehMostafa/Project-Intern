using Newtonsoft.Json;

namespace HotelBookingSystem.Models
{
    public class GeoResponse
    {
    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("results")]
    public Result[] Results { get; set; }
    }
}