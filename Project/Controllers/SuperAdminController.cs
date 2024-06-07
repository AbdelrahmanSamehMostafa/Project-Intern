using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelBookingSystem.Models;
using HotelBookingSystem.Services;

namespace HotelBookingSystem.Controllers
{
    [ApiController]
    [Route("api/SuperAdmin")]
    public class SuperAdminController : ControllerBase
    {
        private readonly ISuperAdminRepository _superAdminRepo;

        public SuperAdminController(ISuperAdminRepository superAdminRepo)
        {
            _superAdminRepo = superAdminRepo;
        }

        [HttpGet("requests")]
        public async Task<IActionResult> GetPendingRequests()
        {
            var requests = await _superAdminRepo.GetPendingRequests();
            return Ok(requests);
        }

        [HttpPost("accept/{requestId}")]
        public async Task<IActionResult> AcceptRequest(int requestId)
        {
            await _superAdminRepo.AcceptRequest(requestId);
            return Ok();
        }

        [HttpPost("reject/{requestId}")]
        public async Task<IActionResult> RejectRequest(int requestId)
        {
            await _superAdminRepo.RejectRequest(requestId);
            return Ok();
        }
    }
}
