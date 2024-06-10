using HotelBookingSystem.Models;
using Microsoft.EntityFrameworkCore;
using HotelBookingSystem.interfaces;

namespace HotelBookingSystem.Services
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDbContext _context;

        public RoomRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Room>> GetAllRoomsAsync()
        {
            return await _context.Rooms.AsNoTracking().ToListAsync();
        }

        public async Task<Room?> GetRoomByIdAsync(int roomId)
        {
            var room = await _context.Rooms.AsNoTracking().Where(r => r.RoomId == roomId).FirstOrDefaultAsync();

            if (room == null)
                return null;

            return room;
        }

        public async Task CreateRoomAsync(Room room)
        {
            if (room == null)
                throw new ArgumentNullException(nameof(room));

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRoomAsync(Room room)
        {
            if (room == null)
                throw new ArgumentNullException(nameof(room));

            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoomAsync(int roomId)
        {
            if (roomId == 0)
                throw new ArgumentNullException(nameof(roomId));


            var room = await GetRoomByIdAsync(roomId);
            if (room == null)
                throw new ArgumentNullException(nameof(room));

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> RoomExistsAsync(int id)
        {
            return await _context.Rooms.AnyAsync(r => r.RoomId == id);
        }
        public async Task<IEnumerable<Room>> GetRoomsByHotelIdAsync(int hotelId)
        {
            return await _context.Rooms
                .AsNoTracking()
                .Where(r => r.HotelId == hotelId)
                .ToListAsync();
        }
    }
}