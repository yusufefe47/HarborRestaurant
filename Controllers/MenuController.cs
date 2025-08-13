using Microsoft.AspNetCore.Mvc;
using HarborRestaurant.Business.Abstract;
using Microsoft.AspNetCore.Localization;

namespace HarborRestaurant.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        // Cache aktif - güvenli ayarlarla
        // [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new[] { "culture" })]
        public async Task<IActionResult> Index()
        {
            var categories = await _menuService.GetCategoriesWithMenuItemsAsync();
            
            // Current culture için dil bilgisini al
            var currentCulture = Request.HttpContext.Features.Get<IRequestCultureFeature>()?.RequestCulture.Culture.Name ?? "tr";
            var isEnglish = currentCulture.StartsWith("en");
            ViewBag.IsEnglish = isEnglish;
            
            return View(categories);
        }

        public async Task<IActionResult> Category(int id)
        {
            var category = await _menuService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            var items = await _menuService.GetItemsByCategoryAsync(id);
            ViewBag.Category = category;
            
            // Current culture için dil bilgisini al
            var currentCulture = Request.HttpContext.Features.Get<IRequestCultureFeature>()?.RequestCulture.Culture.Name ?? "tr";
            var isEnglish = currentCulture.StartsWith("en");
            ViewBag.IsEnglish = isEnglish;
            
            return View(items);
        }
    }
}
