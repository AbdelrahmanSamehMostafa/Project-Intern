using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using HotelBookingSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelBookingSystem.interfaces;
using HotelBookingSystem.Services;
using Microsoft.AspNetCore.Authorization;

namespace HotelBookingSystem.Controllers
{
    [Route("api/Booking")]
    [Authorize]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public BookingController(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDTO>>> GetBookings()
        {
            var bookings = await _bookingRepository.GetAllBookingsAsync();
            return Ok(_mapper.Map<IEnumerable<BookingDTO>>(bookings));
        }


        [HttpGet("HotelBookings/{hotelId}")]
        public async Task<ActionResult<IEnumerable<BookingDTO>>> GetBookingsByHotelId(int hotelId)
        {
            var bookings = await _bookingRepository.GetBookingsByHotelIdAsync(hotelId);
            return Ok(_mapper.Map<IEnumerable<BookingDTO>>(bookings));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDTO>> GetBooking(int id)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<BookingDTO>(booking));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, BookingBaseDTO bookingDTO)
        {
            var existingBooking = await _bookingRepository.GetBookingByIdAsync(id);
            if (existingBooking == null)
            {
                return NotFound();
            }
            _mapper.Map(bookingDTO, existingBooking);
            var result = await _bookingRepository.UpdateBookingAsync(existingBooking);
            return Ok(result);
        }


        [HttpPost("{customerID}/{RoomID}")]
        public async Task<IActionResult> CreateBooking(BookingBaseDTO bookingDTO, int RoomID, int customerID)
        {
            var bookingForCreating = new BookingDTO
            {
                CustomerId = customerID,
                RoomId = RoomID,
                CheckInDate = bookingDTO.CheckInDate,
                CheckOutDate = bookingDTO.CheckOutDate,
                Status = bookingDTO.Status
            };
            var booking = _mapper.Map<Booking>(bookingForCreating);
            var result = await _bookingRepository.AddBookingAsync(booking);
            if (result is OkObjectResult okResult)
            {
                var createdBooking = okResult.Value as Booking;
                var bookingDto = _mapper.Map<BookingDTO>(createdBooking);
                return Ok(bookingDto);
            }
            return result;
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            await _bookingRepository.DeleteBookingAsync(id);
            return NoContent();
        }


        [HttpGet("CustomerBookings/{customerId}")]
        public async Task<ActionResult<IEnumerable<BookingDTO>>> GetBookingsByCustomerId(int customerId)
        {
            var bookings = await _bookingRepository.GetBookingsByCustomerIdAsync(customerId);
            return Ok(_mapper.Map<IEnumerable<BookingDTO>>(bookings));
        }
    }
}