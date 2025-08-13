using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using HarborRestaurant.Models;
using HarborRestaurant.Business.Abstract;
using Microsoft.AspNetCore.Localization;
using HarborRestaurant.Business.Concrete;
using Microsoft.Extensions.Caching.Memory;

namespace HarborRestaurant.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHomePageService _homePageService;
    private readonly IMenuService _menuService;
    private readonly IBlogService _blogService;
    private readonly IRoomService _roomService;
    private readonly IMemoryCache _cache;
    private readonly CacheInvalidationService _cacheInvalidation;

    public HomeController(
        ILogger<HomeController> logger,
    IHomePageService homePageService,
    IMenuService menuService,
    IBlogService blogService,
    IRoomService roomService,
    IMemoryCache cache,
    CacheInvalidationService cacheInvalidation)
    {
        _logger = logger;
        _homePageService = homePageService;
        _menuService = menuService;
        _blogService = blogService;
        _roomService = roomService;
        _cache = cache;
        _cacheInvalidation = cacheInvalidation;
    }

    // Cache aktif - güvenli ayarlarla
    // [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new[] { "culture" })]
    public async Task<IActionResult> Index()
    {
        try
        {
            // Current culture için dil bilgisini al
            var currentCulture = Request.HttpContext.Features.Get<IRequestCultureFeature>()?.RequestCulture.Culture.Name ?? "tr";
            var isEnglish = currentCulture.StartsWith("en");

            // Smart caching with safe duration and culture-specific keys
            var cacheKey = $"home_index_{(isEnglish ? "en" : "tr")}";
            
            if (!_cache.TryGetValue(cacheKey, out var cachedData))
            {
                var homePage = await _homePageService.GetActiveHomePageAsync();
                var specialItems = await _menuService.GetSpecialItemsAsync();
                var recentPosts = await _blogService.GetRecentPostsAsync(3);
                var rooms = await _roomService.GetActiveRoomsAsync();

                cachedData = new
                {
                    HomePage = homePage,
                    SpecialItems = specialItems?.Take(6),
                    RecentPosts = recentPosts,
                    Rooms = rooms?.Take(4)
                };

                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5), // Güvenli 5 dakika cache
                    SlidingExpiration = TimeSpan.FromMinutes(2),
                    Priority = CacheItemPriority.Normal
                };

                _cache.Set(cacheKey, cachedData, cacheOptions);
            }

            var data = (dynamic)cachedData!;
            ViewBag.HomePage = data.HomePage;
            ViewBag.SpecialItems = data.SpecialItems;
            ViewBag.RecentPosts = data.RecentPosts;
            ViewBag.Rooms = data.Rooms;
            ViewBag.IsEnglish = isEnglish;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ana sayfa yüklenirken hata oluştu");
            // Hata durumunda boş verilerle devam et
        }

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
