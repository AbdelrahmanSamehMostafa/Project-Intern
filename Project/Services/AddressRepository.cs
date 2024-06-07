using AutoMapper;
using Microsoft.EntityFrameworkCore;
using HotelBookingSystem.Models;

namespace HotelBookingSystem.Services
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AddressRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<Address>> GetAllAddressesAsync()
        {
            return await _context.Addresses.ToListAsync();
        }

        public async Task<Address?> GetAddressByIdAsync(int addressId)
        {
            return await _context.Addresses.FirstOrDefaultAsync(a => a.AddressId == addressId);
        }

        public async Task CreateAddressAsync(Address address)
        {
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAddressAsync(Address address)
        {
            _context.Addresses.Update(address);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAddressAsync(int addressId)
        {
            var address = await GetAddressByIdAsync(addressId);
            if (address != null)
            {
                _context.Addresses.Remove(address);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> AddressExistsAsync(int id)
        {
            return await _context.Addresses.AnyAsync(a => a.AddressId == id);
        }
    }
}
