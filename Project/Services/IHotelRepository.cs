using System.Threading.Tasks;
using System.Collections.Generic;
namespace HotelBookingSystem.Services
{
public interface IHotelRepository
{
    Task<IEnumerable<Hotel>> GetAllHotels();
    Task<Hotel> GetHotelById(int id);
    Task DeleteHotel(int id);
    Task AddHotel(Hotel hotel);

    Task<IEnumerable<Hotel>> GetAllHotelsOrderedByRating();
    Task<IEnumerable<Hotel>> GetAllHotelsOrderedByAvailableRooms();
    Task<IEnumerable<Hotel>> GetAllHotelsOrderedByAddress();
    Task<IEnumerable<Hotel>> GetAllHotelsOrderedByName();
}
}