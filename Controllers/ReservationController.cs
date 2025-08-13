using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HarborRestaurant.Business.Abstract;
using HarborRestaurant.Entities.Concrete;
using HarborRestaurant.Models.ViewModels;
using System.Globalization;

namespace HarborRestaurant.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IContactMessageService _contactMessageService;
        private readonly IRoomService _roomService;

        public ReservationController(
            IReservationService reservationService,
            IContactMessageService contactMessageService,
            IRoomService roomService)
        {
            _reservationService = reservationService;
            _contactMessageService = contactMessageService;
            _roomService = roomService;
        }

        public async Task<IActionResult> Index(string CheckInDate = "", string CheckOutDate = "", 
            string ReservationTime = "", int GuestCount = 0, int? RoomId = null)
        {
            // Masa tercihi kaldırıldı: tablo listesi yüklenmiyor

            // Ana sayfadan gelen değerleri modele aktar
            var model = new ReservationViewModel();
            if (!string.IsNullOrEmpty(CheckInDate))
            {
                model.CheckInDate = DateTime.Parse(CheckInDate).ToString("dd/MM/yyyy");
            }
            if (!string.IsNullOrEmpty(CheckOutDate))
            {
                model.CheckOutDate = DateTime.Parse(CheckOutDate).ToString("dd/MM/yyyy");
            }
            if (!string.IsNullOrEmpty(ReservationTime))
            {
                model.ReservationTime = ReservationTime;
            }
            if (GuestCount > 0)
            {
                model.GuestCount = GuestCount;
            }
            if (RoomId.HasValue)
            {
                model.RoomId = RoomId;
                // Salon adını göstermek için
                var room = await _roomService.GetRoomByIdAsync(RoomId.Value);
                if (room != null)
                {
                    model.RoomName = room.Name;
                    ViewData["SelectedRoomName"] = room.Name;
                    ViewData["SelectedRoomId"] = room.RoomId;
                    ViewBag.SelectedRoom = room;
                }
            }
            
            return View(model);
        }

    [HttpPost]
    public async Task<IActionResult> Create(ReservationViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Tarih formatını kontrol et
                    DateTime checkInDate, checkOutDate;
                    
                    // HTML date input'tan gelen format: yyyy-MM-dd
                    checkInDate = DateTime.Parse(model.CheckInDate);
                    checkOutDate = DateTime.Parse(model.CheckOutDate);

                    // ViewModel'den Entity'e dönüştür
                    var reservation = new Reservation
                    {
                        FullName = model.FullName,
                        Email = model.Email,
                        Phone = model.Phone,
                        GuestCount = model.GuestCount,
                        CheckInDate = checkInDate,
                        CheckOutDate = checkOutDate,
                        ReservationTime = TimeOnly.Parse(model.ReservationTime),
                        SpecialRequests = model.SpecialRequest,
                        // Masa tercihi kaldırıldı
                        RoomId = model.RoomId,
                        Status = Entities.Enums.ReservationStatus.Pending,
                        IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                        CreatedDate = DateTime.Now
                    };

                    
                    await _reservationService.AddAsync(reservation);
                    
                    // Ana sayfadan geliyorsa ana sayfaya başarı mesajı ile yönlendir
                    if (HttpContext.Request.Headers["Referer"].ToString().Contains("Home"))
                    {
                        TempData["SuccessMessage"] = "Rezervasyon talebiniz başarıyla gönderildi. En kısa sürede size geri dönüş yapılacaktır.";
                        return RedirectToAction("Index", "Home");
                    }
                    
                    ViewData["SuccessMessage"] = "Rezervasyon talebiniz başarıyla gönderildi. En kısa sürede size geri dönüş yapılacaktır.";
                    return View("Index", new ReservationViewModel());
                }
                else
                {
                    // Model validation hatası
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    ViewData["ErrorMessage"] = "Lütfen tüm gerekli alanları doğru şekilde doldurun: " + string.Join(", ", errors);
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = $"Rezervasyon oluşturulurken bir hata oluştu: {ex.Message}";
            }

            // Ana sayfadan geliyorsa ana sayfaya yönlendir
            if (HttpContext.Request.Headers["Referer"].ToString().Contains("Home"))
            {
                TempData["ErrorMessage"] = ViewData["ErrorMessage"]?.ToString();
                return RedirectToAction("Index", "Home");
            }

            return View("Index", model);
        }

        // Ana sayfadan gelen POST request'leri için özel action
        [HttpPost]
        public async Task<IActionResult> CreateFromHome(string FullName, string Email, string Phone, int GuestCount, 
            string CheckInDate, string CheckOutDate, string ReservationTime)
        {
            try
            {
                var reservation = new Reservation
                {
                    FullName = FullName,
                    Email = Email,
                    Phone = Phone,
                    GuestCount = GuestCount,
                    CheckInDate = DateTime.Parse(CheckInDate),
                    CheckOutDate = DateTime.Parse(CheckOutDate),
                    ReservationTime = TimeOnly.Parse(ReservationTime),
                    Status = Entities.Enums.ReservationStatus.Pending,
                    IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    CreatedDate = DateTime.Now
                };

                await _reservationService.AddAsync(reservation);
                TempData["SuccessMessage"] = "Rezervasyon talebiniz başarıyla gönderildi. En kısa sürede size geri dönüş yapılacaktır.";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Rezervasyon oluşturulurken bir hata oluştu. Lütfen tekrar deneyiniz.";
            }

            return RedirectToAction("Index", "Home");
        }        public IActionResult Success()
        {
            return View();
        }

    // Masa tercihi kaldırıldığı için tablo uygunluk uç noktası ve yardımcı metotlar temizlendi
    }
}
