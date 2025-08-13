using Microsoft.EntityFrameworkCore;
using HarborRestaurant.DataAccess.Abstract;
using HarborRestaurant.DataAccess.Context;
using HarborRestaurant.Entities.Concrete;
using HarborRestaurant.Entities.Enums;

namespace HarborRestaurant.DataAccess.Concrete
{
    public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(HarborDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByDateAsync(DateTime date)
        {
            return await _dbSet
                .Include(r => r.Table)
                .Where(r => r.CheckInDate.Date == date.Date)
                .OrderBy(r => r.ReservationTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByStatusAsync(ReservationStatus status)
        {
            return await _dbSet
                .Include(r => r.Table)
                .Where(r => r.Status == status)
                .OrderByDescending(r => r.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetReservationsWithTableAsync()
        {
            return await _dbSet
                .Include(r => r.Table)
                .OrderByDescending(r => r.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetTodaysReservationsAsync()
        {
            var today = DateTime.Today;
            return await _dbSet
                .Include(r => r.Table)
                .Where(r => r.CheckInDate.Date == today)
                .OrderBy(r => r.ReservationTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetUpcomingReservationsAsync()
        {
            var now = DateTime.Now;
            return await _dbSet
                .Include(r => r.Table)
                .Where(r => r.CheckInDate >= now.Date && r.Status == ReservationStatus.Confirmed)
                .OrderBy(r => r.CheckInDate)
                .ThenBy(r => r.ReservationTime)
                .Take(10)
                .ToListAsync();
        }

        public async Task<bool> IsTableAvailableAsync(int? tableId, DateTime date, TimeOnly time)
        {
            if (!tableId.HasValue) return true;

            var reservationDateTime = date.Date;
            
            return !await _dbSet.AnyAsync(r =>
                r.TableId == tableId &&
                r.CheckInDate.Date == reservationDateTime &&
                r.Status != ReservationStatus.Cancelled &&
                Math.Abs((r.ReservationTime.Hour * 60 + r.ReservationTime.Minute) - 
                        (time.Hour * 60 + time.Minute)) < 120); // 2 saatlik buffer
        }

        public async Task<Dictionary<DateTime, int>> GetReservationStatsAsync(DateTime startDate, DateTime endDate)
        {
            var stats = await _dbSet
                .Where(r => r.CheckInDate >= startDate.Date && r.CheckInDate <= endDate.Date)
                .GroupBy(r => r.CheckInDate.Date)
                .Select(g => new { Date = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Date, x => x.Count);

            return stats;
        }
    }
}
