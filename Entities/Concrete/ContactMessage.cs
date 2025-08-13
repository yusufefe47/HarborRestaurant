using System.ComponentModel.DataAnnotations;

namespace HarborRestaurant.Entities.Concrete
{
    public class ContactMessage
    {
        [Key]
        public int ContactMessageId { get; set; }

        [Required(ErrorMessage = "Ad Soyad gereklidir")]
        [MaxLength(100, ErrorMessage = "Ad Soyad en fazla 100 karakter olabilir")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-posta adresi gereklidir")]
        [MaxLength(100, ErrorMessage = "E-posta adresi en fazla 100 karakter olabilir")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        public string Email { get; set; } = string.Empty;

        [MaxLength(20, ErrorMessage = "Telefon numarası en fazla 20 karakter olabilir")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Konu gereklidir")]
        [MaxLength(200, ErrorMessage = "Konu en fazla 200 karakter olabilir")]
        public string Subject { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mesaj gereklidir")]
        [MaxLength(1000, ErrorMessage = "Mesaj en fazla 1000 karakter olabilir")]
        public string Message { get; set; } = string.Empty;

        public bool IsRead { get; set; } = false;
        public DateTime SentDate { get; set; } = DateTime.Now;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ReadDate { get; set; }
        public bool IsReplied { get; set; } = false;
        public DateTime? ReplyDate { get; set; }

        [MaxLength(50, ErrorMessage = "IP adresi en fazla 50 karakter olabilir")]
        public string? IpAddress { get; set; }
    }
}
