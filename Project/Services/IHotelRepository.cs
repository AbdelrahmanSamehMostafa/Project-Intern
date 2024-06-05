using System.Threading.Tasks;
using System.Collections.Generic;

namespace HotelBookingSystem.Services
{
    public interface IHotelRepository
    {
        Task<IEnumerable<HotelDto>> GetAllHotels();
        Task<HotelDto> GetHotelById(int id);
        Task DeleteHotel(int id);
        Task AddHotel(HotelCreateDto hotelCreateDto);

        Task<IEnumerable<HotelDto>> GetAllHotelsOrderedByRating();
        Task<IEnumerable<HotelDto>> GetAllHotelsOrderedByAvailableRooms();
        Task<IEnumerable<HotelDto>> GetAllHotelsOrderedByAddress();
        Task<IEnumerable<HotelDto>> GetAllHotelsOrderedByName();
        Task UpdateHotel(int id, HotelUpdateDto hotelUpdateDto);
    }
}
