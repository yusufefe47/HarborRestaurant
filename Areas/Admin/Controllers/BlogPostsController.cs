using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HarborRestaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BlogPostsController : Controller
    {
        // Redirect to the actual Blog controller's Posts action for backward compatibility
        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("Posts", "Blog");
        }
    }
}
