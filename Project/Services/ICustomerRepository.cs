using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingSystem.Services
{
    public interface ICustomerRepository
    {
        public Task<IEnumerable<Customer>> GetAllCustomersAsync();
        public Task<Customer?> GetCustomerByIdAsync(int customerId);
        public Task CreateCustomerAsync(Customer customer);
        public Task UpdateCustomerAsync(Customer customer);
        public Task DeleteCustomerAsync(int customerId);
        public Task<bool> CustomerExistsAsync(int id);
        public Task<Customer> GetCustomerByEmailAsync(string email);
    }
}


