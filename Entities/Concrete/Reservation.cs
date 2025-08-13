using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HarborRestaurant.Entities.Enums;

namespace HarborRestaurant.Entities.Concrete
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }

        [Required(ErrorMessage = "Ad Soyad gereklidir")]
        [MaxLength(100, ErrorMessage = "Ad Soyad en fazla 100 karakter olabilir")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-posta adresi gereklidir")]
        [MaxLength(100, ErrorMessage = "E-posta adresi en fazla 100 karakter olabilir")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Telefon numarası gereklidir")]
        [MaxLength(20, ErrorMessage = "Telefon numarası en fazla 20 karakter olabilir")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kişi sayısı gereklidir")]
        [Range(1, 20, ErrorMessage = "Kişi sayısı 1 ile 20 arasında olmalıdır")]
        public int GuestCount { get; set; }

        [Required(ErrorMessage = "Giriş tarihi gereklidir")]
        public DateTime CheckInDate { get; set; }

        [Required(ErrorMessage = "Çıkış tarihi gereklidir")]
        public DateTime CheckOutDate { get; set; }

        [Required(ErrorMessage = "Rezervasyon saati gereklidir")]
        public TimeOnly ReservationTime { get; set; }

        [MaxLength(500, ErrorMessage = "Özel istek en fazla 500 karakter olabilir")]
        public string? SpecialRequests { get; set; }

    // Masa tercihi kullanıcı formundan kaldırıldı; alan opsiyonel kalmaya devam ediyor
    public int? TableId { get; set; }

    // Seçilen salon (opsiyonel)
    public int? RoomId { get; set; }

        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }

        [MaxLength(500, ErrorMessage = "Admin notu en fazla 500 karakter olabilir")]
        public string? AdminNotes { get; set; }

        [MaxLength(50, ErrorMessage = "IP adresi en fazla 50 karakter olabilir")]
        public string? IpAddress { get; set; }

        // Navigation Properties
    [ForeignKey("TableId")]
    public virtual Table? Table { get; set; }

    [ForeignKey("RoomId")]
    public virtual Room? Room { get; set; }
    }
}
