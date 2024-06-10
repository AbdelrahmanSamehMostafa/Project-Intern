namespace HotelBookingSystem.interfaces
{
    public interface IAddressRepository
    {
        public Task<bool> AddressExistsAsync(int id);
        public Task<IEnumerable<Address>> GetAllAddressesAsync();
        public Task<Address?> GetAddressByIdAsync(int addressId);
        public Task CreateAddressAsync(Address address);
        public Task UpdateAddressAsync(Address address);
        public Task DeleteAddressAsync(int addressId);
    }
}