using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class TestController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public TestController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> UserInfo()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Json(new { Error = "Kullanıcı bulunamadı" });
            }

            var roles = await _userManager.GetRolesAsync(user);
            
            return Json(new 
            { 
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = roles,
                IsAdmin = roles.Contains("Admin")
            });
        }
    }
}
