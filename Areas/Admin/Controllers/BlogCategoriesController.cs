using HarborRestaurant.Business.Abstract;
using HarborRestaurant.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HarborRestaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BlogCategoriesController : AdminControllerBase
    {
        private readonly IBlogCategoryService _blogCategoryService;

        public BlogCategoriesController(IBlogCategoryService blogCategoryService)
        {
            _blogCategoryService = blogCategoryService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var categories = await _blogCategoryService.GetAllAsync();
                return View(categories.OrderBy(x => x.SortOrder).ThenBy(x => x.Name));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Kategoriler yüklenirken bir hata oluştu: " + ex.Message;
                return View(new List<BlogCategory>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var category = await _blogCategoryService.GetByIdAsync(id);
                if (category == null)
                {
                    TempData["Error"] = "Kategori bulunamadı";
                    return RedirectToAction(nameof(Index));
                }

                return View(category);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Kategori detayları yüklenirken bir hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Create()
        {
            return View(new BlogCategory());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCategory blogCategory)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(blogCategory);
                }

                // Check if name already exists
                if (await _blogCategoryService.NameExistsAsync(blogCategory.Name))
                {
                    ModelState.AddModelError("Name", "Bu kategori adı zaten kullanılıyor");
                    return View(blogCategory);
                }

                await _blogCategoryService.CreateAsync(blogCategory);
                TempData["Success"] = "Blog kategorisi başarıyla oluşturuldu";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Kategori oluşturulurken bir hata oluştu: " + ex.Message;
                return View(blogCategory);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var category = await _blogCategoryService.GetByIdAsync(id);
                if (category == null)
                {
                    TempData["Error"] = "Kategori bulunamadı";
                    return RedirectToAction(nameof(Index));
                }

                return View(category);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Kategori yüklenirken bir hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BlogCategory blogCategory)
        {
            try
            {
                if (id != blogCategory.CategoryId)
                {
                    TempData["Error"] = "Geçersiz kategori ID";
                    return RedirectToAction(nameof(Index));
                }

                if (!ModelState.IsValid)
                {
                    return View(blogCategory);
                }

                // Check if name already exists (excluding current category)
                if (await _blogCategoryService.NameExistsAsync(blogCategory.Name, id))
                {
                    ModelState.AddModelError("Name", "Bu kategori adı zaten kullanılıyor");
                    return View(blogCategory);
                }

                await _blogCategoryService.UpdateAsync(blogCategory);
                TempData["Success"] = "Blog kategorisi başarıyla güncellendi";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Kategori güncellenirken bir hata oluştu: " + ex.Message;
                return View(blogCategory);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _blogCategoryService.DeleteAsync(id);
                if (success)
                {
                    TempData["Success"] = "Blog kategorisi başarıyla silindi";
                }
                else
                {
                    TempData["Error"] = "Kategori bulunamadı";
                }
            }
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.Message;
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Kategori silinirken bir hata oluştu: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleActive(int id)
        {
            try
            {
                var success = await _blogCategoryService.ToggleActiveAsync(id);
                if (success)
                {
                    TempData["Success"] = "Kategori durumu başarıyla güncellendi";
                }
                else
                {
                    TempData["Error"] = "Kategori bulunamadı";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Kategori durumu güncellenirken bir hata oluştu: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // AJAX methods
        [HttpGet]
        public async Task<IActionResult> CheckName(string name, int? id)
        {
            var exists = await _blogCategoryService.NameExistsAsync(name, id);
            return Json(!exists);
        }
    }
}
