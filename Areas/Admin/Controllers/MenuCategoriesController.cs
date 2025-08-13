using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HarborRestaurant.Business.Abstract;
using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MenuCategoriesController : Controller
    {
        private readonly IMenuService _menuService;

        public MenuCategoriesController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _menuService.GetAllMenuCategoriesAsync();
            return View(categories);
        }

        public async Task<IActionResult> Details(int id)
        {
            var category = await _menuService.GetMenuCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuCategory category)
        {
            if (ModelState.IsValid)
            {
                category.CreatedDate = DateTime.Now;
                await _menuService.AddMenuCategoryAsync(category);
                TempData["SuccessMessage"] = "Menü kategorisi başarıyla eklendi.";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _menuService.GetMenuCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MenuCategory category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                category.UpdatedDate = DateTime.Now;
                await _menuService.UpdateMenuCategoryAsync(category);
                TempData["SuccessMessage"] = "Menü kategorisi başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _menuService.GetMenuCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _menuService.DeleteMenuCategoryAsync(id);
            TempData["SuccessMessage"] = "Menü kategorisi başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> ToggleActive(int id)
        {
            var category = await _menuService.GetMenuCategoryByIdAsync(id);
            if (category != null)
            {
                category.IsActive = !category.IsActive;
                category.UpdatedDate = DateTime.Now;
                await _menuService.UpdateMenuCategoryAsync(category);
                return Json(new { success = true, isActive = category.IsActive });
            }
            return Json(new { success = false });
        }
    }
}
