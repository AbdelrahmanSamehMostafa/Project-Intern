using System.Threading.Tasks;
using System.Collections.Generic;
using HotelBookingSystem.Models;

namespace HotelBookingSystem.interfaces
{
    public interface IHotelRepository
    {
        Task<IEnumerable<HotelDto>> GetAllHotels(string orderBy, string filter);
        Task<HotelDto> GetHotelById(int id);
        Task DeleteHotel(int id);
        Task AddHotel(Hotel hotel);
        Task<IEnumerable<Hotel>> GetHotelsByIdsAsync(IEnumerable<string> hotelIds);
        Task UpdateHotel(int id, HotelUpdateDto hotelUpdateDto);
        Task<bool> HotelExistsAsync(int id);
        Task<IEnumerable<Hotel>> GetHotelsByAdminId(int adminId);
    }
}
