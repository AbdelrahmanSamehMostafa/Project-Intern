using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using HotelBookingSystem.interfaces;
using HotelBookingSystem.Models;

namespace HotelBookingSystem.Services
{
    public class ValidationServices
    {
        
        private readonly ApplicationDbContext _context;
        

        public ValidationServices(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<UserValidationResult> ValidateUserCredentials(string email, string password)
        {
            var SuperAdmin=await _context.SuperAdmins.FindAsync(1);
            if(email==SuperAdmin.Name&&password==SuperAdmin.Password)
            {
                var superAdmin = await _context.SuperAdmins.Where(u => u.Name == email && u.Password == password)
                .FirstOrDefaultAsync();
                return new UserValidationResult { User = superAdmin, Role = "SuperAdmin" };
            }
            else
            {
            var customer = await _context.Customers
                .Where(u => u.Email == email && u.Password == password)
                .FirstOrDefaultAsync();
            if (customer != null)
            {
                return new UserValidationResult { User = customer, Role = "Customer" };
            }

            var admin = await _context.Admins
                .Where(u => u.Email == email && u.Password == password)
                .FirstOrDefaultAsync();
            if (admin != null)
            {
                return new UserValidationResult { User = admin, Role = "Admin" };
            }
            }
            return null;
        }
    }
}