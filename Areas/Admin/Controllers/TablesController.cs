using Microsoft.AspNetCore.Mvc;
using HarborRestaurant.Business.Abstract;
using HarborRestaurant.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;

namespace HarborRestaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class TablesController : AdminControllerBase
    {
        private readonly ITableService _tableService;

        public TablesController(ITableService tableService)
        {
            _tableService = tableService;
        }

        // LIST ALL TABLES
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var tables = await _tableService.GetAllAsync();
                tables = tables.OrderBy(t => t.TableNumber);
                return View(tables);
            }
            catch (Exception ex)
            {
                SetErrorMessage("Masalar yüklenirken bir hata oluştu: " + ex.Message);
                return View(new List<Table>());
            }
        }

        // TABLE DETAILS
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var table = await _tableService.GetByIdAsync(id);
                if (table == null)
                {
                    SetErrorMessage("Masa bulunamadı.");
                    return RedirectToAction("Index");
                }

                return View(table);
            }
            catch (Exception ex)
            {
                SetErrorMessage("Masa detayları yüklenirken bir hata oluştu: " + ex.Message);
                return RedirectToAction("Index");
            }
        }

        // CREATE NEW TABLE
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Table());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Table table)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Check if table number already exists
                    if (await _tableService.IsTableNumberExistsAsync(table.TableNumber))
                    {
                        ModelState.AddModelError("TableNumber", "Bu masa numarası zaten mevcut.");
                        return View(table);
                    }

                    await _tableService.CreateAsync(table);
                    SetSuccessMessage("Masa başarıyla oluşturuldu.");
                    return RedirectToAction("Details", new { id = table.TableId });
                }
                catch (Exception ex)
                {
                    SetErrorMessage("Masa oluşturulurken bir hata oluştu: " + ex.Message);
                }
            }

            return View(table);
        }

        // EDIT TABLE
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var table = await _tableService.GetByIdAsync(id);
                if (table == null)
                {
                    SetErrorMessage("Masa bulunamadı.");
                    return RedirectToAction("Index");
                }

                return View(table);
            }
            catch (Exception ex)
            {
                SetErrorMessage("Masa düzenleme sayfası yüklenirken bir hata oluştu: " + ex.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Table table)
        {
            if (id != table.TableId)
            {
                SetErrorMessage("Masa ID eşleşmiyor.");
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Check if table number already exists for other tables
                    if (await _tableService.IsTableNumberExistsAsync(table.TableNumber, table.TableId))
                    {
                        ModelState.AddModelError("TableNumber", "Bu masa numarası başka bir masa tarafından kullanılıyor.");
                        return View(table);
                    }

                    var existingTable = await _tableService.GetByIdAsync(id);
                    if (existingTable == null)
                    {
                        SetErrorMessage("Masa bulunamadı.");
                        return RedirectToAction("Index");
                    }

                    // Update fields
                    existingTable.TableNumber = table.TableNumber;
                    existingTable.Capacity = table.Capacity;
                    existingTable.Location = table.Location;
                    existingTable.IsActive = table.IsActive;
                    
                    await _tableService.UpdateAsync(existingTable);
                    SetSuccessMessage("Masa başarıyla güncellendi.");
                    return RedirectToAction("Details", new { id = id });
                }
                catch (Exception ex)
                {
                    SetErrorMessage("Masa güncellenirken bir hata oluştu: " + ex.Message);
                }
            }

            return View(table);
        }

        // DELETE TABLE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var table = await _tableService.GetByIdAsync(id);
                if (table == null)
                {
                    SetErrorMessage("Masa bulunamadı.");
                    return RedirectToAction("Index");
                }

                await _tableService.DeleteAsync(id);
                SetSuccessMessage("Masa başarıyla silindi.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                SetErrorMessage("Masa silinirken bir hata oluştu: " + ex.Message);
                return RedirectToAction("Index");
            }
        }

        // TOGGLE TABLE STATUS
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            try
            {
                var table = await _tableService.GetByIdAsync(id);
                if (table == null)
                {
                    SetErrorMessage("Masa bulunamadı.");
                    return RedirectToAction("Index");
                }

                table.IsActive = !table.IsActive;
                await _tableService.UpdateAsync(table);
                
                var statusText = table.IsActive ? "aktif" : "pasif";
                SetSuccessMessage($"Masa durumu '{statusText}' olarak güncellendi.");
                
                return RedirectToAction("Details", new { id = id });
            }
            catch (Exception ex)
            {
                SetErrorMessage("Masa durumu güncellenirken bir hata oluştu: " + ex.Message);
                return RedirectToAction("Index");
            }
        }

        // GET AVAILABLE TABLES (AJAX)
        [HttpGet]
        public async Task<IActionResult> GetAvailableTables(DateTime date, string time, int guestCount = 1)
        {
            try
            {
                if (!TimeOnly.TryParse(time, out var reservationTime))
                {
                    return Json(new { success = false, message = "Geçersiz saat formatı." });
                }

                var availableTables = await _tableService.GetAvailableTablesAsync(date, reservationTime, guestCount);
                
                var tableOptions = availableTables.Select(t => new 
                {
                    value = t.TableId,
                    text = $"Masa {t.TableNumber} (Kapasite: {t.Capacity})",
                    location = t.Location
                });

                return Json(new { success = true, tables = tableOptions });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Müsait masalar yüklenirken hata oluştu: " + ex.Message });
            }
        }
    }
}
