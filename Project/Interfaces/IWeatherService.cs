using System.Threading.Tasks;
namespace HotelBookingSystem.interfaces
{
public interface IWeatherService
{
    Task<WeatherModel> GetWeatherAsync(string city);
}
}
