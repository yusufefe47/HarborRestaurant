using Microsoft.AspNetCore.Mvc;
using HarborRestaurant.Business.Abstract;
using HarborRestaurant.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;

namespace HarborRestaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class RoomsController : AdminControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var rooms = await _roomService.GetAllRoomsAsync();
            return View(rooms);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Room model, IFormFile? roomImage)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Resim yükleme işlemi
                    if (roomImage != null && roomImage.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "rooms");
                        
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + roomImage.FileName;
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await roomImage.CopyToAsync(fileStream);
                        }

                        model.ImageUrl = "/images/rooms/" + uniqueFileName;
                    }

                    await _roomService.CreateRoomAsync(model);
                    
                    SetSuccessMessage("Salon başarıyla oluşturuldu.");
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    SetErrorMessage("Salon oluşturulurken bir hata oluştu: " + ex.Message);
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var room = await _roomService.GetRoomByIdAsync(id);
            if (room == null)
            {
                SetErrorMessage("Salon bulunamadı.");
                return RedirectToAction("Index");
            }

            return View(room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Room model, IFormFile? roomImage)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingRoom = await _roomService.GetRoomByIdAsync(model.RoomId);
                    if (existingRoom == null)
                    {
                        SetErrorMessage("Salon bulunamadı.");
                        return RedirectToAction("Index");
                    }

                    // Resim yükleme işlemi
                    if (roomImage != null && roomImage.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "rooms");
                        
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + roomImage.FileName;
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await roomImage.CopyToAsync(fileStream);
                        }

                        existingRoom.ImageUrl = "/images/rooms/" + uniqueFileName;
                    }

                    existingRoom.Name = model.Name;
                    existingRoom.Description = model.Description;
                    existingRoom.Capacity = model.Capacity;
                    existingRoom.MinimumOrderAmount = model.MinimumOrderAmount;
                    existingRoom.Features = model.Features;
                    existingRoom.StarRating = model.StarRating;
                    existingRoom.IsActive = model.IsActive;
                    existingRoom.SortOrder = model.SortOrder;

                    await _roomService.UpdateRoomAsync(existingRoom);
                    
                    SetSuccessMessage("Salon başarıyla güncellendi.");
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    SetErrorMessage("Salon güncellenirken bir hata oluştu: " + ex.Message);
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var room = await _roomService.GetRoomByIdAsync(id);
            if (room == null)
            {
                SetErrorMessage("Salon bulunamadı.");
                return RedirectToAction("Index");
            }

            return View(room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _roomService.DeleteRoomAsync(id);
                SetSuccessMessage("Salon başarıyla silindi.");
            }
            catch (Exception ex)
            {
                SetErrorMessage("Salon silinirken bir hata oluştu: " + ex.Message);
            }

            return RedirectToAction("Index");
        }
    }
}
