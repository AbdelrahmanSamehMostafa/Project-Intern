using HotelBookingSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/GoogleMaps")]
[Authorize]
[ApiController]
public class GoogleMapsController : ControllerBase
{
    private readonly GoogleMapsServices _googleMapsService;

    public GoogleMapsController(GoogleMapsServices googleMapsService)
    {
        _googleMapsService = googleMapsService;
    }

    [HttpGet("getCoordinates")]
    public async Task<IActionResult> GetCoordinates([FromQuery] string address)
    {
        if (string.IsNullOrWhiteSpace(address))
        {
            return BadRequest("Address is required");
        }

        try
        {
            var (latitude, longitude) = await _googleMapsService.GetCoordinatesAsync(address);
            return Ok(new { Latitude = latitude, Longitude = longitude });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpGet]
    [Route("RouteWithETA")]
    public async Task<IActionResult> GetRoute(double originLat, double originLng, double destLat, double destLng)
    {
        var (route, duration) = await _googleMapsService.GetRouteAsync(originLat, originLng, destLat, destLng);
        
        if (route == null || duration == null)
        {
            return BadRequest("Could not retrieve route information.");
        }

        return Ok(new { Route = route, Duration = duration });
    }



}
