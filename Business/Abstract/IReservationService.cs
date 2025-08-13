using HarborRestaurant.Entities.Concrete;
using HarborRestaurant.Entities.Enums;

namespace HarborRestaurant.Business.Abstract
{
    public interface IReservationService
    {
        // Async Methods
        Task<Reservation?> GetByIdAsync(int id);
        Task<IEnumerable<Reservation>> GetAllAsync();
        Task<IEnumerable<Reservation>> GetReservationsByDateAsync(DateTime date);
        Task<IEnumerable<Reservation>> GetReservationsByStatusAsync(ReservationStatus status);
        Task<IEnumerable<Reservation>> GetReservationsWithTableAsync();
        Task<IEnumerable<Reservation>> GetTodaysReservationsAsync();
        Task<IEnumerable<Reservation>> GetUpcomingReservationsAsync();
        Task<IEnumerable<Table>> GetAvailableTablesAsync(DateTime date, TimeOnly time, int guestCount);
        Task<bool> IsTableAvailableAsync(int? tableId, DateTime date, TimeOnly time);
        Task CreateAsync(Reservation reservation);
        Task AddAsync(Reservation reservation);
        Task UpdateAsync(Reservation reservation);
        Task DeleteAsync(int id);
        Task ConfirmReservationAsync(int reservationId);
        Task CancelReservationAsync(int reservationId);
        Task CompleteReservationAsync(int reservationId);
        Task<Dictionary<DateTime, int>> GetReservationStatsAsync(DateTime startDate, DateTime endDate);
        Task<int> GetTodaysReservationCountAsync();
        Task<int> GetPendingReservationCountAsync();
        Task<IEnumerable<Reservation>> GetReservationsByDateRangeAsync(DateTime startDate, DateTime endDate);

        // Table Management
        Task<Table?> GetTableByIdAsync(int id);
        Task<IEnumerable<Table>> GetAllTablesAsync();
        Task<IEnumerable<Table>> GetActiveTablesAsync();
        Task AddTableAsync(Table table);
        Task UpdateTableAsync(Table table);
        Task DeleteTableAsync(int id);

        // Sync Methods
        Reservation? GetById(int id);
        IEnumerable<Reservation> GetAll();
        IEnumerable<Reservation> GetReservationsByDate(DateTime date);
        IEnumerable<Reservation> GetReservationsByStatus(ReservationStatus status);
        IEnumerable<Reservation> GetReservationsWithTable();
        IEnumerable<Reservation> GetTodaysReservations();
        IEnumerable<Reservation> GetUpcomingReservations();
        IEnumerable<Table> GetAvailableTables(DateTime date, TimeOnly time, int guestCount);
        bool IsTableAvailable(int? tableId, DateTime date, TimeOnly time);
        void Create(Reservation reservation);
        void Add(Reservation reservation);
        void Update(Reservation reservation);
        void Delete(int id);
        void ConfirmReservation(int reservationId);
        void CancelReservation(int reservationId);
        void CompleteReservation(int reservationId);
        Dictionary<DateTime, int> GetReservationStats(DateTime startDate, DateTime endDate);
        int GetTodaysReservationCount();
        int GetPendingReservationCount();

        // Table Management Sync
        Table? GetTableById(int id);
        IEnumerable<Table> GetAllTables();
        IEnumerable<Table> GetActiveTables();
        void AddTable(Table table);
        void UpdateTable(Table table);
        void DeleteTable(int id);
    }
}
