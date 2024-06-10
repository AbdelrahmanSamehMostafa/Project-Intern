using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingSystem.interfaces
{
    public interface IAdminRepository
    {
        public Task<IEnumerable<Admin>> GetAllAdminsAsync();
        public Task<Admin?> GetAdminByIdAsync(int adminId);
        public Task CreateAdminAsync(Admin admin);
        public Task UpdateAdminAsync(Admin admin);
        public Task DeleteAdminAsync(int adminId);
        public void CreateAdminRequest(Admin admin);
        public Task<bool?> GetAdminStatusByIdAsync(int adminId);
        public Task<bool> AdminExistsAsync(int adminId);

    }
}