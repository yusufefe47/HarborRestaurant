using Microsoft.AspNetCore.Mvc;
using HarborRestaurant.Business.Abstract;
using Microsoft.AspNetCore.Identity;
using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.Areas.Admin.Controllers
{
    public class DashboardController : AdminControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IContactMessageService _contactMessageService;
        private readonly IMenuService _menuService;
        private readonly IBlogService _blogService;
        private readonly IRoomService _roomService;
        private readonly UserManager<AppUser> _userManager;

        public DashboardController(
            IReservationService reservationService,
            IContactMessageService contactMessageService,
            IMenuService menuService,
            IBlogService blogService,
            IRoomService roomService,
            UserManager<AppUser> userManager)
        {
            _reservationService = reservationService;
            _contactMessageService = contactMessageService;
            _menuService = menuService;
            _blogService = blogService;
            _roomService = roomService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            try
            {
                // Dashboard İstatistikleri
                var todayReservations = _reservationService.GetTodaysReservationCount();
                var pendingReservations = _reservationService.GetPendingReservationCount();
                var unreadMessages = _contactMessageService.GetUnreadMessageCount();
                
                // Son rezervasyonlar
                var recentReservations = _reservationService.GetUpcomingReservations();
                
                // Son mesajlar
                var recentMessages = _contactMessageService.GetUnreadMessages().Take(5);
                
                // Menü istatistikleri
                var totalMenuItems = _menuService.GetAllItems().Count();
                var totalCategories = _menuService.GetAllCategories().Count();
                
                // Blog istatistikleri
                var totalPosts = _blogService.GetAllPosts().Count();
                var featuredPosts = _blogService.GetFeaturedPosts().Count();
                
                // Kullanıcı istatistikleri
                var totalUsers = _userManager.Users.Count();
                
                // Room/Salon istatistikleri
                var totalRooms = _roomService.GetTotalRoomCount();

                // Son 7 günün rezervasyon istatistikleri
                var weekStats = _reservationService.GetReservationStats(
                    DateTime.Today.AddDays(-7), DateTime.Today);

                ViewBag.TodayReservations = todayReservations;
                ViewBag.PendingReservations = pendingReservations;
                ViewBag.UnreadMessages = unreadMessages;
                ViewBag.TotalMenuItems = totalMenuItems;
                ViewBag.TotalCategories = totalCategories;
                ViewBag.TotalPosts = totalPosts;
                ViewBag.FeaturedPosts = featuredPosts;
                ViewBag.TotalUsers = totalUsers;
                ViewBag.TotalRooms = totalRooms;
                ViewBag.WeekStats = weekStats;
                ViewBag.RecentReservations = recentReservations;
                ViewBag.RecentMessages = recentMessages;
            }
            catch (Exception)
            {
                // Hata durumunda boş verilerle devam et
                ViewBag.TodayReservations = 0;
                ViewBag.PendingReservations = 0;
                ViewBag.UnreadMessages = 0;
                ViewBag.TotalMenuItems = 0;
                ViewBag.TotalCategories = 0;
                ViewBag.TotalPosts = 0;
                ViewBag.FeaturedPosts = 0;
                ViewBag.TotalUsers = 0;
                ViewBag.TotalRooms = 0;
                ViewBag.WeekStats = new Dictionary<DateTime, int>();
                ViewBag.RecentReservations = new List<object>();
                ViewBag.RecentMessages = new List<object>();
                
                SetErrorMessage("Dashboard verileri yüklenirken hata oluştu.");
            }

            return View();
        }
    }
}
