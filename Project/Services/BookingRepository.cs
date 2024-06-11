using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelBookingSystem.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.Services
{
public class BookingRepository : IBookingRepository
{
    private readonly ApplicationDbContext _context;

    public BookingRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
    {
        return await _context.Bookings.AsNoTracking().Include(b => b.Customer).Include(b => b.Room).ToListAsync();
    }

    public async Task<IEnumerable<Booking>> GetBookingsByHotelIdAsync(int hotelId)
    {
        return await _context.Bookings.AsNoTracking().Include(b => b.Customer).Include(b => b.Room)
            .Where(b => b.Room.HotelId == hotelId).ToListAsync();
    }

    public async Task<Booking> GetBookingByIdAsync(int id)
    {
         var booking = await _context.Bookings.AsNoTracking().Include(b => b.Customer).Include(b => b.Room)
            .FirstOrDefaultAsync(b => b.BookingId == id);

            if (booking != null){return booking;}

        return null;
    }

    public async Task<IActionResult> AddBookingAsync(Booking booking)
    {
        if (booking.RoomId == 0)
        {
            return new BadRequestObjectResult(new { Message = "RoomId cannot be null or zero." });
        }

        var room = await _context.Rooms.FindAsync(booking.RoomId);

        if (room == null)
        {
            return new NotFoundObjectResult(new { Message = "Room not found." });
        }

        if (room.isAvailable && !room.isBooked)
        {
            room.isBooked = true;
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return new OkObjectResult(booking);
        }
        else
        {
            return new BadRequestObjectResult(new { Message = "Room is not available or already booked." });
        }
    }

        public async Task<IActionResult> UpdateBookingAsync(Booking booking)
        {
        try
        {
            _context.Entry(booking).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new NoContentResult();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BookingExists(booking.BookingId))
            {
                return new NotFoundResult();
            }
            else
            {
                throw;
            }
        }
        }

private bool BookingExists(int id)
{
    return _context.Bookings.Any(e => e.BookingId == id);
}


    public async Task DeleteBookingAsync(int id)
    {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking != null)
        {
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
        }
    }

     public async Task<IEnumerable<Booking>> GetBookingsByCustomerIdAsync(int customerId)
    {
        return await _context.Bookings.AsNoTracking().Include(b => b.Customer).Include(b => b.Room)
            .Where(b => b.CustomerId == customerId).ToListAsync();
    }
}
}