using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.DataAccess.Abstract
{
    public interface ITableRepository : IGenericRepository<Table>
    {
        Task<IEnumerable<Table>> GetActiveTablesAsync();
        Task<IEnumerable<Table>> GetAvailableTablesAsync(DateTime date, TimeOnly time, int guestCount);
        Task<Table?> GetTableWithReservationsAsync(int tableId);
    }
}
