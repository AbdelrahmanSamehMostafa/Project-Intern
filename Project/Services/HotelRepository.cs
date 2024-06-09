using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBookingSystem.Models;

namespace HotelBookingSystem.Services
{
    public class HotelRepository : IHotelRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public HotelRepository(ApplicationDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HotelDto>> GetAllHotels(string orderBy, string filter)
        {
            IQueryable<Hotel> query = _dbContext.Hotels.Include(h => h.Address);

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(h => h.Name.Contains(filter) || h.Description.Contains(filter));
            }

            switch (orderBy.ToLower())
            {
                case "rating":
                    query = query.OrderByDescending(h => h.Rating);
                    break;
                case "availablerooms":
                    query = query.OrderByDescending(h => h.NumberOfAvailableRooms);
                    break;
                case "address":
                    query = query.OrderBy(h => h.Address.City);
                    break;
                case "name":
                    query = query.OrderBy(h => h.Name);
                    break;
                default:
                    break;
            }
            var hotels = await query.ToListAsync();
            return _mapper.Map<IEnumerable<HotelDto>>(hotels);
        }

        public async Task<HotelDto> GetHotelById(int id)
        {
            var hotel = await _dbContext.Hotels
                .Include(h => h.Address)
                .FirstOrDefaultAsync(h => h.HotelId == id);

            return _mapper.Map<HotelDto>(hotel);
        }

        public async Task<IEnumerable<Hotel>> GetHotelsByIdsAsync(IEnumerable<string> hotelIds)
        {
            var ids = hotelIds.Select(id => int.Parse(id)).ToList();
            var hotels = await _dbContext.Hotels
                .Where(h => ids.Contains(h.HotelId))
                .Include(h => h.Address)
                .ToListAsync();

            return hotels;
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

        public async Task UpdateHotel(int id, HotelUpdateDto hotelUpdateDto)
        {
            var existingHotel = await _dbContext.Hotels.FirstOrDefaultAsync(h => h.HotelId == id);
            if (existingHotel == null)
            {
                throw new ArgumentException($"Hotel with id {id} not found.");
            }

            _mapper.Map(hotelUpdateDto, existingHotel);
            _dbContext.Hotels.Update(existingHotel);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> HotelExistsAsync(int id)
        {
            return await _dbContext.Hotels.AnyAsync(c => c.HotelId == id);
        }
    }
}
