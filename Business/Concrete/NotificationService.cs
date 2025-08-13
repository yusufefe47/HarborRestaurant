using Microsoft.AspNetCore.SignalR;
using HarborRestaurant.Hubs;

namespace HarborRestaurant.Business.Concrete
{
    public interface INotificationService
    {
        Task SendReservationNotificationAsync(string message);
        Task SendContactMessageNotificationAsync(string message);
        Task SendGeneralNotificationAsync(string message);
    }

    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendReservationNotificationAsync(string message)
        {
            await _hubContext.Clients.Group("Admins").SendAsync("ReceiveReservationNotification", message);
        }

        public async Task SendContactMessageNotificationAsync(string message)
        {
            await _hubContext.Clients.Group("Admins").SendAsync("ReceiveContactNotification", message);
        }

        public async Task SendGeneralNotificationAsync(string message)
        {
            await _hubContext.Clients.Group("Admins").SendAsync("ReceiveNotification", message);
        }
    }
}
