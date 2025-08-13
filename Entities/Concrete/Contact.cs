using System.ComponentModel.DataAnnotations;

namespace HarborRestaurant.Entities.Concrete
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }

        [Required(ErrorMessage = "Adres gereklidir")]
        [MaxLength(500, ErrorMessage = "Adres en fazla 500 karakter olabilir")]
        public string Address { get; set; } = string.Empty;

        [MaxLength(500, ErrorMessage = "İngilizce adres en fazla 500 karakter olabilir")]
        public string? AddressEn { get; set; }

        [Required(ErrorMessage = "Telefon numarası gereklidir")]
        [MaxLength(20, ErrorMessage = "Telefon numarası en fazla 20 karakter olabilir")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-posta adresi gereklidir")]
        [MaxLength(100, ErrorMessage = "E-posta adresi en fazla 100 karakter olabilir")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        public string Email { get; set; } = string.Empty;

        [MaxLength(100, ErrorMessage = "Çalışma saatleri en fazla 100 karakter olabilir")]
        public string? WorkingHours { get; set; }

        [MaxLength(100, ErrorMessage = "İngilizce çalışma saatleri en fazla 100 karakter olabilir")]
        public string? WorkingHoursEn { get; set; }

        [MaxLength(200, ErrorMessage = "Harita URL en fazla 200 karakter olabilir")]
        public string? MapUrl { get; set; }

        [MaxLength(50, ErrorMessage = "Harita enlemi en fazla 50 karakter olabilir")]
        public string? MapLatitude { get; set; }

        [MaxLength(50, ErrorMessage = "Harita boylamı en fazla 50 karakter olabilir")]
        public string? MapLongitude { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
    }
}
