using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelBookingSystem.Services;
using AutoMapper;
using HotelBookingSystem.Models;

namespace HotelBookingSystem.Controllers
{
    [Route("api/Hotel")]
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
        public async Task<IActionResult> GetAllHotels(string? orderBy = "")
        {
            IEnumerable<HotelDto> hotels;

            switch (orderBy.ToLower())
            {
                case "rating":
                    hotels = await _hotelRepo.GetAllHotelsOrderedByRating();
                    break;
                case "availablerooms":
                    hotels = await _hotelRepo.GetAllHotelsOrderedByAvailableRooms();
                    break;
                case "address":
                    hotels = await _hotelRepo.GetAllHotelsOrderedByAddress();
                    break;
                case "name":
                    hotels = await _hotelRepo.GetAllHotelsOrderedByName();
                    break;
                default:
                    hotels = await _hotelRepo.GetAllHotels();
                    break;
            }

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
                return NotFound(); // Hotel with the given id not found
            }
            await _hotelRepo.DeleteHotel(id);
            return NoContent(); // Hotel successfully deleted
        }



        [HttpPost]
        public async Task<IActionResult> AddHotel(HotelCreateDto simpleHotelCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hotelToCreate = _mapper.Map<Hotel>(simpleHotelCreateDto);

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

            return NoContent(); // Hotel successfully updated
        }
    }
}
