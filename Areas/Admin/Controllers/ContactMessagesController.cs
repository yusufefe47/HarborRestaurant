using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HarborRestaurant.Business.Abstract;

namespace HarborRestaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ContactMessagesController : Controller
    {
        private readonly IContactMessageService _contactMessageService;

        public ContactMessagesController(IContactMessageService contactMessageService)
        {
            _contactMessageService = contactMessageService;
        }

        public async Task<IActionResult> Index()
        {
            var messages = await _contactMessageService.GetAllContactMessagesAsync();
            return View(messages.OrderByDescending(m => m.CreatedDate));
        }

        public async Task<IActionResult> Details(int id)
        {
            var message = await _contactMessageService.GetContactMessageByIdAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            // Mesajı okundu olarak işaretle
            if (!message.IsRead)
            {
                message.IsRead = true;
                message.ReadDate = DateTime.Now;
                await _contactMessageService.UpdateContactMessageAsync(message);
            }

            return View(message);
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var message = await _contactMessageService.GetContactMessageByIdAsync(id);
            if (message != null)
            {
                message.IsRead = true;
                message.ReadDate = DateTime.Now;
                await _contactMessageService.UpdateContactMessageAsync(message);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsUnread(int id)
        {
            var message = await _contactMessageService.GetContactMessageByIdAsync(id);
            if (message != null)
            {
                message.IsRead = false;
                message.ReadDate = null;
                await _contactMessageService.UpdateContactMessageAsync(message);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsReplied(int id)
        {
            var message = await _contactMessageService.GetContactMessageByIdAsync(id);
            if (message != null)
            {
                message.IsReplied = true;
                message.ReplyDate = DateTime.Now;
                await _contactMessageService.UpdateContactMessageAsync(message);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var message = await _contactMessageService.GetContactMessageByIdAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            return View(message);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _contactMessageService.DeleteContactMessageAsync(id);
            TempData["SuccessMessage"] = "İletişim mesajı başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> BulkAction(string action, int[] selectedIds)
        {
            if (selectedIds == null || !selectedIds.Any())
            {
                TempData["ErrorMessage"] = "Lütfen en az bir mesaj seçin.";
                return RedirectToAction(nameof(Index));
            }

            switch (action)
            {
                case "markRead":
                    foreach (var id in selectedIds)
                    {
                        var message = await _contactMessageService.GetContactMessageByIdAsync(id);
                        if (message != null)
                        {
                            message.IsRead = true;
                            message.ReadDate = DateTime.Now;
                            await _contactMessageService.UpdateContactMessageAsync(message);
                        }
                    }
                    TempData["SuccessMessage"] = "Seçili mesajlar okundu olarak işaretlendi.";
                    break;

                case "markUnread":
                    foreach (var id in selectedIds)
                    {
                        var message = await _contactMessageService.GetContactMessageByIdAsync(id);
                        if (message != null)
                        {
                            message.IsRead = false;
                            message.ReadDate = null;
                            await _contactMessageService.UpdateContactMessageAsync(message);
                        }
                    }
                    TempData["SuccessMessage"] = "Seçili mesajlar okunmadı olarak işaretlendi.";
                    break;

                case "delete":
                    foreach (var id in selectedIds)
                    {
                        await _contactMessageService.DeleteContactMessageAsync(id);
                    }
                    TempData["SuccessMessage"] = "Seçili mesajlar başarıyla silindi.";
                    break;
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GetUnreadCount()
        {
            var unreadMessages = await _contactMessageService.GetUnreadContactMessagesAsync();
            return Json(new { count = unreadMessages.Count() });
        }
    }
}
