using HotelBookingSystem.Models;

namespace HotelBookingSystem.Services
{
    public interface IRoomRepository
    {
        public Task<bool> RoomExistsAsync(int id);
        public Task<IEnumerable<Room>> GetAllRoomsAsync();

        public Task<Room?> GetRoomByIdAsync(int roomId);

        public Task CreateRoomAsync(Room room);

        public Task UpdateRoomAsync(Room room);

        public Task DeleteRoomAsync(int roomId);
        
    }
}