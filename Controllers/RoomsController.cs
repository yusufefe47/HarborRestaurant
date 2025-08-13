using Microsoft.AspNetCore.Mvc;
using HarborRestaurant.Business.Abstract;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Localization;

namespace HarborRestaurant.Controllers
{
    public class RoomsController : Controller
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        // Cache aktif - güvenli ayarlarla - Geçici kapalı WARNING'ler için
        // [ResponseCache(Duration = 180, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new[] { "culture", "CheckInDate", "CheckOutDate", "MinPrice", "MaxPrice", "Stars", "SortBy" })]
        public async Task<IActionResult> Index(
            string? CheckInDate = null,
            string? CheckOutDate = null,
            string? ReservationTime = null,
            int GuestCount = 0,
            string? q = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            int? star = null,
            string? sort = null)
        {
            var list = await _roomService.GetActiveRoomsAsync();

            // Current culture için dil bilgisini al
            var currentCulture = Request.HttpContext.Features.Get<IRequestCultureFeature>()?.RequestCulture.Culture.Name ?? "tr";
            var isEnglish = currentCulture.StartsWith("en");

            // Arama
            if (!string.IsNullOrWhiteSpace(q))
            {
                var term = q.ToLowerInvariant();
                list = list.Where(r => (r.Name?.ToLower().Contains(term) ?? false)
                                    || (r.Description?.ToLower().Contains(term) ?? false)
                                    || (r.Features?.ToLower().Contains(term) ?? false)
                                    || (isEnglish && (r.NameEn?.ToLower().Contains(term) ?? false))
                                    || (isEnglish && (r.DescriptionEn?.ToLower().Contains(term) ?? false)));
            }

            // Kapasite filtresi
            if (GuestCount > 0)
            {
                list = list.Where(r => r.Capacity >= GuestCount);
            }

            // Fiyat filtreleri (MinimumOrderAmount fiyat gibi kullanılıyor)
            if (minPrice.HasValue)
                list = list.Where(r => (r.MinimumOrderAmount ?? 0) >= minPrice.Value);
            if (maxPrice.HasValue)
                list = list.Where(r => (r.MinimumOrderAmount ?? 0) <= maxPrice.Value);

            // Yıldız filtresi
            if (star.HasValue)
                list = list.Where(r => r.StarRating >= star.Value);

            // Sıralama
            list = sort switch
            {
                "price_asc" => list.OrderBy(r => r.MinimumOrderAmount ?? 0),
                "price_desc" => list.OrderByDescending(r => r.MinimumOrderAmount ?? 0),
                "star_desc" => list.OrderByDescending(r => r.StarRating),
                _ => list.OrderBy(r => r.SortOrder)
            };

            var vm = new HarborRestaurant.Models.ViewModels.RoomsSearchViewModel
            {
                CheckInDate = CheckInDate,
                CheckOutDate = CheckOutDate,
                ReservationTime = ReservationTime,
                GuestCount = GuestCount,
                Q = q,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                Star = star,
                Sort = sort,
                Rooms = list
            };

            ViewBag.IsEnglish = isEnglish;
            return View(vm);
        }

        // Salon detay
        public async Task<IActionResult> Details(int id)
        {
            var room = await _roomService.GetRoomByIdAsync(id);
            if (room == null || !room.IsActive)
                return NotFound();

            // Current culture için dil bilgisini al
            var currentCulture = Request.HttpContext.Features.Get<IRequestCultureFeature>()?.RequestCulture.Culture.Name ?? "tr";
            var isEnglish = currentCulture.StartsWith("en");
            ViewBag.IsEnglish = isEnglish;

            return View(room);
        }
    }
}
