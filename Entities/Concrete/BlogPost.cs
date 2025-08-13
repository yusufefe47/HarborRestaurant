using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HarborRestaurant.Entities.Concrete
{
    public class BlogPost
    {
        [Key]
        public int PostId { get; set; }

        [Required(ErrorMessage = "Başlık gereklidir")]
        [MaxLength(200, ErrorMessage = "Başlık en fazla 200 karakter olabilir")]
        public string Title { get; set; } = string.Empty;

        [MaxLength(200, ErrorMessage = "İngilizce başlık en fazla 200 karakter olabilir")]
        public string? TitleEn { get; set; }

        [MaxLength(300, ErrorMessage = "Kısa açıklama en fazla 300 karakter olabilir")]
        public string? Summary { get; set; }

        [MaxLength(300, ErrorMessage = "İngilizce kısa açıklama en fazla 300 karakter olabilir")]
        public string? SummaryEn { get; set; }

        [Required(ErrorMessage = "İçerik gereklidir")]
        public string Content { get; set; } = string.Empty;

        public string? ContentEn { get; set; }

        [MaxLength(200, ErrorMessage = "Resim URL en fazla 200 karakter olabilir")]
        public string? ImageUrl { get; set; }

        public int CategoryId { get; set; }

        [MaxLength(200, ErrorMessage = "Yazar adı en fazla 200 karakter olabilir")]
        public string? Author { get; set; }

        public int ViewCount { get; set; } = 0;

    public bool IsActive { get; set; } = true;
    public bool IsPublished { get; set; } = false;

    public bool IsFeatured { get; set; } = false; // Öne çıkan yazılar

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? PublishedDate { get; set; }
        public DateTime PublishDate { get; set; } = DateTime.Now;

        // SEO alanları
        [MaxLength(200, ErrorMessage = "Meta başlık en fazla 200 karakter olabilir")]
        public string? MetaTitle { get; set; }

        [MaxLength(300, ErrorMessage = "Meta açıklama en fazla 300 karakter olabilir")]
        public string? MetaDescription { get; set; }

        // Navigation Properties
        [ForeignKey("CategoryId")]
        public virtual BlogCategory? Category { get; set; }
    }
}
