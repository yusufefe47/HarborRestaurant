using HarborRestaurant.Business.Abstract;
using HarborRestaurant.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HarborRestaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        // Kategoriler
        public async Task<IActionResult> Categories()
        {
            var categories = await _menuService.GetAllCategoriesAsync();
            return View(categories);
        }

        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(MenuCategory category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    category.CreatedDate = DateTime.Now;
                    await _menuService.AddCategoryAsync(category);
                    TempData["SuccessMessage"] = "Kategori başarıyla eklendi.";
                    return RedirectToAction(nameof(Categories));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Kategori eklenirken hata oluştu: {ex.Message}";
                }
            }
            return View(category);
        }

        public async Task<IActionResult> EditCategory(int id)
        {
            var category = await _menuService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                TempData["ErrorMessage"] = "Kategori bulunamadı.";
                return RedirectToAction(nameof(Categories));
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(int id, MenuCategory category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    category.UpdatedDate = DateTime.Now;
                    await _menuService.UpdateCategoryAsync(category);
                    TempData["SuccessMessage"] = "Kategori başarıyla güncellendi.";
                    return RedirectToAction(nameof(Categories));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Kategori güncellenirken hata oluştu: {ex.Message}";
                }
            }
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _menuService.DeleteCategoryAsync(id);
                TempData["SuccessMessage"] = "Kategori başarıyla silindi.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Kategori silinirken hata oluştu: {ex.Message}";
            }
            return RedirectToAction(nameof(Categories));
        }

        // Menü Öğeleri
        public async Task<IActionResult> Items(int? categoryId = null)
        {
            var items = categoryId.HasValue 
                ? await _menuService.GetItemsByCategoryAsync(categoryId.Value)
                : await _menuService.GetAllItemsAsync();
            
            var categories = await _menuService.GetAllCategoriesAsync();
            ViewBag.Categories = categories;
            ViewBag.SelectedCategoryId = categoryId;
            
            return View(items);
        }

        public async Task<IActionResult> CreateItem()
        {
            var categories = await _menuService.GetAllCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateItem(MenuItem item, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Resim yükleme
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "menu");
                        Directory.CreateDirectory(uploadsFolder);
                        
                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(fileStream);
                        }
                        
                        item.ImageUrl = "/images/menu/" + uniqueFileName;
                    }

                    item.CreatedDate = DateTime.Now;
                    await _menuService.AddItemAsync(item);
                    TempData["SuccessMessage"] = "Menü öğesi başarıyla eklendi.";
                    return RedirectToAction(nameof(Items));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Menü öğesi eklenirken hata oluştu: {ex.Message}";
                }
            }

            var categories = await _menuService.GetAllCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name", item.CategoryId);
            return View(item);
        }

        public async Task<IActionResult> EditItem(int id)
        {
            var item = await _menuService.GetItemByIdAsync(id);
            if (item == null)
            {
                TempData["ErrorMessage"] = "Menü öğesi bulunamadı.";
                return RedirectToAction(nameof(Items));
            }

            var categories = await _menuService.GetAllCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name", item.CategoryId);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditItem(int id, MenuItem item, IFormFile? imageFile)
        {
            if (id != item.ItemId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingItem = await _menuService.GetItemByIdAsync(id);
                    if (existingItem == null)
                    {
                        TempData["ErrorMessage"] = "Menü öğesi bulunamadı.";
                        return RedirectToAction(nameof(Items));
                    }

                    // Resim yükleme
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "menu");
                        Directory.CreateDirectory(uploadsFolder);
                        
                        // Eski resmi sil
                        if (!string.IsNullOrEmpty(existingItem.ImageUrl))
                        {
                            var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingItem.ImageUrl.TrimStart('/'));
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }
                        
                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(fileStream);
                        }
                        
                        item.ImageUrl = "/images/menu/" + uniqueFileName;
                    }
                    else
                    {
                        item.ImageUrl = existingItem.ImageUrl;
                    }

                    item.CreatedDate = existingItem.CreatedDate;
                    item.UpdatedDate = DateTime.Now;
                    await _menuService.UpdateItemAsync(item);
                    TempData["SuccessMessage"] = "Menü öğesi başarıyla güncellendi.";
                    return RedirectToAction(nameof(Items));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Menü öğesi güncellenirken hata oluştu: {ex.Message}";
                }
            }

            var categories = await _menuService.GetAllCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name", item.CategoryId);
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteItem(int id)
        {
            try
            {
                var item = await _menuService.GetItemByIdAsync(id);
                if (item != null)
                {
                    // Resmi sil
                    if (!string.IsNullOrEmpty(item.ImageUrl))
                    {
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", item.ImageUrl.TrimStart('/'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                }

                await _menuService.DeleteItemAsync(id);
                TempData["SuccessMessage"] = "Menü öğesi başarıyla silindi.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Menü öğesi silinirken hata oluştu: {ex.Message}";
            }
            return RedirectToAction(nameof(Items));
        }

        // Ana menü sayfası
        public async Task<IActionResult> Index()
        {
            var categories = await _menuService.GetAllCategoriesAsync();
            var items = await _menuService.GetAllItemsAsync();
            
            ViewBag.TotalCategories = categories.Count();
            ViewBag.TotalItems = items.Count();
            ViewBag.ActiveItems = items.Count(i => i.IsActive && i.IsAvailable);
            ViewBag.InactiveItems = items.Count(i => !i.IsActive || !i.IsAvailable);
            
            return View();
        }

        // Create action that redirects to CreateItem (for compatibility)
        public IActionResult Create()
        {
            return RedirectToAction(nameof(CreateItem));
        }
    }
}
