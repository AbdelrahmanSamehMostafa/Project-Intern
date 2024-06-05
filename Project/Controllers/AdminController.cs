using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelBookingSystem.Models;
using HotelBookingSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HotelBookingSystem.Controllers
{
    [Route("api/Admin")]
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


        [HttpPost]
        public async Task<ActionResult<AdminDTO>> CreateAdmin(AdminForCreationDTO admin)
        {
            var adminEntity = _mapper.Map<Admin>(admin);
            await _adminRepository.CreateAdminAsync(adminEntity);

            var adminToReturn = _mapper.Map<AdminDTO>(admin);
            return CreatedAtRoute("GetAdminById", new { adminId = adminToReturn.AdminId }, adminToReturn);
        }

        [HttpPut("{adminId}")]
        public async Task<IActionResult> UpdateAdmin(int adminId, AdminDTO adminDTO)
        {
            if (adminId != adminDTO.AdminId)
            {
                return BadRequest();
            }

            var admin = _mapper.Map<Admin>(adminDTO);
            await _adminRepository.UpdateAdminAsync(admin);
            return NoContent();
        }

        [HttpDelete("{adminId}")]
        public async Task<IActionResult> DeleteAdmin(int adminId)
        {
            await _adminRepository.DeleteAdminAsync(adminId);
            return NoContent();
        }
    }
}