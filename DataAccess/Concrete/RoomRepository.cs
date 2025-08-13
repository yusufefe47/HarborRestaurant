using HarborRestaurant.DataAccess.Abstract;
using HarborRestaurant.DataAccess.Context;
using HarborRestaurant.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace HarborRestaurant.DataAccess.Concrete
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HarborDbContext _context;

        public RoomRepository(HarborDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            return await _context.Rooms.OrderBy(r => r.SortOrder).ToListAsync();
        }

        public async Task<IEnumerable<Room>> GetActiveRoomsAsync()
        {
            return await _context.Rooms
                .Where(r => r.IsActive)
                .OrderBy(r => r.SortOrder)
                .ToListAsync();
        }

        public async Task<Room?> GetByIdAsync(int id)
        {
            return await _context.Rooms.FindAsync(id);
        }

        public async Task<Room> CreateAsync(Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task<Room> UpdateAsync(Room room)
        {
            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task DeleteAsync(int id)
        {
            var room = await GetByIdAsync(id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetTotalRoomCountAsync()
        {
            return await _context.Rooms.CountAsync();
        }
    }
}
