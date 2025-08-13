using Microsoft.AspNetCore.Mvc;
using HarborRestaurant.Business.Abstract;
using HarborRestaurant.Entities.Concrete;
using HarborRestaurant.Entities.Enums;
using Microsoft.AspNetCore.Authorization;

namespace HarborRestaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ReservationsController : AdminControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly ITableService _tableService;

        public ReservationsController(
            IReservationService reservationService,
            ITableService tableService)
        {
            _reservationService = reservationService;
            _tableService = tableService;
        }

        // LIST ALL RESERVATIONS
        [HttpGet]
        public async Task<IActionResult> Index(string? status, DateTime? date, int page = 1)
        {
            try
            {
                var reservations = await _reservationService.GetAllAsync();

                // Filter by status
                if (!string.IsNullOrEmpty(status))
                {
                    if (Enum.TryParse<ReservationStatus>(status, true, out var statusEnum))
                    {
                        reservations = reservations.Where(r => r.Status == statusEnum);
                    }
                }

                // Filter by date
                if (date.HasValue)
                {
                    reservations = reservations.Where(r => r.CheckInDate.Date == date.Value.Date);
                }

                // Order by date and time
                reservations = reservations.OrderByDescending(r => r.CheckInDate).ThenByDescending(r => r.ReservationTime);

                // Pagination
                const int pageSize = 20;
                var paginatedReservations = reservations.Skip((page - 1) * pageSize).Take(pageSize);

                ViewBag.CurrentStatus = status;
                ViewBag.CurrentDate = date;
                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = (int)Math.Ceiling((double)reservations.Count() / pageSize);
                ViewBag.TotalCount = reservations.Count();

                return View(paginatedReservations);
            }
            catch (Exception ex)
            {
                SetErrorMessage("Rezervasyonlar yüklenirken bir hata oluştu: " + ex.Message);
                return View(new List<Reservation>());
            }
        }

        // RESERVATION DETAILS
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var reservation = await _reservationService.GetByIdAsync(id);
                if (reservation == null)
                {
                    SetErrorMessage("Rezervasyon bulunamadı.");
                    return RedirectToAction("Index");
                }

                return View(reservation);
            }
            catch (Exception ex)
            {
                SetErrorMessage("Rezervasyon detayları yüklenirken bir hata oluştu: " + ex.Message);
                return RedirectToAction("Index");
            }
        }

        // CREATE NEW RESERVATION
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Tables = await _tableService.GetAvailableTablesAsync();
            ViewBag.ReservationStatuses = Enum.GetValues<ReservationStatus>();
            return View(new Reservation());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    reservation.CreatedDate = DateTime.Now;
                    reservation.Status = ReservationStatus.Pending;
                    
                    await _reservationService.CreateAsync(reservation);
                    SetSuccessMessage("Rezervasyon başarıyla oluşturuldu.");
                    return RedirectToAction("Details", new { id = reservation.ReservationId });
                }
                catch (Exception ex)
                {
                    SetErrorMessage("Rezervasyon oluşturulurken bir hata oluştu: " + ex.Message);
                }
            }

            ViewBag.Tables = await _tableService.GetAvailableTablesAsync();
            ViewBag.ReservationStatuses = Enum.GetValues<ReservationStatus>();
            return View(reservation);
        }

        // EDIT RESERVATION
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var reservation = await _reservationService.GetByIdAsync(id);
                if (reservation == null)
                {
                    SetErrorMessage("Rezervasyon bulunamadı.");
                    return RedirectToAction("Index");
                }

                ViewBag.Tables = await _tableService.GetAvailableTablesAsync();
                ViewBag.ReservationStatuses = Enum.GetValues<ReservationStatus>();
                return View(reservation);
            }
            catch (Exception ex)
            {
                SetErrorMessage("Rezervasyon düzenleme sayfası yüklenirken bir hata oluştu: " + ex.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Reservation reservation)
        {
            if (id != reservation.ReservationId)
            {
                SetErrorMessage("Rezervasyon ID eşleşmiyor.");
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingReservation = await _reservationService.GetByIdAsync(id);
                    if (existingReservation == null)
                    {
                        SetErrorMessage("Rezervasyon bulunamadı.");
                        return RedirectToAction("Index");
                    }

                    // Update fields
                    existingReservation.FullName = reservation.FullName;
                    existingReservation.Email = reservation.Email;
                    existingReservation.Phone = reservation.Phone;
                    existingReservation.GuestCount = reservation.GuestCount;
                    existingReservation.CheckInDate = reservation.CheckInDate;
                    existingReservation.CheckOutDate = reservation.CheckOutDate;
                    existingReservation.ReservationTime = reservation.ReservationTime;
                    existingReservation.SpecialRequests = reservation.SpecialRequests;
                    existingReservation.Status = reservation.Status;
                    existingReservation.TableId = reservation.TableId;
                    existingReservation.AdminNotes = reservation.AdminNotes;
                    existingReservation.UpdatedDate = DateTime.Now;

                    await _reservationService.UpdateAsync(existingReservation);
                    SetSuccessMessage("Rezervasyon başarıyla güncellendi.");
                    return RedirectToAction("Details", new { id = id });
                }
                catch (Exception ex)
                {
                    SetErrorMessage("Rezervasyon güncellenirken bir hata oluştu: " + ex.Message);
                }
            }

            ViewBag.Tables = await _tableService.GetAvailableTablesAsync();
            ViewBag.ReservationStatuses = Enum.GetValues<ReservationStatus>();
            return View(reservation);
        }

        // UPDATE RESERVATION STATUS
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, ReservationStatus status, string? adminNotes)
        {
            try
            {
                var reservation = await _reservationService.GetByIdAsync(id);
                if (reservation == null)
                {
                    SetErrorMessage("Rezervasyon bulunamadı.");
                    return RedirectToAction("Index");
                }

                reservation.Status = status;
                reservation.AdminNotes = adminNotes;
                reservation.UpdatedDate = DateTime.Now;

                await _reservationService.UpdateAsync(reservation);
                SetSuccessMessage($"Rezervasyon durumu '{GetStatusText(status)}' olarak güncellendi.");
                
                return RedirectToAction("Details", new { id = id });
            }
            catch (Exception ex)
            {
                SetErrorMessage("Rezervasyon durumu güncellenirken bir hata oluştu: " + ex.Message);
                return RedirectToAction("Index");
            }
        }

        // QUICK ACTIONS (GET) - to avoid 404 when accessed via direct links
        [HttpGet]
        public async Task<IActionResult> Confirm(int id)
        {
            try
            {
                var reservation = await _reservationService.GetByIdAsync(id);
                if (reservation == null)
                {
                    SetErrorMessage("Rezervasyon bulunamadı.");
                    return RedirectToAction("Index");
                }

                reservation.Status = ReservationStatus.Confirmed;
                reservation.UpdatedDate = DateTime.Now;
                await _reservationService.UpdateAsync(reservation);

                SetSuccessMessage("Rezervasyon onaylandı.");
                return RedirectToAction("Details", new { id });
            }
            catch (Exception ex)
            {
                SetErrorMessage("Rezervasyon onaylanırken bir hata oluştu: " + ex.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Cancel(int id)
        {
            try
            {
                var reservation = await _reservationService.GetByIdAsync(id);
                if (reservation == null)
                {
                    SetErrorMessage("Rezervasyon bulunamadı.");
                    return RedirectToAction("Index");
                }

                reservation.Status = ReservationStatus.Cancelled;
                reservation.UpdatedDate = DateTime.Now;
                await _reservationService.UpdateAsync(reservation);

                SetSuccessMessage("Rezervasyon iptal edildi.");
                return RedirectToAction("Details", new { id });
            }
            catch (Exception ex)
            {
                SetErrorMessage("Rezervasyon iptal edilirken bir hata oluştu: " + ex.Message);
                return RedirectToAction("Index");
            }
        }

        // DELETE RESERVATION (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var reservation = await _reservationService.GetByIdAsync(id);
                if (reservation == null)
                {
                    SetErrorMessage("Rezervasyon bulunamadı.");
                    return RedirectToAction("Index");
                }

                await _reservationService.DeleteAsync(id);
                SetSuccessMessage("Rezervasyon başarıyla silindi.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                SetErrorMessage("Rezervasyon silinirken bir hata oluştu: " + ex.Message);
                return RedirectToAction("Index");
            }
        }

        // DELETE RESERVATION (GET convenience to avoid 404 on bookmarked links)
        [HttpGet]
        public async Task<IActionResult> Delete(int id, bool confirm = false)
        {
            try
            {
                var reservation = await _reservationService.GetByIdAsync(id);
                if (reservation == null)
                {
                    SetErrorMessage("Rezervasyon bulunamadı.");
                    return RedirectToAction("Index");
                }

                await _reservationService.DeleteAsync(id);
                SetSuccessMessage("Rezervasyon başarıyla silindi.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                SetErrorMessage("Rezervasyon silinirken bir hata oluştu: " + ex.Message);
                return RedirectToAction("Index");
            }
        }

        // CALENDAR VIEW
        [HttpGet]
        public async Task<IActionResult> Calendar()
        {
            try
            {
                var reservations = await _reservationService.GetAllAsync();
                return View(reservations);
            }
            catch (Exception ex)
            {
                SetErrorMessage("Takvim görünümü yüklenirken bir hata oluştu: " + ex.Message);
                return View(new List<Reservation>());
            }
        }

        // EXPORT RESERVATIONS
        [HttpGet]
        public async Task<IActionResult> Export(string? status, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var reservations = await _reservationService.GetAllAsync();

                // Apply filters
                if (!string.IsNullOrEmpty(status) && Enum.TryParse<ReservationStatus>(status, true, out var statusEnum))
                {
                    reservations = reservations.Where(r => r.Status == statusEnum);
                }

                if (startDate.HasValue)
                {
                    reservations = reservations.Where(r => r.CheckInDate >= startDate.Value);
                }

                if (endDate.HasValue)
                {
                    reservations = reservations.Where(r => r.CheckInDate <= endDate.Value);
                }

                // Generate CSV content
                var csv = "Tarih,Saat,Ad Soyad,Email,Telefon,Kişi Sayısı,Durum,Özel İstekler\n";
                foreach (var reservation in reservations.OrderBy(r => r.CheckInDate))
                {
                    csv += $"{reservation.CheckInDate:dd.MM.yyyy},{reservation.ReservationTime},{reservation.FullName},{reservation.Email},{reservation.Phone},{reservation.GuestCount},{GetStatusText(reservation.Status)},{reservation.SpecialRequests?.Replace(",", ";")}\n";
                }

                var bytes = System.Text.Encoding.UTF8.GetBytes(csv);
                var fileName = $"rezervasyonlar_{DateTime.Now:yyyyMMdd}.csv";
                
                return File(bytes, "text/csv", fileName);
            }
            catch (Exception ex)
            {
                SetErrorMessage("Rezervasyonlar dışa aktarılırken bir hata oluştu: " + ex.Message);
                return RedirectToAction("Index");
            }
        }

        private static string GetStatusText(ReservationStatus status)
        {
            return status switch
            {
                ReservationStatus.Pending => "Bekliyor",
                ReservationStatus.Confirmed => "Onaylandı",
                ReservationStatus.Completed => "Tamamlandı",
                ReservationStatus.Cancelled => "İptal Edildi",
                _ => "Bilinmiyor"
            };
        }
    }
}
