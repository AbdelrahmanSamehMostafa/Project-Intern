using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<IEnumerable<HotelDto>> GetAllHotelsOrderedByRating()
        {
            var hotels = await _dbContext.Hotels.Include(h => h.Address).OrderByDescending(h => h.Rating).ToListAsync();
            return _mapper.Map<IEnumerable<HotelDto>>(hotels);
        }

        public async Task<IEnumerable<HotelDto>> GetAllHotelsOrderedByAvailableRooms()
        {
            var hotels = await _dbContext.Hotels.Include(h => h.Address).OrderByDescending(h => h.NumberOfAvailableRooms).ToListAsync();
            return _mapper.Map<IEnumerable<HotelDto>>(hotels);
        }

        public async Task<IEnumerable<HotelDto>> GetAllHotelsOrderedByAddress()
        {
            var hotels = await _dbContext.Hotels.Include(h => h.Address).OrderBy(h => h.Address).ToListAsync();
            return _mapper.Map<IEnumerable<HotelDto>>(hotels);
        }

        public async Task<IEnumerable<HotelDto>> GetAllHotelsOrderedByName()
        {
            var hotels = await _dbContext.Hotels.Include(h => h.Address).OrderBy(h => h.Name).ToListAsync();
            return _mapper.Map<IEnumerable<HotelDto>>(hotels);
        }

        public async Task<IEnumerable<HotelDto>> GetAllHotels()
        {
            var hotels = await _dbContext.Hotels.Include(h => h.Address).ToListAsync();
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

            await _dbContext.SaveChangesAsync();
        }
    }
}
