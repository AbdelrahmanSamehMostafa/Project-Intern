using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Services
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _context;

        public AddressRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        

        // public async Task<IEnumerable<Address>> GetAllAddressesAsync()
        // {
        //     return await _context.Addresses.Include(a=> a.Hotel).ToListAsync();
        // }

        // public async Task<Address?> GetAddressByIdAsync(int addressId)
        // {
        //     var address = await _context.Addresses.Include(a=> a.Hotel).Where(a => a.AddressId == addressId).FirstOrDefaultAsync();
            
        //     if(address == null)
        //         return null;

        //     return address;
        // }

        public async Task CreateAddressAsync(Address address)
        {
            if(address == null)
                throw new ArgumentNullException(nameof(address));

            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAddressAsync(Address address)
        {
            if(address == null)
                throw new ArgumentNullException(nameof(address));

            _context.Addresses.Update(address);
            await _context.SaveChangesAsync();
        }

        // public async Task DeleteAddressAsync(int addressId)
        // {
        //     if(addressId == 0)
        //         throw new ArgumentNullException(nameof(addressId));

        //     var address = await GetAddressByIdAsync(addressId);
        //     if (address == null)
        //         throw new ArgumentNullException(nameof(address));
            
        //     _context.Addresses.Remove(address);
        //     await _context.SaveChangesAsync();
        // }

        public async Task<bool> AddressExistsAsync(int id)
        {
            return await _context.Addresses.AnyAsync(a => a.AddressId == id);
        }
    }
    
}