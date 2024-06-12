using AutoMapper;
using HotelBookingSystem.Models;
using HotelBookingSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HotelBookingSystem.interfaces;

namespace HotelBookingSystem.Controllers
{
    [Route("api/Rooms")]
    [Authorize]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public RoomController(IRoomRepository roomRepository, IMapper mapper, IHotelRepository hotelRepository)
        {
            _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _hotelRepository = hotelRepository ?? throw new ArgumentNullException(nameof(hotelRepository));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRooms()
        {
            var rooms = await _roomRepository.GetAllRoomsAsync();
            return Ok(_mapper.Map<IEnumerable<RoomDTO>>(rooms));
        }

        [HttpGet("Hotel/{hotelId}")]
        public async Task<IActionResult> GetRoomsByHotelId(int hotelId)
        {
            var rooms = await _roomRepository.GetRoomsByHotelIdAsync(hotelId);
            return Ok(_mapper.Map<IEnumerable<RoomWithIdDTO>>(rooms));
        }

        [HttpGet]
        [Route("{roomId}", Name = "GetRoomById")]
        public async Task<IActionResult> GetRoomById(int roomId)
        {
            var room = await _roomRepository.GetRoomByIdAsync(roomId);
            if (room == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<RoomDTO>(room));
        }

        [HttpPost]
        [Route("{HotelId}")]
        public async Task<IActionResult> CreateRoom(int HotelId, [FromBody] RoomDTO roomdto)
        {
            var roomEntity = _mapper.Map<Room>(roomdto);
            roomEntity.HotelId = HotelId;

            await _roomRepository.CreateRoomAsync(roomEntity);

            var roomToReturn = _mapper.Map<RoomWithIdDTO>(roomEntity);
            roomToReturn.isAvailable = roomdto.isAvailable;

            return CreatedAtRoute("GetRoomById", new { roomId = roomToReturn.RoomId }, roomToReturn);
        }


        [HttpPut]
        [Route("{roomId}")]
        public async Task<IActionResult> UpdateRoom(int roomId, [FromBody] RoomWithIdDTO roomWithIdDTO)
        {
            var room = await _roomRepository.GetRoomByIdAsync(roomId);
            if (room == null)
            {
                return NotFound();
            }

            if (roomWithIdDTO.RoomId != roomId)
                return BadRequest();
            _mapper.Map(roomWithIdDTO, room);
            room.isAvailable = roomWithIdDTO.isAvailable;
            await _roomRepository.UpdateRoomAsync(room);
            return NoContent();
        }

        [HttpDelete]
        [Route("{roomId}")]
        public async Task<IActionResult> DeleteRoom(int roomId)
        {
            if (!await _roomRepository.RoomExistsAsync(roomId))
            {
                return NotFound();
            }

            await _roomRepository.DeleteRoomAsync(roomId);
            return NoContent();
        }
    }
}