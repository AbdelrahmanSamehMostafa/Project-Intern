using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HotelBookingSystem.Services;
using Microsoft.AspNetCore.Authorization;
using HotelBookingSystem.interfaces;

namespace HotelBookingSystem.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/weather")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        private readonly IHotelRepository _hotelRepository;
        private readonly ILogger<WeatherController> _logger;


        public WeatherController(IWeatherService weatherService, ILogger<WeatherController> logger, IHotelRepository hotelRepository)
        {
            _weatherService = weatherService;
            _logger = logger;
            _hotelRepository = hotelRepository;
        }

        [HttpGet("{city}")]
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

        [HttpGet("{HotelId:int}")]
        public async Task<ActionResult<WeatherModel>> GetWeatherbyHotelId(int HotelId)
        {
            var hotel = await _hotelRepository.GetHotelById(HotelId);

            if (hotel == null)
            {
                return NotFound($"Hotel with ID {HotelId} not found.");
            }
            var weather = await _weatherService.GetWeatherAsync(hotel.Address.City);
            return Ok(weather);
        }
    }
}