using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelBookingSystem.interfaces;

namespace HotelBookingSystem.Services
{
public class SuperAdminRepository : ISuperAdminRepository
{
    private readonly ApplicationDbContext _dbContext;

    public SuperAdminRepository(ApplicationDbContext context)
    {
        _dbContext = context;
    }
    public async Task<IEnumerable<PendingReq>> GetPendingRequests()
    {
        return await _dbContext.PendingReqs.AsNoTracking().ToListAsync();
    }

    public async Task AcceptRequest(int requestId)
    {
        var request = await _dbContext.PendingReqs.FindAsync(requestId);
        if (request != null)
        {
            var tempAdmin=_dbContext.Admins.Where(e=>e.AdminId==request.AdminID).FirstOrDefault();
            tempAdmin.IsActive = true;
            _dbContext.PendingReqs.Remove(request);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task RejectRequest(int requestId)
    {
        var request = await _dbContext.PendingReqs.FindAsync(requestId);
        if (request != null)
        {
            _dbContext.PendingReqs.Remove(request);
            var admin = await _dbContext.Admins.FindAsync(request.AdminID);
            _dbContext.Remove(admin);
            await _dbContext.SaveChangesAsync();
        }
    }
}
}
