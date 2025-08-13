using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HarborRestaurant.Business.Abstract;
using HarborRestaurant.Entities.Concrete;
using HarborRestaurant.Models.ViewModels;

namespace HarborRestaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MenuItemsController : Controller
    {
        private readonly IMenuService _menuService;

        public MenuItemsController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public async Task<IActionResult> Index()
        {
            var menuItems = await _menuService.GetAllMenuItemsWithCategoryAsync();
            return View(menuItems);
        }

        public async Task<IActionResult> Details(int id)
        {
            var menuItem = await _menuService.GetMenuItemByIdAsync(id);
            if (menuItem == null)
            {
                return NotFound();
            }
            return View(menuItem);
        }

        public async Task<IActionResult> Create()
        {
            await LoadViewBagData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuItem menuItem)
        {
            if (ModelState.IsValid)
            {
                menuItem.CreatedDate = DateTime.Now;
                await _menuService.AddMenuItemAsync(menuItem);
                TempData["SuccessMessage"] = "Menü öğesi başarıyla eklendi.";
                return RedirectToAction(nameof(Index));
            }
            await LoadViewBagData();
            return View(menuItem);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var menuItem = await _menuService.GetMenuItemByIdAsync(id);
            if (menuItem == null)
            {
                return NotFound();
            }
            await LoadViewBagData();
            return View(menuItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MenuItem menuItem)
        {
            if (id != menuItem.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                menuItem.UpdatedDate = DateTime.Now;
                await _menuService.UpdateMenuItemAsync(menuItem);
                TempData["SuccessMessage"] = "Menü öğesi başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            await LoadViewBagData();
            return View(menuItem);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var menuItem = await _menuService.GetMenuItemByIdAsync(id);
            if (menuItem == null)
            {
                return NotFound();
            }
            return View(menuItem);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _menuService.DeleteMenuItemAsync(id);
            TempData["SuccessMessage"] = "Menü öğesi başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> ToggleActive(int id)
        {
            var menuItem = await _menuService.GetMenuItemByIdAsync(id);
            if (menuItem != null)
            {
                menuItem.IsActive = !menuItem.IsActive;
                menuItem.UpdatedDate = DateTime.Now;
                await _menuService.UpdateMenuItemAsync(menuItem);
                return Json(new { success = true, isActive = menuItem.IsActive });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> ToggleAvailable(int id)
        {
            var menuItem = await _menuService.GetMenuItemByIdAsync(id);
            if (menuItem != null)
            {
                menuItem.IsAvailable = !menuItem.IsAvailable;
                menuItem.UpdatedDate = DateTime.Now;
                await _menuService.UpdateMenuItemAsync(menuItem);
                return Json(new { success = true, isAvailable = menuItem.IsAvailable });
            }
            return Json(new { success = false });
        }

        private async Task LoadViewBagData()
        {
            var categories = await _menuService.GetAllMenuCategoriesAsync();
            ViewBag.Categories = new SelectList(categories.Where(c => c.IsActive), "CategoryId", "Name");
        }
    }
}
