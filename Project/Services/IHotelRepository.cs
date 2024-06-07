using System.Threading.Tasks;
using System.Collections.Generic;
using HotelBookingSystem.Models;

namespace HotelBookingSystem.Services
{
    public interface IHotelRepository
    {
        Task<IEnumerable<HotelDto>> GetAllHotels();
        Task<HotelDto> GetHotelById(int id);
        Task DeleteHotel(int id);
        Task AddHotel(Hotel hotel);

        Task<IEnumerable<HotelDto>> GetAllHotelsOrderedByRating();
        Task<IEnumerable<HotelDto>> GetAllHotelsOrderedByAvailableRooms();
        Task<IEnumerable<HotelDto>> GetAllHotelsOrderedByAddress();
        Task<IEnumerable<HotelDto>> GetAllHotelsOrderedByName();
        Task UpdateHotel(int id, HotelUpdateDto hotelUpdateDto);
    }
}
