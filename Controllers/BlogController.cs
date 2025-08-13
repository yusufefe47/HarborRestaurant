using Microsoft.AspNetCore.Mvc;
using HarborRestaurant.Business.Abstract;

namespace HarborRestaurant.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        // Cache aktif - güvenli ayarlarla
        // [ResponseCache(Duration = 240, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new[] { "culture" })]
        public async Task<IActionResult> Index()
        {
            var posts = await _blogService.GetActivePostsAsync();
            var categories = await _blogService.GetActiveCategoriesAsync();
            
            ViewBag.Categories = categories;
            return View(posts);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var post = await _blogService.GetPostByIdAsync(id);
            if (post == null || !post.IsActive)
            {
                return NotFound();
            }

            // Görüntüleme sayısını artır
            await _blogService.IncrementViewCountAsync(id);
            
            // Diğer yazıları getir
            var recentPosts = await _blogService.GetRecentPostsAsync(5);
            ViewBag.RecentPosts = recentPosts.Where(p => p.PostId != id);

            return View(post);
        }

        public async Task<IActionResult> Category(int id)
        {
            var category = await _blogService.GetCategoryByIdAsync(id);
            if (category == null || !category.IsActive)
            {
                return NotFound();
            }

            var posts = await _blogService.GetPostsByCategoryAsync(id);
            var categories = await _blogService.GetActiveCategoriesAsync();
            
            ViewBag.Categories = categories;
            ViewBag.CurrentCategory = category;
            
            return View("Index", posts);
        }
    }
}
