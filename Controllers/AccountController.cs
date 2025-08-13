using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using HarborRestaurant.Entities.Concrete;
using HarborRestaurant.Models.ViewModels;

namespace HarborRestaurant.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult TestAdmin()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    // Giriş tarihini güncelle
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (user != null)
                    {
                        user.LastLoginDate = DateTime.Now;
                        await _userManager.UpdateAsync(user);
                    }

                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Geçersiz e-posta adresi veya şifre.");
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    IsActive = true,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Admin rolünün var olup olmadığını kontrol et, yoksa oluştur
                    if (!await _roleManager.RoleExistsAsync("Admin"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    }

                    // Kullanıcıyı Admin rolüne ekle
                    await _userManager.AddToRoleAsync(user, "Admin");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    TempData["SuccessMessage"] = "Hesabınız başarıyla oluşturuldu ve Admin yetkisi ile giriş yapıldı.";
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        private IActionResult RedirectToLocal(string? returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // İlk admin kullanıcısı oluşturma (geliştirme amaçlı)
        [HttpGet]
        public async Task<IActionResult> CreateAdmin()
        {
            var adminUser = await _userManager.FindByEmailAsync("admin@harborrestaurant.com");
            if (adminUser == null)
            {
                adminUser = new AppUser
                {
                    UserName = "admin@harborrestaurant.com",
                    Email = "admin@harborrestaurant.com",
                    FirstName = "Admin",
                    LastName = "User",
                    EmailConfirmed = true,
                    IsActive = true
                };

                var result = await _userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    // Admin rolünün var olup olmadığını kontrol et, yoksa oluştur
                    if (!await _roleManager.RoleExistsAsync("Admin"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    }

                    // Kullanıcıyı Admin rolüne ekle
                    await _userManager.AddToRoleAsync(adminUser, "Admin");

                    TempData["SuccessMessage"] = "Admin kullanıcısı oluşturuldu. E-posta: admin@harborrestaurant.com, Şifre: Admin123!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Admin kullanıcısı oluşturulamadı: " + string.Join(", ", result.Errors.Select(e => e.Description));
                }
            }
            else
            {
                TempData["InfoMessage"] = "Admin kullanıcısı zaten mevcut. E-posta: admin@harborrestaurant.com";
            }

            return RedirectToAction("Login");
        }
    }
}
