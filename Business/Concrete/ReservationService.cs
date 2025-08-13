using HarborRestaurant.Business.Abstract;
using HarborRestaurant.DataAccess.Abstract;
using HarborRestaurant.Entities.Concrete;
using HarborRestaurant.Entities.Enums;

namespace HarborRestaurant.Business.Concrete
{
    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReservationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Reservations

        public async Task<Reservation?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Reservations.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Reservation>> GetAllAsync()
        {
            return await _unitOfWork.Reservations.GetReservationsWithTableAsync();
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByDateAsync(DateTime date)
        {
            return await _unitOfWork.Reservations.GetReservationsByDateAsync(date);
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByStatusAsync(ReservationStatus status)
        {
            return await _unitOfWork.Reservations.GetReservationsByStatusAsync(status);
        }

        public async Task<IEnumerable<Reservation>> GetReservationsWithTableAsync()
        {
            return await _unitOfWork.Reservations.GetReservationsWithTableAsync();
        }

        public async Task<IEnumerable<Reservation>> GetTodaysReservationsAsync()
        {
            return await _unitOfWork.Reservations.GetTodaysReservationsAsync();
        }

        public async Task<IEnumerable<Reservation>> GetUpcomingReservationsAsync()
        {
            return await _unitOfWork.Reservations.GetUpcomingReservationsAsync();
        }

        public async Task<IEnumerable<Table>> GetAvailableTablesAsync(DateTime date, TimeOnly time, int guestCount)
        {
            return await _unitOfWork.Tables.GetAvailableTablesAsync(date, time, guestCount);
        }

        public async Task<bool> IsTableAvailableAsync(int? tableId, DateTime date, TimeOnly time)
        {
            return await _unitOfWork.Reservations.IsTableAvailableAsync(tableId, date, time);
        }

        public async Task AddAsync(Reservation reservation)
        {
            reservation.CreatedDate = DateTime.Now;
            reservation.Status = ReservationStatus.Pending;
            
            // Otomatik masa ataması (eğer masa seçilmemişse)
            if (!reservation.TableId.HasValue)
            {
                var availableTables = await GetAvailableTablesAsync(reservation.CheckInDate, reservation.ReservationTime, reservation.GuestCount);
                // Deterministik seçim: kapasite ve masa numarasına göre
                var suitableTable = availableTables
                    .OrderBy(t => t.Capacity)
                    .ThenBy(t => t.TableNumber)
                    .FirstOrDefault();
                if (suitableTable != null)
                {
                    reservation.TableId = suitableTable.TableId;
                }
            }

            await _unitOfWork.Reservations.AddAsync(reservation);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CreateAsync(Reservation reservation)
        {
            await AddAsync(reservation);
        }

        public async Task UpdateAsync(Reservation reservation)
        {
            reservation.UpdatedDate = DateTime.Now;
            _unitOfWork.Reservations.Update(reservation);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var reservation = await _unitOfWork.Reservations.GetByIdAsync(id);
            if (reservation != null)
            {
                _unitOfWork.Reservations.Remove(reservation);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task ConfirmReservationAsync(int reservationId)
        {
            var reservation = await _unitOfWork.Reservations.GetByIdAsync(reservationId);
            if (reservation != null)
            {
                reservation.Status = ReservationStatus.Confirmed;
                reservation.UpdatedDate = DateTime.Now;
                _unitOfWork.Reservations.Update(reservation);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task CancelReservationAsync(int reservationId)
        {
            var reservation = await _unitOfWork.Reservations.GetByIdAsync(reservationId);
            if (reservation != null)
            {
                reservation.Status = ReservationStatus.Cancelled;
                reservation.UpdatedDate = DateTime.Now;
                _unitOfWork.Reservations.Update(reservation);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task CompleteReservationAsync(int reservationId)
        {
            var reservation = await _unitOfWork.Reservations.GetByIdAsync(reservationId);
            if (reservation != null)
            {
                reservation.Status = ReservationStatus.Completed;
                reservation.UpdatedDate = DateTime.Now;
                _unitOfWork.Reservations.Update(reservation);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<Dictionary<DateTime, int>> GetReservationStatsAsync(DateTime startDate, DateTime endDate)
        {
            return await _unitOfWork.Reservations.GetReservationStatsAsync(startDate, endDate);
        }

        public async Task<int> GetTodaysReservationCountAsync()
        {
            var today = DateTime.Today;
            return await _unitOfWork.Reservations.CountAsync(r => r.CheckInDate.Date == today);
        }

        public async Task<int> GetPendingReservationCountAsync()
        {
            return await _unitOfWork.Reservations.CountAsync(r => r.Status == ReservationStatus.Pending);
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var allReservations = await _unitOfWork.Reservations.GetAllAsync();
            return allReservations.Where(r => r.CheckInDate.Date >= startDate.Date && r.CheckInDate.Date <= endDate.Date);
        }

        #endregion

        #region Tables

        public async Task<Table?> GetTableByIdAsync(int id)
        {
            return await _unitOfWork.Tables.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Table>> GetAllTablesAsync()
        {
            var tables = await _unitOfWork.Tables.GetAllAsync();
            return tables.OrderBy(t => t.TableNumber);
        }

        public async Task<IEnumerable<Table>> GetActiveTablesAsync()
        {
            return await _unitOfWork.Tables.GetActiveTablesAsync();
        }

        public async Task AddTableAsync(Table table)
        {
            table.CreatedDate = DateTime.Now;
            await _unitOfWork.Tables.AddAsync(table);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateTableAsync(Table table)
        {
            table.UpdatedDate = DateTime.Now;
            _unitOfWork.Tables.Update(table);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteTableAsync(int id)
        {
            var table = await _unitOfWork.Tables.GetByIdAsync(id);
            if (table != null)
            {
                _unitOfWork.Tables.Remove(table);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        #endregion

        #region Sync Methods for Dashboard

        public int GetTodaysReservationCount()
        {
            return GetTodaysReservationCountAsync().GetAwaiter().GetResult();
        }

        public int GetPendingReservationCount()
        {
            return GetPendingReservationCountAsync().GetAwaiter().GetResult();
        }

        public IEnumerable<Reservation> GetUpcomingReservations()
        {
            return GetUpcomingReservationsAsync().GetAwaiter().GetResult();
        }

        public Dictionary<DateTime, int> GetReservationStats(DateTime startDate, DateTime endDate)
        {
            return GetReservationStatsAsync(startDate, endDate).GetAwaiter().GetResult();
        }

        public Reservation? GetById(int id)
        {
            return GetByIdAsync(id).GetAwaiter().GetResult();
        }

        public IEnumerable<Reservation> GetAll()
        {
            return GetAllAsync().GetAwaiter().GetResult();
        }

        public IEnumerable<Reservation> GetReservationsByDate(DateTime date)
        {
            return GetReservationsByDateAsync(date).GetAwaiter().GetResult();
        }

        public IEnumerable<Reservation> GetReservationsByStatus(ReservationStatus status)
        {
            return GetReservationsByStatusAsync(status).GetAwaiter().GetResult();
        }

        public IEnumerable<Reservation> GetReservationsWithTable()
        {
            return GetReservationsWithTableAsync().GetAwaiter().GetResult();
        }

        public IEnumerable<Reservation> GetTodaysReservations()
        {
            return GetTodaysReservationsAsync().GetAwaiter().GetResult();
        }

        public IEnumerable<Table> GetAvailableTables(DateTime date, TimeOnly time, int guestCount)
        {
            return GetAvailableTablesAsync(date, time, guestCount).GetAwaiter().GetResult();
        }

        public bool IsTableAvailable(int? tableId, DateTime date, TimeOnly time)
        {
            return IsTableAvailableAsync(tableId, date, time).GetAwaiter().GetResult();
        }

        public void Create(Reservation reservation)
        {
            CreateAsync(reservation).GetAwaiter().GetResult();
        }

        public void Add(Reservation reservation)
        {
            AddAsync(reservation).GetAwaiter().GetResult();
        }

        public void Update(Reservation reservation)
        {
            UpdateAsync(reservation).GetAwaiter().GetResult();
        }

        public void Delete(int id)
        {
            DeleteAsync(id).GetAwaiter().GetResult();
        }

        public void ConfirmReservation(int reservationId)
        {
            ConfirmReservationAsync(reservationId).GetAwaiter().GetResult();
        }

        public void CancelReservation(int reservationId)
        {
            CancelReservationAsync(reservationId).GetAwaiter().GetResult();
        }

        public void CompleteReservation(int reservationId)
        {
            CompleteReservationAsync(reservationId).GetAwaiter().GetResult();
        }

        public Table? GetTableById(int id)
        {
            return GetTableByIdAsync(id).GetAwaiter().GetResult();
        }

        public IEnumerable<Table> GetAllTables()
        {
            return GetAllTablesAsync().GetAwaiter().GetResult();
        }

        public IEnumerable<Table> GetActiveTables()
        {
            return GetActiveTablesAsync().GetAwaiter().GetResult();
        }

        public void AddTable(Table table)
        {
            AddTableAsync(table).GetAwaiter().GetResult();
        }

        public void UpdateTable(Table table)
        {
            UpdateTableAsync(table).GetAwaiter().GetResult();
        }

        public void DeleteTable(int id)
        {
            DeleteTableAsync(id).GetAwaiter().GetResult();
        }

        #endregion
    }
}
