using Microsoft.EntityFrameworkCore;
using HarborRestaurant.DataAccess.Abstract;
using HarborRestaurant.DataAccess.Context;
using HarborRestaurant.Entities.Concrete;
using HarborRestaurant.Entities.Enums;

namespace HarborRestaurant.DataAccess.Concrete
{
    public class TableRepository : GenericRepository<Table>, ITableRepository
    {
        public TableRepository(HarborDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Table>> GetActiveTablesAsync()
        {
            return await _dbSet
                .Where(t => t.IsActive)
                .OrderBy(t => t.TableNumber)
                .ToListAsync();
        }

        public async Task<IEnumerable<Table>> GetAvailableTablesAsync(DateTime date, TimeOnly time, int guestCount)
        {
            var reservationDateTime = date.Date;
            
            return await _dbSet
                .Where(t => t.IsActive && t.Capacity >= guestCount)
                .Where(t => !t.Reservations.Any(r => 
                    r.CheckInDate.Date == reservationDateTime &&
                    r.Status != ReservationStatus.Cancelled &&
                    Math.Abs((r.ReservationTime.Hour * 60 + r.ReservationTime.Minute) - 
                            (time.Hour * 60 + time.Minute)) < 120)) // 2 saatlik buffer
                .OrderBy(t => t.TableNumber)
                .ToListAsync();
        }

        public async Task<Table?> GetTableWithReservationsAsync(int tableId)
        {
            return await _dbSet
                .Include(t => t.Reservations)
                .FirstOrDefaultAsync(t => t.TableId == tableId);
        }
    }
}
