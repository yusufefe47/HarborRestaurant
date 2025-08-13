using Microsoft.AspNetCore.Mvc;
using HarborRestaurant.Business.Abstract;
using HarborRestaurant.Entities.Concrete;
using Microsoft.Extensions.Localization;

namespace HarborRestaurant.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IContactMessageService _contactMessageService;
        private readonly IEmailService _emailService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public ContactController(
            IContactService contactService,
            IContactMessageService contactMessageService,
            IEmailService emailService,
            IStringLocalizer<SharedResource> localizer)
        {
            _contactService = contactService;
            _contactMessageService = contactMessageService;
            _emailService = emailService;
            _localizer = localizer;
        }

        public async Task<IActionResult> Index()
        {
            var contact = await _contactService.GetActiveContactAsync();
            return View(contact);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string FullName, string Email, string Subject, string Message)
        {
            try
            {
                if (string.IsNullOrEmpty(FullName) || string.IsNullOrEmpty(Email) || 
                    string.IsNullOrEmpty(Subject) || string.IsNullOrEmpty(Message))
                {
                    TempData["ErrorMessage"] = _localizer["Contact.Error"];
                    return RedirectToAction("Index");
                }

                var contactMessage = new ContactMessage
                {
                    FullName = FullName,
                    Email = Email,
                    Subject = Subject,
                    Message = Message,
                    SentDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    IsRead = false,
                    IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString()
                };

                await _contactMessageService.AddAsync(contactMessage);
                
                // Admin'e email bildirimi gönder
                try
                {
                    await _emailService.SendContactMessageNotificationAsync(FullName, Email, Subject, Message);
                }
                catch (Exception emailEx)
                {
                    // Email gönderimi başarısız olsa bile mesaj kaydedilir
                    System.Diagnostics.Debug.WriteLine($"Email gönderimi başarısız: {emailEx.Message}");
                }
                
                TempData["SuccessMessage"] = _localizer["Contact.Success"];
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = _localizer["Contact.Error"];
                return RedirectToAction("Index");
            }
        }
    }
}
