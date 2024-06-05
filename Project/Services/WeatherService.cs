using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<WeatherService> _logger;
    private readonly string _apiKey;

    public WeatherService(HttpClient httpClient, ILogger<WeatherService> logger, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _logger = logger;
        _apiKey = configuration["WeatherApi:ApiKey"];
    }

    public async Task<WeatherModel> GetWeatherAsync(string city)
{
    string url = $"http://api.weatherapi.com/v1/current.json?key={_apiKey}&q={city}&aqi=no";

    try
    {
        var response = await _httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var weatherData = JsonConvert.DeserializeObject<WeatherApiResponse>(json);

            // Handle null or empty values gracefully
            string cityName = !string.IsNullOrEmpty(weatherData?.Location?.Name) ? weatherData.Location.Name : "Unknown";
            double temperature = weatherData?.Current?.TempC ?? 0;
            double humidity = weatherData?.Current?.Humidity ?? 0;
            double windSpeed = weatherData?.Current?.WindKph ?? 0;

            return new WeatherModel
            {
                City = cityName,
                Temperature = temperature,
                Humidity = humidity,
                WindSpeed = windSpeed
            };
        }
        else
        {
            _logger.LogError($"Failed to retrieve weather data. Status Code: {response.StatusCode}, Reason: {response.ReasonPhrase}");
            throw new HttpRequestException($"Failed to retrieve weather data. Status Code: {response.StatusCode}");
        }
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, $"Exception occurred while retrieving weather data for {city}");
        throw;
    }
}

}
