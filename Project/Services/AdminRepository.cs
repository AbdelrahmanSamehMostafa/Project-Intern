using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Services
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AdminRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<Admin>> GetAllAdminsAsync()
        {
            return await _dbContext.Admins.ToListAsync();
        }

        public async Task<Admin?> GetAdminByIdAsync(int adminId)
        {
            return await _dbContext.Admins.Where(e => e.AdminId == adminId).FirstOrDefaultAsync();
        }
        public async void CreateAdminRequest(Admin admin)
        {
            var RequestToAdd = new PendingReq
            {
                AdminID = admin.AdminId,
                SuperAdminID = 1,
                AdminName=admin.FirstName+" "+ admin.LastName,
                AdminMail=admin.Email
            };
            _dbContext.PendingReqs.Add(RequestToAdd);
        }

        public async Task CreateAdminAsync(Admin admin)
        {
            if (admin == null)
            {
                throw new ArgumentNullException(nameof(admin));
            }

            _dbContext.Admins.Add(admin);

            await _dbContext.SaveChangesAsync();
            CreateAdminRequest(admin);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAdminAsync(Admin admin)
        {
            if (admin == null)
            {
                throw new ArgumentNullException(nameof(admin));
            }

            _dbContext.Admins.Update(admin);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAdminAsync(int adminId)
        {
            var admin = await _dbContext.Admins.FindAsync(adminId);
            if (admin == null)
            {
                throw new ArgumentNullException(nameof(admin));
            }
            _dbContext.Admins.Remove(admin);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> AdminExistsAsync(int adminId)
        {
            return await _dbContext.Admins.AnyAsync(c => c.AdminId == adminId);
        }

        public async Task<bool?> GetAdminStatusByIdAsync(int adminId)
        {
            var admin = await _dbContext.Admins
                .Where(a => a.AdminId == adminId)
                .Select(a => a.IsActive)
                .FirstOrDefaultAsync();
            return admin;
        }
    }
}