using AutoMapper;
using HotelBookingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HotelBookingSystem.interfaces;
using Project.Models;

namespace HotelBookingSystem.Controllers
{
    [Route("api/Admin")]
    [Authorize]
    [ApiController]
    public class AdminController : Controller
    {

        private IAdminRepository _adminRepository;
        private IMapper _mapper;


        public AdminController(IAdminRepository adminRepository, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAdmins()
        {
            var admins = await _adminRepository.GetAllAdminsAsync();
            if (admins == null)
            {
                return NotFound();
            }
            var adminDTOs = admins.Select(admin => _mapper.Map<AdminDTO>(admin)).ToList();
            return Ok(adminDTOs);
        }

        [HttpGet("{adminId}", Name = "GetAdminById")]
        public async Task<IActionResult> GetAdminById(int adminId)
        {
            var admin = await _adminRepository.GetAdminByIdAsync(adminId);
            if (admin == null)
            {
                return NotFound();
            }
            var adminDtoToReturn = _mapper.Map<AdminDTO>(admin);
            return Ok(adminDtoToReturn);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<AdminDTO>> CreateAdmin(AdminForCreationDTO admin)
        {
            var adminEntity = _mapper.Map<Admin>(admin);
            adminEntity.SuperAdminId = 1;
            await _adminRepository.CreateAdminAsync(adminEntity);

            var adminToReturn = _mapper.Map<AdminForCreationDTO>(admin);
            return Ok(adminToReturn);
        }

        [HttpPut("{adminId}")]
        public async Task<IActionResult> UpdateAdmin(int adminId, AdminForUpdateDTO admin)
        {

            var existingAdmin = await _adminRepository.GetAdminByIdAsync(adminId);

            // Map properties from customerDTO to existingCustomer
            _mapper.Map(admin, existingAdmin);

            // Update the customer in the repository
            await _adminRepository.UpdateAdminAsync(existingAdmin);
            return NoContent();
        }

        [HttpDelete("{adminId}")]
        public async Task<IActionResult> DeleteAdmin(int adminId)
        {
            await _adminRepository.DeleteAdminAsync(adminId);
            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("{adminId}/status")]
        public async Task<IActionResult> GetAdminStatus(int adminId)
        {
            var adminExists = await _adminRepository.AdminExistsAsync(adminId);
            if (!adminExists)
            {
                return NotFound("Admin not found");
            }

            var isActive = await _adminRepository.GetAdminStatusByIdAsync(adminId);
            if (isActive == null)
            {
                return NotFound();
            }

            return Ok(new { AdminId = adminId, IsActive = isActive });
        }

    }
}
