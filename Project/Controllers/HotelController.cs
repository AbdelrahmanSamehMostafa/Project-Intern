using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelBookingSystem.Services;
namespace HotelBookingSystem.Controllers
{
    [Route("api/Hotel")]
    [ApiController]
    
    public class HotelController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepo;

        public HotelController(IHotelRepository hotelRepo)
        {
            _hotelRepo = hotelRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHotels(string? orderBy = "")
        {
            IEnumerable<Hotel> hotels;

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

        // POST: api/hotel
        [HttpPost]
        public async Task<IActionResult> AddHotel(Hotel hotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Invalid hotel data
            }
            await _hotelRepo.AddHotel(hotel);
            return CreatedAtAction(nameof(GetHotelById), new { id = hotel.HotelId }, hotel); // Hotel successfully added
        }
    }
}
