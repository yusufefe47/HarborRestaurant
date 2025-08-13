using System.ComponentModel.DataAnnotations;

namespace HarborRestaurant.Entities.Concrete
{
    public class HomePage
    {
        [Key]
        public int HomePageId { get; set; }

        [Required(ErrorMessage = "Ana başlık gereklidir")]
        [MaxLength(200, ErrorMessage = "Ana başlık en fazla 200 karakter olabilir")]
        public string MainTitle { get; set; } = string.Empty;

        [MaxLength(200, ErrorMessage = "İngilizce ana başlık en fazla 200 karakter olabilir")]
        public string? MainTitleEn { get; set; }

        [Required(ErrorMessage = "Alt başlık gereklidir")]
        [MaxLength(300, ErrorMessage = "Alt başlık en fazla 300 karakter olabilir")]
        public string Subtitle { get; set; } = string.Empty;

        [MaxLength(300, ErrorMessage = "İngilizce alt başlık en fazla 300 karakter olabilir")]
        public string? SubtitleEn { get; set; }

        [MaxLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir")]
        public string? Description { get; set; }

        [MaxLength(500, ErrorMessage = "İngilizce açıklama en fazla 500 karakter olabilir")]
        public string? DescriptionEn { get; set; }

        [MaxLength(200, ErrorMessage = "Ana resim URL en fazla 200 karakter olabilir")]
        public string? HeroImageUrl { get; set; }

        [MaxLength(200, ErrorMessage = "Buton metni en fazla 200 karakter olabilir")]
        public string? ButtonText { get; set; }

        [MaxLength(200, ErrorMessage = "İngilizce buton metni en fazla 200 karakter olabilir")]
        public string? ButtonTextEn { get; set; }

        [MaxLength(200, ErrorMessage = "Buton linki en fazla 200 karakter olabilir")]
        public string? ButtonUrl { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
    }
}
