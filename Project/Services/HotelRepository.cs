using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace HotelBookingSystem.Services
{
public class HotelRepository : IHotelRepository
{
    private readonly ApplicationDbContext _dbContext;

    public HotelRepository(ApplicationDbContext context)
    {
        _dbContext = context;
    }

    public async Task<IEnumerable<Hotel>> GetAllHotelsOrderedByRating()
    {
        return await _dbContext.Hotels.OrderByDescending(h => h.Rating).ToListAsync();
    }

    public async Task<IEnumerable<Hotel>> GetAllHotelsOrderedByAvailableRooms()
    {
        return await _dbContext.Hotels.OrderByDescending(h => h.NumberOfAvailableRooms).ToListAsync();
    }

    public async Task<IEnumerable<Hotel>> GetAllHotelsOrderedByAddress()
    {
        return await _dbContext.Hotels.OrderBy(h => h.Address).ToListAsync();
    }

    public async Task<IEnumerable<Hotel>> GetAllHotelsOrderedByName()
    {
        return await _dbContext.Hotels.OrderBy(h => h.Name).ToListAsync();
    }

    public async Task<IEnumerable<Hotel>> GetAllHotels()
    {
         return await _dbContext.Hotels.ToListAsync();
    }

    public async Task<Hotel> GetHotelById(int id)
    {
        return await _dbContext.Hotels.FirstOrDefaultAsync(h => h.HotelId == id);
    }

    public async Task DeleteHotel(int id)
    {
        var hotelToRemove = await _dbContext.Hotels.FirstOrDefaultAsync(h => h.HotelId == id);
        if (hotelToRemove != null)
        {
            _dbContext.Hotels.Remove(hotelToRemove);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task AddHotel(Hotel hotel)
    {
        if (hotel == null)
        {
            throw new ArgumentNullException(nameof(hotel));
        }
        _dbContext.Hotels.Add(hotel);
        await _dbContext.SaveChangesAsync();
    }
}
}