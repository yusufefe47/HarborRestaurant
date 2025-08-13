using HarborRestaurant.Entities.Concrete;
using HarborRestaurant.Entities.Enums;

namespace HarborRestaurant.DataAccess.Abstract
{
    public interface IReservationRepository : IGenericRepository<Reservation>
    {
        Task<IEnumerable<Reservation>> GetReservationsByDateAsync(DateTime date);
        Task<IEnumerable<Reservation>> GetReservationsByStatusAsync(ReservationStatus status);
        Task<IEnumerable<Reservation>> GetReservationsWithTableAsync();
        Task<IEnumerable<Reservation>> GetTodaysReservationsAsync();
        Task<IEnumerable<Reservation>> GetUpcomingReservationsAsync();
        Task<bool> IsTableAvailableAsync(int? tableId, DateTime date, TimeOnly time);
        Task<Dictionary<DateTime, int>> GetReservationStatsAsync(DateTime startDate, DateTime endDate);
    }
}
