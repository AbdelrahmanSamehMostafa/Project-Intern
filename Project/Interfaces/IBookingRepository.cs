using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.interfaces
{
    public interface IBookingRepository
    {
    Task<IEnumerable<Booking>> GetAllBookingsAsync();
    Task<IEnumerable<Booking>> GetBookingsByHotelIdAsync(int hotelId);
    Task<Booking> GetBookingByIdAsync(int id);
    Task<IActionResult> AddBookingAsync(Booking booking);
    Task<IActionResult> UpdateBookingAsync(Booking booking);
    Task<IEnumerable<Booking>> GetBookingsByCustomerIdAsync(int customerId); 
    Task DeleteBookingAsync(int id);
    }
}