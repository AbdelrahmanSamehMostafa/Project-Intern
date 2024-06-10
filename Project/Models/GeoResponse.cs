using Newtonsoft.Json;

namespace HotelBookingSystem.Models
{
    public record GeoResponse
    {
    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("results")]
    public Result[] Results { get; set; }
    }
}