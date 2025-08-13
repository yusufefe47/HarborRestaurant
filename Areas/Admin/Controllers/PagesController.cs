using Microsoft.AspNetCore.Mvc;
using HarborRestaurant.Business.Abstract;
using HarborRestaurant.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;

namespace HarborRestaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PagesController : AdminControllerBase
    {
        private readonly IHomePageService _homePageService;
        private readonly IAboutService _aboutService;
        private readonly IContactService _contactService;
        private readonly ITranslationService _translationService;

        public PagesController(
            IHomePageService homePageService,
            IAboutService aboutService,
            IContactService contactService,
            ITranslationService translationService)
        {
            _homePageService = homePageService;
            _aboutService = aboutService;
            _contactService = contactService;
            _translationService = translationService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // HOME PAGE MANAGEMENT
        [HttpGet]
        public async Task<IActionResult> Home()
        {
            var homePage = await _homePageService.GetActiveHomePageAsync();
            if (homePage == null)
            {
                homePage = new HomePage
                {
                    MainTitle = "Harborlights'a Hoş Geldiniz",
                    Subtitle = "En lezzetli deneyim sizleri bekliyor",
                    Description = "Harborlights olarak, taze deniz ürünleri ve eşsiz lezzetlerimizle sizleri ağırlamaya hazırız.",
                    ButtonText = "Rezervasyon Yap",
                    ButtonUrl = "/Reservation",
                    MainTitleEn = "Welcome to Harborlights",
                    SubtitleEn = "The finest experience awaits you",
                    DescriptionEn = "As Harborlights, we are ready to welcome you with fresh seafood and unique flavors.",
                    ButtonTextEn = "Make Reservation",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                };
            }
            return View(homePage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Home(HomePage model, IFormFile? heroImage)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Resim yükleme işlemi
                    if (heroImage != null && heroImage.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "uploads");
                        
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + heroImage.FileName;
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await heroImage.CopyToAsync(fileStream);
                        }

                        model.HeroImageUrl = "/images/uploads/" + uniqueFileName;
                    }

                    var existingPage = await _homePageService.GetActiveHomePageAsync();
                    
                    if (existingPage != null)
                    {
                        // Mevcut sayfayı güncelle
                        existingPage.MainTitle = model.MainTitle;
                        existingPage.Subtitle = model.Subtitle;
                        existingPage.Description = model.Description;
                        existingPage.ButtonText = model.ButtonText;
                        existingPage.ButtonUrl = model.ButtonUrl;
                        // English fields
                        existingPage.MainTitleEn = model.MainTitleEn;
                        existingPage.SubtitleEn = model.SubtitleEn;
                        existingPage.DescriptionEn = model.DescriptionEn;
                        existingPage.ButtonTextEn = model.ButtonTextEn;
                        existingPage.UpdatedDate = DateTime.Now;
                        
                        if (!string.IsNullOrEmpty(model.HeroImageUrl))
                        {
                            existingPage.HeroImageUrl = model.HeroImageUrl;
                        }

                        await _homePageService.UpdateAsync(existingPage);
                    }
                    else
                    {
                        // Yeni sayfa oluştur
                        model.CreatedDate = DateTime.Now;
                        model.IsActive = true;
                        await _homePageService.CreateAsync(model);
                    }

                    SetSuccessMessage("Anasayfa başarıyla güncellendi.");
                    return RedirectToAction("Home");
                }
                catch (Exception ex)
                {
                    SetErrorMessage("Anasayfa güncellenirken bir hata oluştu: " + ex.Message);
                }
            }

            return View(model);
        }

        // ABOUT PAGE MANAGEMENT
        [HttpGet]
        public async Task<IActionResult> About()
        {
            var about = await _aboutService.GetActiveAboutAsync();
            if (about == null)
            {
                about = new About
                {
                    Title = "Hakkımızda",
                    Subtitle = "Harborlights",
                    Description = "Harborlights olarak 20 yıldır İzmir'in en güzel köşesinde hizmet vermekteyiz.",
                    TitleEn = "About Us",
                    SubtitleEn = "Harborlights",
                    DescriptionEn = "As Harborlights, we have been serving in the most beautiful corner of Izmir for 20 years.",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                };
            }
            return View(about);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> About(About model, IFormFile? aboutImage)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Resim yükleme işlemi
                    if (aboutImage != null && aboutImage.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "uploads");
                        
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + aboutImage.FileName;
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await aboutImage.CopyToAsync(fileStream);
                        }

                        model.ImageUrl = "/images/uploads/" + uniqueFileName;
                    }

                    var existingAbout = await _aboutService.GetActiveAboutAsync();
                    
                    if (existingAbout != null)
                    {
                        // Mevcut hakkımızda sayfasını güncelle
                        existingAbout.Title = model.Title;
                        existingAbout.Subtitle = model.Subtitle;
                        existingAbout.Description = model.Description;
                        // English fields
                        existingAbout.TitleEn = model.TitleEn;
                        existingAbout.SubtitleEn = model.SubtitleEn;
                        existingAbout.DescriptionEn = model.DescriptionEn;
                        existingAbout.UpdatedDate = DateTime.Now;
                        
                        if (!string.IsNullOrEmpty(model.ImageUrl))
                        {
                            existingAbout.ImageUrl = model.ImageUrl;
                        }

                        await _aboutService.UpdateAsync(existingAbout);
                    }
                    else
                    {
                        // Yeni hakkımızda sayfası oluştur
                        model.CreatedDate = DateTime.Now;
                        model.IsActive = true;
                        await _aboutService.CreateAsync(model);
                    }

                    SetSuccessMessage("Hakkımızda sayfası başarıyla güncellendi.");
                    return RedirectToAction("About");
                }
                catch (Exception ex)
                {
                    SetErrorMessage("Hakkımızda sayfası güncellenirken bir hata oluştu: " + ex.Message);
                }
            }

            return View(model);
        }

        // CONTACT PAGE MANAGEMENT
        [HttpGet]
        public async Task<IActionResult> Contact()
        {
            var contact = await _contactService.GetActiveContactAsync();
            if (contact == null)
            {
                contact = new Contact
                {
                    Address = "Atatürk Bulvarı No:123, Alsancak/İzmir",
                    Phone = "+90 232 123 45 67",
                    Email = "info@harborrestaurant.com",
                    WorkingHours = "Pazartesi - Pazar: 11:00 - 23:00",
                    MapLatitude = "38.4237",
                    MapLongitude = "27.1428",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                };
            }
            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(Contact model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingContact = await _contactService.GetActiveContactAsync();
                    
                    if (existingContact != null)
                    {
                        // Mevcut iletişim bilgilerini güncelle
                        existingContact.Address = model.Address;
                        existingContact.Phone = model.Phone;
                        existingContact.Email = model.Email;
                        existingContact.WorkingHours = model.WorkingHours;
                        existingContact.MapLatitude = model.MapLatitude;
                        existingContact.MapLongitude = model.MapLongitude;
                        existingContact.UpdatedDate = DateTime.Now;

                        await _contactService.UpdateAsync(existingContact);
                    }
                    else
                    {
                        // Yeni iletişim bilgisi oluştur
                        model.CreatedDate = DateTime.Now;
                        model.IsActive = true;
                        await _contactService.CreateAsync(model);
                    }

                    SetSuccessMessage("İletişim bilgileri başarıyla güncellendi.");
                    return RedirectToAction("Contact");
                }
                catch (Exception ex)
                {
                    SetErrorMessage("İletişim bilgileri güncellenirken bir hata oluştu: " + ex.Message);
                }
            }

            return View(model);
        }

        // TRANSLATION ENDPOINTS
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TranslateHomePage()
        {
            try
            {
                var homePage = await _homePageService.GetActiveHomePageAsync();
                if (homePage != null)
                {
                    await _translationService.TranslateHomePageAsync(homePage, true);
                    await _homePageService.UpdateAsync(homePage);
                    
                    SetSuccessMessage("Anasayfa İngilizce çevirisi başarıyla oluşturuldu.");
                }
                else
                {
                    SetErrorMessage("Çevrilecek anasayfa bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                SetErrorMessage($"Çeviri sırasında bir hata oluştu: {ex.Message}");
            }
            
            return RedirectToAction("Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TranslateAbout()
        {
            try
            {
                var about = await _aboutService.GetActiveAboutAsync();
                if (about != null)
                {
                    await _translationService.TranslateAboutAsync(about, true);
                    await _aboutService.UpdateAsync(about);
                    
                    SetSuccessMessage("Hakkımızda sayfası İngilizce çevirisi başarıyla oluşturuldu.");
                }
                else
                {
                    SetErrorMessage("Çevrilecek hakkımızda sayfası bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                SetErrorMessage($"Çeviri sırasında bir hata oluştu: {ex.Message}");
            }
            
            return RedirectToAction("About");
        }

        [HttpPost]
        public async Task<IActionResult> TranslateText([FromBody] TranslateRequest request)
        {
            try
            {
                var translatedText = await _translationService.TranslateAsync(request.Text, request.FromLanguage, request.ToLanguage);
                return Json(new { success = true, translatedText });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
    }

    public class TranslateRequest
    {
        public string Text { get; set; } = string.Empty;
        public string FromLanguage { get; set; } = "tr";
        public string ToLanguage { get; set; } = "en";
    }
}
