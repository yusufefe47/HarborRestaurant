using HarborRestaurant.Business.Abstract;
using HarborRestaurant.DataAccess.Abstract;
using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.Business.Concrete
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<IEnumerable<Room>> GetAllRoomsAsync()
        {
            return await _roomRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Room>> GetActiveRoomsAsync()
        {
            return await _roomRepository.GetActiveRoomsAsync();
        }

        public async Task<Room?> GetRoomByIdAsync(int id)
        {
            return await _roomRepository.GetByIdAsync(id);
        }

        public async Task<Room> CreateRoomAsync(Room room)
        {
            room.CreatedDate = DateTime.Now;
            return await _roomRepository.CreateAsync(room);
        }

        public async Task<Room> UpdateRoomAsync(Room room)
        {
            room.UpdatedDate = DateTime.Now;
            return await _roomRepository.UpdateAsync(room);
        }

        public async Task DeleteRoomAsync(int id)
        {
            await _roomRepository.DeleteAsync(id);
        }

        public async Task<int> GetTotalRoomCountAsync()
        {
            return await _roomRepository.GetTotalRoomCountAsync();
        }

        // Sync methods for dashboard
        public IEnumerable<Room> GetAllRooms()
        {
            return GetAllRoomsAsync().Result;
        }

        public int GetTotalRoomCount()
        {
            return GetTotalRoomCountAsync().Result;
        }
    }
}
