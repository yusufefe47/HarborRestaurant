using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.Controllers
{
    public class TestController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public TestController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> UserInfo()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Content("Kullanıcı giriş yapmamış");
            }

            var roles = await _userManager.GetRolesAsync(currentUser);
            var allRoles = _roleManager.Roles.Select(r => r.Name).ToList();

            var info = $@"
                <h3>Kullanıcı Bilgileri:</h3>
                <p><strong>Email:</strong> {currentUser.Email}</p>
                <p><strong>Ad:</strong> {currentUser.FirstName}</p>
                <p><strong>Soyad:</strong> {currentUser.LastName}</p>
                <p><strong>Aktif:</strong> {currentUser.IsActive}</p>
                <p><strong>Kullanıcı Rolleri:</strong> {string.Join(", ", roles)}</p>
                <p><strong>Sistemdeki Tüm Roller:</strong> {string.Join(", ", allRoles)}</p>
                <p><strong>Admin Rolü Var mı:</strong> {roles.Contains("Admin")}</p>
                <hr>
                <a href='/Admin' class='btn btn-primary'>Admin Paneline Git</a>
                <a href='/' class='btn btn-secondary'>Anasayfaya Git</a>
            ";

            return Content(info, "text/html");
        }

        public async Task<IActionResult> MakeAdmin()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Content("Kullanıcı giriş yapmamış");
            }

            // Admin rolünün var olup olmadığını kontrol et
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // Kullanıcıyı Admin rolüne ekle
            var result = await _userManager.AddToRoleAsync(currentUser, "Admin");
            
            if (result.Succeeded)
            {
                return Content($@"
                    <h3>Başarılı!</h3>
                    <p>{currentUser.Email} kullanıcısına Admin rolü verildi.</p>
                    <a href='/Test/UserInfo' class='btn btn-info'>Bilgileri Kontrol Et</a>
                    <a href='/Admin' class='btn btn-primary'>Admin Paneline Git</a>
                ", "text/html");
            }
            else
            {
                return Content($"Hata: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
    }
}
