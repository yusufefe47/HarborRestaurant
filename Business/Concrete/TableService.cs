using HarborRestaurant.Business.Abstract;
using HarborRestaurant.DataAccess.Abstract;
using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.Business.Concrete
{
    public class TableService : ITableService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TableService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Table?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Tables.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Table>> GetAllAsync()
        {
            return await _unitOfWork.Tables.GetAllAsync();
        }

        public async Task<IEnumerable<Table>> GetActiveTablesAsync()
        {
            var tables = await _unitOfWork.Tables.GetAllAsync();
            return tables.Where(t => t.IsActive);
        }

        public async Task<IEnumerable<Table>> GetAvailableTablesAsync()
        {
            return await GetActiveTablesAsync();
        }

        public async Task<IEnumerable<Table>> GetAvailableTablesAsync(DateTime date, TimeOnly time, int guestCount)
        {
            return await _unitOfWork.Tables.GetAvailableTablesAsync(date, time, guestCount);
        }

        public async Task CreateAsync(Table table)
        {
            table.CreatedDate = DateTime.Now;
            table.IsActive = true;
            await _unitOfWork.Tables.AddAsync(table);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(Table table)
        {
            table.UpdatedDate = DateTime.Now;
            _unitOfWork.Tables.Update(table);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var table = await _unitOfWork.Tables.GetByIdAsync(id);
            if (table != null)
            {
                _unitOfWork.Tables.Remove(table);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<bool> IsTableNumberExistsAsync(string tableNumber, int? excludeTableId = null)
        {
            var tables = await _unitOfWork.Tables.GetAllAsync();
            
            if (excludeTableId.HasValue)
            {
                return tables.Any(t => t.TableNumber.Equals(tableNumber, StringComparison.OrdinalIgnoreCase) 
                                     && t.TableId != excludeTableId.Value);
            }
            
            return tables.Any(t => t.TableNumber.Equals(tableNumber, StringComparison.OrdinalIgnoreCase));
        }
    }
}
