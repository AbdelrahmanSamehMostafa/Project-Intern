using Newtonsoft.Json;

public class Current
{
    [JsonProperty("temp_c")]
    public double TempC { get; set; }

    [JsonProperty("humidity")]
    public double Humidity { get; set; }

    [JsonProperty("wind_kph")]
    public double WindKph { get; set; }
}