using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Services
{
    public class ValidationServices
    {
        private readonly ApplicationDbContext _context;

        public ValidationServices(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task<dynamic> ValidateUserCredentials(string email, string password)
        {
            var customer = await  _context.Customers.Where(u => u.Email == email && u.Password == password).FirstOrDefaultAsync();
            if(customer != null)
            {
                return customer;
            }

            var admin = await  _context.Admins.Where(u => u.Email == email && u.Password == password).FirstOrDefaultAsync();
            if(admin != null)
            {
                return admin;
            }

            return null;
            
        }
    }

}