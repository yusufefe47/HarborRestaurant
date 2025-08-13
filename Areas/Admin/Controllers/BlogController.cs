using Microsoft.AspNetCore.Mvc;
using HarborRestaurant.Business.Abstract;
using HarborRestaurant.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using HarborRestaurant.Business.Concrete;

namespace HarborRestaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BlogController : AdminControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly IFileService _fileService;
        private readonly ILogger<BlogController> _logger;

        public BlogController(IBlogService blogService, IFileService fileService, ILogger<BlogController> logger)
        {
            _blogService = blogService;
            _fileService = fileService;
            _logger = logger;
        }

        #region Categories

        [HttpGet]
        public async Task<IActionResult> Categories()
        {
            var categories = await _blogService.GetAllCategoriesAsync();
            return View(categories);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(BlogCategory model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreatedDate = DateTime.Now;
                    model.IsActive = true;
                    
                    await _blogService.AddCategoryAsync(model);
                    
                    SetSuccessMessage("Kategori başarıyla oluşturuldu.");
                    return RedirectToAction("Categories");
                }
                catch (Exception ex)
                {
                    SetErrorMessage("Kategori oluşturulurken bir hata oluştu: " + ex.Message);
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditCategory(int id)
        {
            var category = await _blogService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                SetErrorMessage("Kategori bulunamadı.");
                return RedirectToAction("Categories");
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(BlogCategory model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingCategory = await _blogService.GetCategoryByIdAsync(model.CategoryId);
                    if (existingCategory == null)
                    {
                        SetErrorMessage("Kategori bulunamadı.");
                        return RedirectToAction("Categories");
                    }

                    existingCategory.Name = model.Name;
                    existingCategory.Description = model.Description;
                    existingCategory.IsActive = model.IsActive;
                    existingCategory.UpdatedDate = DateTime.Now;

                    await _blogService.UpdateCategoryAsync(existingCategory);
                    
                    SetSuccessMessage("Kategori başarıyla güncellendi.");
                    return RedirectToAction("Categories");
                }
                catch (Exception ex)
                {
                    SetErrorMessage("Kategori güncellenirken bir hata oluştu: " + ex.Message);
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _blogService.DeleteCategoryAsync(id);
                SetSuccessMessage("Kategori başarıyla silindi.");
            }
            catch (Exception ex)
            {
                SetErrorMessage("Kategori silinirken bir hata oluştu: " + ex.Message);
            }

            return RedirectToAction("Categories");
        }

        #endregion

        #region Posts

        [HttpGet]
        public async Task<IActionResult> Posts()
        {
            // Admin tarafında tüm yazıları görmek için aktif filtre uygulamadan getiriyoruz
            var posts = await _blogService.GetAllPostsAsync();
            return View(posts);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _blogService.GetActiveCategoriesAsync();
            ViewBag.Categories = categories;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreatePost()
        {
            var categories = await _blogService.GetActiveCategoriesAsync();
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(BlogPost model, IFormFile? featuredImage)
        {
            try
            {
                // ModelState'den CategoryId validasyonunu kaldır
                ModelState.Remove("CategoryId");
                ModelState.Remove("Category");
                
                // CategoryId 0 ise veya boşsa veritabanında varsayılan kategori oluştur
                if (model.CategoryId <= 0)
                {
                    var categories = await _blogService.GetActiveCategoriesAsync();
                    if (!categories.Any())
                    {
                        // Varsayılan kategori oluştur
                        var defaultCategory = new BlogCategory
                        {
                            Name = "Genel",
                            Description = "Genel blog yazıları",
                            IsActive = true,
                            CreatedDate = DateTime.Now,
                            SortOrder = 1
                        };
                        await _blogService.AddCategoryAsync(defaultCategory);
                        
                        // Yeni oluşturulan kategorinin ID'sini al
                        var allCategories = await _blogService.GetAllCategoriesAsync();
                        var lastCreated = allCategories
                            .OrderByDescending(c => c.CreatedDate)
                            .FirstOrDefault();
                        if (lastCreated == null)
                        {
                            SetErrorMessage("Kategori oluşturulamadı.");
                            var categoriesAfterError = await _blogService.GetActiveCategoriesAsync();
                            ViewBag.Categories = categoriesAfterError;
                            return View(model);
                        }
                        model.CategoryId = lastCreated.CategoryId;
                    }
                    else
                    {
                        var firstActive = categories.OrderBy(c => c.SortOrder).ThenBy(c => c.CreatedDate).FirstOrDefault();
                        if (firstActive == null)
                        {
                            SetErrorMessage("Aktif kategori bulunamadı.");
                            ViewBag.Categories = categories;
                            return View(model);
                        }
                        model.CategoryId = firstActive.CategoryId;
                    }
                }

                // Debug: ModelState hatalarını kontrol et
                if (!ModelState.IsValid)
                {
                    var errors = ModelState
                        .Where(x => x.Value?.Errors.Count > 0)
                        .Select(x => new { Field = x.Key, Errors = x.Value?.Errors.Select(e => e.ErrorMessage) })
                        .ToList();
                    
                    SetErrorMessage("Form verilerinde hatalar var: " + string.Join(", ", errors.SelectMany(e => e.Errors ?? new List<string>())));
                    
                    var categories = await _blogService.GetActiveCategoriesAsync();
                    ViewBag.Categories = categories;
                    return View(model);
                }

                // Resim yükleme işlemi (FileService ile)
                if (featuredImage != null && featuredImage.Length > 0)
                {
                    try
                    {
                        var url = await _fileService.UploadImageAsync(featuredImage, "blog");
                        model.ImageUrl = url;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Blog resmi yüklenemedi.");
                        SetErrorMessage("Resim yüklenirken bir hata oluştu: " + ex.Message);
                        var categoriesAfterError = await _blogService.GetActiveCategoriesAsync();
                        ViewBag.Categories = categoriesAfterError;
                        return View(model);
                    }
                }

                model.CreatedDate = DateTime.Now;
                model.PublishedDate = DateTime.Now;
                model.Author = User.Identity?.Name ?? "Admin";
                model.ViewCount = 0;
                
                await _blogService.AddPostAsync(model);
                
                SetSuccessMessage("Blog yazısı başarıyla oluşturuldu.");
                return RedirectToAction("Posts");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Blog yazısı oluşturulurken hata oluştu.");
                SetErrorMessage("Blog yazısı oluşturulurken bir hata oluştu: " + ex.Message);
            }

            var categoriesOnError = await _blogService.GetActiveCategoriesAsync();
            ViewBag.Categories = categoriesOnError;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditPost(int id)
        {
            var post = await _blogService.GetPostByIdAsync(id);
            if (post == null)
            {
                SetErrorMessage("Blog yazısı bulunamadı.");
                return RedirectToAction("Posts");
            }

            var categories = await _blogService.GetActiveCategoriesAsync();
            ViewBag.Categories = categories;
            
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(BlogPost model, IFormFile? featuredImage)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingPost = await _blogService.GetPostByIdAsync(model.PostId);
                    if (existingPost == null)
                    {
                        SetErrorMessage("Blog yazısı bulunamadı.");
                        return RedirectToAction("Posts");
                    }

                    // Resim yükleme işlemi (FileService ile)
                    if (featuredImage != null && featuredImage.Length > 0)
                    {
                        try
                        {
                            var url = await _fileService.UploadImageAsync(featuredImage, "blog");
                            existingPost.ImageUrl = url;
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Blog resmi güncellenirken yüklenemedi.");
                            SetErrorMessage("Resim yüklenirken bir hata oluştu: " + ex.Message);
                            var categoriesAfterError = await _blogService.GetActiveCategoriesAsync();
                            ViewBag.Categories = categoriesAfterError;
                            return View(model);
                        }
                    }

                    existingPost.Title = model.Title;
                    existingPost.Summary = model.Summary;
                    existingPost.Content = model.Content;
                    existingPost.Summary = model.Summary;
                    existingPost.CategoryId = model.CategoryId;
                    existingPost.MetaTitle = model.MetaTitle;
                    existingPost.MetaDescription = model.MetaDescription;
                    existingPost.IsFeatured = model.IsFeatured;
                    existingPost.IsActive = model.IsActive;
                    existingPost.UpdatedDate = DateTime.Now;

                    await _blogService.UpdatePostAsync(existingPost);
                    
                    SetSuccessMessage("Blog yazısı başarıyla güncellendi.");
                    return RedirectToAction("Posts");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Blog yazısı güncellenirken hata oluştu.");
                    SetErrorMessage("Blog yazısı güncellenirken bir hata oluştu: " + ex.Message);
                }
            }

            var categories = await _blogService.GetActiveCategoriesAsync();
            ViewBag.Categories = categories;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DetailPost(int id)
        {
            var post = await _blogService.GetPostByIdAsync(id);
            if (post == null)
            {
                SetErrorMessage("Blog yazısı bulunamadı.");
                return RedirectToAction("Posts");
            }

            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                await _blogService.DeletePostAsync(id);
                SetSuccessMessage("Blog yazısı başarıyla silindi.");
            }
            catch (Exception ex)
            {
                SetErrorMessage("Blog yazısı silinirken bir hata oluştu: " + ex.Message);
            }

            return RedirectToAction("Posts");
        }

        #endregion

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var posts = await _blogService.GetRecentPostsAsync(5);
            var categories = await _blogService.GetActiveCategoriesAsync();
            
            ViewBag.TotalPosts = (await _blogService.GetAllPostsAsync()).Count();
            ViewBag.ActivePosts = (await _blogService.GetActivePostsAsync()).Count();
            ViewBag.TotalCategories = categories.Count();
            ViewBag.FeaturedPosts = (await _blogService.GetFeaturedPostsAsync()).Count();
            
            return View(posts);
        }
    }
}
