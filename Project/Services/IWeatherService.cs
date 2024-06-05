using System.Threading.Tasks;

public interface IWeatherService
{
    Task<WeatherModel> GetWeatherAsync(string city);
}
