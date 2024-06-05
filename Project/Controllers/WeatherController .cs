using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HotelBookingSystem.Services;

public class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;
    private readonly ILogger<WeatherController> _logger;

    public WeatherController(IWeatherService weatherService, ILogger<WeatherController> logger)
    {
        _weatherService = weatherService;
        _logger = logger;
    }

    [HttpGet("weather/{city}")]
    public async Task<ActionResult<WeatherModel>> GetWeather(string city)
    {
        try
        {
            var weather = await _weatherService.GetWeatherAsync(city);
            return Ok(weather);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to get weather data for {city}");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
