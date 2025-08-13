using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SetupController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SetupController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> MakeAdmin()
        {
            try
            {
                // Önce Admin rolünün var olup olmadığını kontrol et
                if (!await _roleManager.RoleExistsAsync("Admin"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                // Mevcut kullanıcıyı al
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return Json(new { Success = false, Message = "Kullanıcı bulunamadı" });
                }

                // Kullanıcının zaten Admin rolü var mı kontrol et
                if (await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    return Json(new { Success = true, Message = "Kullanıcı zaten Admin rolüne sahip" });
                }

                // Kullanıcıyı Admin rolüne ekle
                var result = await _userManager.AddToRoleAsync(user, "Admin");

                if (result.Succeeded)
                {
                    return Json(new { Success = true, Message = "Kullanıcı başarıyla Admin rolüne eklendi" });
                }
                else
                {
                    return Json(new { Success = false, Message = "Admin rolü eklenirken hata oluştu: " + string.Join(", ", result.Errors.Select(e => e.Description)) });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = "Hata: " + ex.Message });
            }
        }
    }
}
