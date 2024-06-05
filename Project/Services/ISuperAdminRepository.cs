using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Services
{
    public interface ISuperAdminRepository
{
    
    Task<IEnumerable<PendingReq>> GetPendingRequests();
    Task AcceptRequest(int requestId);
    Task RejectRequest(int requestId);
}
}


