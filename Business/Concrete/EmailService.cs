using HarborRestaurant.Business.Abstract;
using System.Net.Mail;
using System.Net;

namespace HarborRestaurant.Business.Concrete
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                var smtpSettings = _configuration.GetSection("EmailSettings");
                
                using var client = new SmtpClient(smtpSettings["SmtpServer"])
                {
                    Port = int.Parse(smtpSettings["Port"] ?? "587"),
                    Credentials = new NetworkCredential(smtpSettings["Username"], smtpSettings["Password"]),
                    EnableSsl = bool.Parse(smtpSettings["EnableSsl"] ?? "true"),
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(smtpSettings["FromEmail"] ?? "info@harborrestaurant.com", "Harbor Restaurant"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };

                mailMessage.To.Add(to);

                await client.SendMailAsync(mailMessage);
                _logger.LogInformation($"Email sent successfully to {to}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send email to {to}");
                // Email gönderimi başarısız olsa bile uygulama çökmez
            }
        }

        public async Task SendReservationConfirmationAsync(string to, string customerName, DateTime reservationDate, int guestCount)
        {
            var subject = "Rezervasyon Onayı - Harbor Restaurant";
            var body = $@"
                <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px;'>
                    <div style='background-color: #f8f9fa; padding: 30px; border-radius: 10px;'>
                        <div style='text-align: center; margin-bottom: 30px;'>
                            <h1 style='color: #2c5aa0; margin-bottom: 10px;'>Harbor Restaurant</h1>
                            <h2 style='color: #495057; margin-bottom: 0;'>Rezervasyon Onayı</h2>
                        </div>
                        
                        <div style='background-color: white; padding: 25px; border-radius: 8px; box-shadow: 0 2px 4px rgba(0,0,0,0.1);'>
                            <p style='font-size: 16px; color: #333; margin-bottom: 20px;'>Merhaba <strong>{customerName}</strong>,</p>
                            
                            <p style='color: #666; line-height: 1.6;'>
                                Rezervasyonunuz başarıyla alınmıştır. Aşağıda rezervasyon detaylarınızı bulabilirsiniz:
                            </p>
                            
                            <div style='background-color: #e3f2fd; padding: 20px; border-radius: 6px; margin: 20px 0;'>
                                <h3 style='color: #1976d2; margin-top: 0;'>Rezervasyon Detayları</h3>
                                <p style='margin: 8px 0;'><strong>Tarih:</strong> {reservationDate:dd MMMM yyyy, dddd}</p>
                                <p style='margin: 8px 0;'><strong>Saat:</strong> {reservationDate:HH:mm}</p>
                                <p style='margin: 8px 0;'><strong>Kişi Sayısı:</strong> {guestCount}</p>
                            </div>
                            
                            <p style='color: #666; line-height: 1.6;'>
                                Size en iyi hizmeti sunmak için hazırlık yapıyoruz. Herhangi bir değişiklik yapmak istiyorsanız 
                                lütfen bizimle iletişime geçin.
                            </p>
                            
                            <div style='margin: 30px 0; padding: 20px; background-color: #fff3e0; border-radius: 6px; border-left: 4px solid #ff9800;'>
                                <h4 style='color: #e65100; margin-top: 0;'>İletişim Bilgileri</h4>
                                <p style='margin: 5px 0; color: #666;'>📞 Telefon: +90 232 123 45 67</p>
                                <p style='margin: 5px 0; color: #666;'>📧 E-posta: info@harborrestaurant.com</p>
                                <p style='margin: 5px 0; color: #666;'>📍 Adres: Atatürk Bulvarı No:123, Alsancak/İzmir</p>
                            </div>
                            
                            <p style='color: #666; text-align: center; margin-bottom: 0;'>
                                Harbor Restaurant olarak sizi ağırlamak için sabırsızlanıyoruz!
                            </p>
                        </div>
                        
                        <div style='text-align: center; margin-top: 20px; color: #999; font-size: 14px;'>
                            © 2025 Harbor Restaurant. Tüm hakları saklıdır.
                        </div>
                    </div>
                </div>";

            await SendEmailAsync(to, subject, body);
        }

        public async Task SendContactMessageNotificationAsync(string customerName, string email, string subject, string message)
        {
            var adminEmail = _configuration["EmailSettings:AdminEmail"] ?? "admin@harborrestaurant.com";
            var notificationSubject = $"Yeni İletişim Mesajı - {subject}";
            
            var body = $@"
                <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px;'>
                    <div style='background-color: #f8f9fa; padding: 20px; border-radius: 10px;'>
                        <h2 style='color: #dc3545; text-align: center;'>Yeni İletişim Mesajı</h2>
                        
                        <div style='background-color: white; padding: 20px; border-radius: 8px; margin: 20px 0;'>
                            <p><strong>Gönderen:</strong> {customerName}</p>
                            <p><strong>E-posta:</strong> {email}</p>
                            <p><strong>Konu:</strong> {subject}</p>
                            <p><strong>Gönderim Tarihi:</strong> {DateTime.Now:dd.MM.yyyy HH:mm}</p>
                            
                            <div style='border: 1px solid #dee2e6; border-radius: 5px; padding: 15px; margin-top: 15px;'>
                                <strong>Mesaj:</strong><br>
                                <div style='margin-top: 10px; line-height: 1.6; color: #333;'>
                                    {message.Replace("\n", "<br>")}
                                </div>
                            </div>
                        </div>
                        
                        <div style='text-align: center; margin-top: 20px;'>
                            <p style='color: #666; font-size: 14px;'>
                                Bu mesajı admin panelinden yanıtlayabilirsiniz.
                            </p>
                        </div>
                    </div>
                </div>";

            await SendEmailAsync(adminEmail, notificationSubject, body);
        }

        public async Task SendWelcomeEmailAsync(string to, string userName)
        {
            var subject = "Harbor Restaurant'a Hoş Geldiniz!";
            var body = $@"
                <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px;'>
                    <div style='background-color: #f8f9fa; padding: 30px; border-radius: 10px;'>
                        <div style='text-align: center; margin-bottom: 30px;'>
                            <h1 style='color: #2c5aa0;'>Harbor Restaurant</h1>
                            <h2 style='color: #495057;'>Hoş Geldiniz!</h2>
                        </div>
                        
                        <div style='background-color: white; padding: 25px; border-radius: 8px;'>
                            <p style='font-size: 16px; color: #333;'>Merhaba <strong>{userName}</strong>,</p>
                            
                            <p style='color: #666; line-height: 1.6;'>
                                Harbor Restaurant ailesine katıldığınız için teşekkür ederiz! 
                                Artık lezzetli yemeklerimizi keşfedebilir ve kolay rezervasyon yapabilirsiniz.
                            </p>
                            
                            <div style='background-color: #e8f5e8; padding: 20px; border-radius: 6px; margin: 20px 0;'>
                                <h3 style='color: #2e7d32; margin-top: 0;'>Neler Yapabilirsiniz?</h3>
                                <ul style='color: #333; line-height: 1.8;'>
                                    <li>🍽️ Zengin menümüzü keşfedin</li>
                                    <li>📅 Online rezervasyon yapın</li>
                                    <li>📞 Bizimle kolayca iletişime geçin</li>
                                    <li>📰 Blog yazılarımızı okuyun</li>
                                </ul>
                            </div>
                            
                            <div style='text-align: center; margin: 30px 0;'>
                                <a href='{_configuration["BaseUrl"]}/Menu' style='background-color: #2c5aa0; color: white; padding: 12px 30px; text-decoration: none; border-radius: 5px; display: inline-block;'>
                                    Menümüzü İnceleyin
                                </a>
                            </div>
                            
                            <p style='color: #666; text-align: center;'>
                                Harbor Restaurant olarak size en iyi deneyimi sunmak için buradayız!
                            </p>
                        </div>
                    </div>
                </div>";

            await SendEmailAsync(to, subject, body);
        }
    }
}
