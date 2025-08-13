using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.Business.Abstract
{
    public interface ITableService
    {
        Task<Table?> GetByIdAsync(int id);
        Task<IEnumerable<Table>> GetAllAsync();
        Task<IEnumerable<Table>> GetActiveTablesAsync();
        Task<IEnumerable<Table>> GetAvailableTablesAsync();
        Task<IEnumerable<Table>> GetAvailableTablesAsync(DateTime date, TimeOnly time, int guestCount);
        Task CreateAsync(Table table);
        Task UpdateAsync(Table table);
        Task DeleteAsync(int id);
        Task<bool> IsTableNumberExistsAsync(string tableNumber, int? excludeTableId = null);
    }
}
