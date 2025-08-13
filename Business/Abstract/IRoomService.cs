using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.Business.Abstract
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAllRoomsAsync();
        Task<IEnumerable<Room>> GetActiveRoomsAsync();
        Task<Room?> GetRoomByIdAsync(int id);
        Task<Room> CreateRoomAsync(Room room);
        Task<Room> UpdateRoomAsync(Room room);
        Task DeleteRoomAsync(int id);
        Task<int> GetTotalRoomCountAsync();
        
        // Sync methods for dashboard
        IEnumerable<Room> GetAllRooms();
        int GetTotalRoomCount();
    }
}
