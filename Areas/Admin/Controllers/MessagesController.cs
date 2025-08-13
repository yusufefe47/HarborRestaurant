using Microsoft.AspNetCore.Mvc;
using HarborRestaurant.Business.Abstract;
using Microsoft.AspNetCore.Authorization;

namespace HarborRestaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class MessagesController : AdminControllerBase
    {
        private readonly IContactMessageService _contactMessageService;

        public MessagesController(IContactMessageService contactMessageService)
        {
            _contactMessageService = contactMessageService;
        }

        // LIST ALL MESSAGES
        [HttpGet]
        public IActionResult Index(bool? isRead, int page = 1)
        {
            try
            {
                var messages = _contactMessageService.GetAll();

                // Filter by read status
                if (isRead.HasValue)
                {
                    messages = messages.Where(m => m.IsRead == isRead.Value);
                }

                // Order by date (unread first, then by created date desc)
                messages = messages.OrderBy(m => m.IsRead).ThenByDescending(m => m.SentDate);

                // Pagination
                const int pageSize = 20;
                var paginatedMessages = messages.Skip((page - 1) * pageSize).Take(pageSize);

                ViewBag.IsRead = isRead;
                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = (int)Math.Ceiling((double)messages.Count() / pageSize);
                ViewBag.TotalCount = messages.Count();
                ViewBag.UnreadCount = messages.Count(m => !m.IsRead);

                return View(paginatedMessages);
            }
            catch (Exception ex)
            {
                SetErrorMessage("Mesajlar yüklenirken bir hata oluştu: " + ex.Message);
                return View(new List<HarborRestaurant.Entities.Concrete.ContactMessage>());
            }
        }

        // MESSAGE DETAILS
        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
                var message = _contactMessageService.GetById(id);
                if (message == null)
                {
                    SetErrorMessage("Mesaj bulunamadı.");
                    return RedirectToAction("Index");
                }

                // Mark as read
                if (!message.IsRead)
                {
                    message.IsRead = true;
                    message.ReadDate = DateTime.Now;
                    _contactMessageService.Update(message);
                }

                return View(message);
            }
            catch (Exception ex)
            {
                SetErrorMessage("Mesaj detayları yüklenirken bir hata oluştu: " + ex.Message);
                return RedirectToAction("Index");
            }
        }

        // MARK AS READ
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MarkAsRead(int id)
        {
            try
            {
                var message = _contactMessageService.GetById(id);
                if (message == null)
                {
                    SetErrorMessage("Mesaj bulunamadı.");
                    return RedirectToAction("Index");
                }

                message.IsRead = true;
                message.ReadDate = DateTime.Now;
                _contactMessageService.Update(message);
                
                SetSuccessMessage("Mesaj okundu olarak işaretlendi.");
                return RedirectToAction("Details", new { id = id });
            }
            catch (Exception ex)
            {
                SetErrorMessage("Mesaj güncellenirken bir hata oluştu: " + ex.Message);
                return RedirectToAction("Index");
            }
        }

        // MARK AS UNREAD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MarkAsUnread(int id)
        {
            try
            {
                var message = _contactMessageService.GetById(id);
                if (message == null)
                {
                    SetErrorMessage("Mesaj bulunamadı.");
                    return RedirectToAction("Index");
                }

                message.IsRead = false;
                message.ReadDate = null;
                _contactMessageService.Update(message);
                
                SetSuccessMessage("Mesaj okunmadı olarak işaretlendi.");
                return RedirectToAction("Details", new { id = id });
            }
            catch (Exception ex)
            {
                SetErrorMessage("Mesaj güncellenirken bir hata oluştu: " + ex.Message);
                return RedirectToAction("Index");
            }
        }

        // DELETE MESSAGE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var message = _contactMessageService.GetById(id);
                if (message == null)
                {
                    SetErrorMessage("Mesaj bulunamadı.");
                    return RedirectToAction("Index");
                }

                _contactMessageService.Delete(message);
                SetSuccessMessage("Mesaj başarıyla silindi.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                SetErrorMessage("Mesaj silinirken bir hata oluştu: " + ex.Message);
                return RedirectToAction("Index");
            }
        }

        // BULK ACTIONS
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BulkAction(string action, int[] messageIds)
        {
            if (messageIds == null || !messageIds.Any())
            {
                SetWarningMessage("Lütfen işlem yapmak için mesaj seçin.");
                return RedirectToAction("Index");
            }

            try
            {
                int successCount = 0;

                foreach (var messageId in messageIds)
                {
                    var message = _contactMessageService.GetById(messageId);
                    if (message != null)
                    {
                        switch (action.ToLower())
                        {
                            case "markread":
                                message.IsRead = true;
                                message.ReadDate = DateTime.Now;
                                _contactMessageService.Update(message);
                                successCount++;
                                break;

                            case "markunread":
                                message.IsRead = false;
                                message.ReadDate = null;
                                _contactMessageService.Update(message);
                                successCount++;
                                break;

                            case "delete":
                                _contactMessageService.Delete(message);
                                successCount++;
                                break;
                        }
                    }
                }

                var actionText = action.ToLower() switch
                {
                    "markread" => "okundu olarak işaretlendi",
                    "markunread" => "okunmadı olarak işaretlendi",
                    "delete" => "silindi",
                    _ => "işlendi"
                };

                SetSuccessMessage($"{successCount} mesaj başarıyla {actionText}.");
            }
            catch (Exception ex)
            {
                SetErrorMessage("Toplu işlem sırasında bir hata oluştu: " + ex.Message);
            }

            return RedirectToAction("Index");
        }

        // REPLY TO MESSAGE
        [HttpGet]
        public IActionResult Reply(int id)
        {
            try
            {
                var message = _contactMessageService.GetById(id);
                if (message == null)
                {
                    SetErrorMessage("Mesaj bulunamadı.");
                    return RedirectToAction("Index");
                }

                ViewBag.OriginalMessage = message;
                return View();
            }
            catch (Exception ex)
            {
                SetErrorMessage("Yanıt sayfası yüklenirken bir hata oluştu: " + ex.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reply(int id, string replySubject, string replyMessage)
        {
            try
            {
                var originalMessage = _contactMessageService.GetById(id);
                if (originalMessage == null)
                {
                    SetErrorMessage("Orijinal mesaj bulunamadı.");
                    return RedirectToAction("Index");
                }

                // Here you would implement email sending logic
                // For now, we'll just mark the original message as replied
                originalMessage.IsReplied = true;
                originalMessage.ReplyDate = DateTime.Now;
                _contactMessageService.Update(originalMessage);

                SetSuccessMessage("Yanıt başarıyla gönderildi.");
                SetInfoMessage("Not: Email gönderme özelliği henüz aktif değil. Mesaj 'Yanıtlandı' olarak işaretlendi.");

                return RedirectToAction("Details", new { id = id });
            }
            catch (Exception ex)
            {
                SetErrorMessage("Yanıt gönderilirken bir hata oluştu: " + ex.Message);
                return RedirectToAction("Reply", new { id = id });
            }
        }

        // STATISTICS
        [HttpGet]
        public IActionResult Statistics()
        {
            try
            {
                var messages = _contactMessageService.GetAll();

                var stats = new
                {
                    TotalMessages = messages.Count(),
                    UnreadMessages = messages.Count(m => !m.IsRead),
                    ReadMessages = messages.Count(m => m.IsRead),
                    RepliedMessages = messages.Count(m => m.IsReplied),
                    TodayMessages = messages.Count(m => m.SentDate.Date == DateTime.Today),
                    WeekMessages = messages.Count(m => m.SentDate >= DateTime.Today.AddDays(-7)),
                    MonthMessages = messages.Count(m => m.SentDate >= DateTime.Today.AddMonths(-1))
                };

                ViewBag.Statistics = stats;

                // Daily message chart data for last 30 days
                var chartData = new Dictionary<DateTime, int>();
                for (int i = 29; i >= 0; i--)
                {
                    var date = DateTime.Today.AddDays(-i);
                    var count = messages.Count(m => m.SentDate.Date == date);
                    chartData.Add(date, count);
                }

                ViewBag.ChartData = chartData;

                return View();
            }
            catch (Exception ex)
            {
                SetErrorMessage("İstatistikler yüklenirken bir hata oluştu: " + ex.Message);
                return RedirectToAction("Index");
            }
        }
    }
}
