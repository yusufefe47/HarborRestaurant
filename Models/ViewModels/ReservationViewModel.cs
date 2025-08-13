using System.ComponentModel.DataAnnotations;

namespace HarborRestaurant.Models.ViewModels
{
    public class ReservationViewModel
    {
        [Required(ErrorMessage = "Adınız soyadınız gereklidir")]
        [Display(Name = "Ad Soyad")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-posta adresi gereklidir")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi girin")]
        [Display(Name = "E-posta")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Telefon numarası gereklidir")]
        [Display(Name = "Telefon")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kişi sayısı seçmelisiniz")]
        [Range(1, 20, ErrorMessage = "Kişi sayısı 1 ile 20 arasında olmalıdır")]
        [Display(Name = "Kişi Sayısı")]
        public int GuestCount { get; set; }

        [Required(ErrorMessage = "Giriş tarihi gereklidir")]
        [Display(Name = "Giriş Tarihi")]
        public string CheckInDate { get; set; } = string.Empty;

        [Required(ErrorMessage = "Çıkış tarihi gereklidir")]
        [Display(Name = "Çıkış Tarihi")]
        public string CheckOutDate { get; set; } = string.Empty;

        // Backward compatibility için eski alan
        [Display(Name = "Rezervasyon Tarihi")]
        public string ReservationDate { get; set; } = string.Empty;

        [Required(ErrorMessage = "Rezervasyon saati gereklidir")]
        [Display(Name = "Rezervasyon Saati")]
        public string ReservationTime { get; set; } = string.Empty;

        [Display(Name = "Özel İstek")]
        public string? SpecialRequest { get; set; }

    // Masa tercihi kaldırıldı
    // public int? TableId { get; set; }

    [Display(Name = "Salon")]
    public int? RoomId { get; set; }

    // Sadece görüntüleme için
    public string? RoomName { get; set; }
    }
}
