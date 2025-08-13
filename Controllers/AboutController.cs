using Microsoft.AspNetCore.Mvc;
using HarborRestaurant.Business.Abstract;
using Microsoft.AspNetCore.Localization;

namespace HarborRestaurant.Controllers
{
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        // Cache aktif - güvenli ayarlarla  
        // [ResponseCache(Duration = 600, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new[] { "culture" })]
        public async Task<IActionResult> Index()
        {
            var about = await _aboutService.GetActiveAboutAsync();
            
            // Current culture için dil bilgisini al
            var currentCulture = Request.HttpContext.Features.Get<IRequestCultureFeature>()?.RequestCulture.Culture.Name ?? "tr";
            var isEnglish = currentCulture.StartsWith("en");
            ViewBag.IsEnglish = isEnglish;
            
            return View(about);
        }
    }
}
