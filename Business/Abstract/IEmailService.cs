namespace HarborRestaurant.Business.Abstract
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
        Task SendReservationConfirmationAsync(string to, string customerName, DateTime reservationDate, int guestCount);
        Task SendContactMessageNotificationAsync(string customerName, string email, string subject, string message);
        Task SendWelcomeEmailAsync(string to, string userName);
    }
}
