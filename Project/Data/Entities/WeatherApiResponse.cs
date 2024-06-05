using Newtonsoft.Json;

public class WeatherApiResponse
{
    [JsonProperty("location")]
    public Location Location { get; set; }

    [JsonProperty("current")]
    public Current Current { get; set; }
}