using System.ComponentModel.DataAnnotations;

namespace HarborRestaurant.Entities.Concrete
{
    public class About
    {
        [Key]
        public int AboutId { get; set; }

        [Required(ErrorMessage = "Başlık gereklidir")]
        [MaxLength(200, ErrorMessage = "Başlık en fazla 200 karakter olabilir")]
        public string Title { get; set; } = string.Empty;

        [MaxLength(200, ErrorMessage = "İngilizce başlık en fazla 200 karakter olabilir")]
        public string? TitleEn { get; set; }

        [Required(ErrorMessage = "Alt başlık gereklidir")]
        [MaxLength(300, ErrorMessage = "Alt başlık en fazla 300 karakter olabilir")]
        public string Subtitle { get; set; } = string.Empty;

        [MaxLength(300, ErrorMessage = "İngilizce alt başlık en fazla 300 karakter olabilir")]
        public string? SubtitleEn { get; set; }

        [Required(ErrorMessage = "Açıklama gereklidir")]
        public string Description { get; set; } = string.Empty;

        public string? DescriptionEn { get; set; }

        [MaxLength(200, ErrorMessage = "Resim yolu en fazla 200 karakter olabilir")]
        public string? ImageUrl { get; set; }

        [MaxLength(200, ErrorMessage = "Video URL en fazla 200 karakter olabilir")]
        public string? VideoUrl { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
    }
}
