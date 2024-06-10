using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using HotelBookingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using HotelBookingSystem.interfaces;

namespace HotelBookingSystem.Controllers
{
    [Route("api/Hotel")]
    [Authorize]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepo;
        private readonly IMapper _mapper;

        public HotelController(IHotelRepository hotelRepo, IMapper mapper)
        {
            _hotelRepo = hotelRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHotels(string? orderBy = "", string? filter = "")
        {
            var hotels = await _hotelRepo.GetAllHotels(orderBy, filter);
            return Ok(hotels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotelById(int id)
        {
            var hotel = await _hotelRepo.GetHotelById(id);
            if (hotel == null)
            {
                return NotFound();
            }
            return Ok(hotel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _hotelRepo.GetHotelById(id);
            if (hotel == null)
            {
                return NotFound();
            }
            await _hotelRepo.DeleteHotel(id);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> AddHotel(HotelCreateDto hotelCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hotelToCreate = _mapper.Map<Hotel>(hotelCreateDto);

            await _hotelRepo.AddHotel(hotelToCreate);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotel(int id, HotelUpdateDto hotelUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingHotel = await _hotelRepo.GetHotelById(id);
            if (existingHotel == null)
            {
                return NotFound();
            }

            await _hotelRepo.UpdateHotel(id, hotelUpdateDto);

            return NoContent();
        }

        [HttpGet("admin/{adminId}")]
        public async Task<IActionResult> GetHotelsByAdminId(int adminId)
        {
            var hotels = await _hotelRepo.GetHotelsByAdminId(adminId);
            if (hotels == null || !hotels.Any())
            {
                return NotFound();
            }

            var hotelDtos = _mapper.Map<List<HotelDto>>(hotels);
            return Ok(hotelDtos);
        }
    }
}
