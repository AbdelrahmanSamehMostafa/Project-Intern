using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingSystem.Services
{
    public interface IAdminRepository
    {
        public Task<IEnumerable<Admin>> GetAllAdminsAsync();
        public Task<Admin?> GetAdminByIdAsync(int adminId);
        public Task CreateAdminAsync(Admin admin);
        public Task UpdateAdminAsync(Admin admin);
        public Task DeleteAdminAsync(int adminId);
    }
}