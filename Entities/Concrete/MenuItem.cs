using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HarborRestaurant.Entities.Concrete
{
    public class MenuItem
    {
        [Key]
        public int ItemId { get; set; }

        [Required(ErrorMessage = "Ürün adı gereklidir")]
        [MaxLength(100, ErrorMessage = "Ürün adı en fazla 100 karakter olabilir")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100, ErrorMessage = "İngilizce ürün adı en fazla 100 karakter olabilir")]
        public string? NameEn { get; set; }

        [MaxLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir")]
        public string? Description { get; set; }

        [MaxLength(500, ErrorMessage = "İngilizce açıklama en fazla 500 karakter olabilir")]
        public string? DescriptionEn { get; set; }

        [Required(ErrorMessage = "Fiyat gereklidir")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [MaxLength(200, ErrorMessage = "Resim URL en fazla 200 karakter olabilir")]
        public string? ImageUrl { get; set; }

        [MaxLength(300, ErrorMessage = "İçindekiler en fazla 300 karakter olabilir")]
        public string? Ingredients { get; set; }

        public int? PreparationTime { get; set; } // Dakika cinsinden

        public int? Calories { get; set; }

        [Required(ErrorMessage = "Kategori seçimi gereklidir")]
        public int CategoryId { get; set; }

        public int SortOrder { get; set; } = 0;

        [Required]
        public bool IsActive { get; set; } = true;

        [Required]
        public bool IsAvailable { get; set; } = true; // Stokta var mı?

        [Required]
        public bool IsFeatured { get; set; } = false; // Öne çıkan ürünler için

        [Required]
        public bool IsSpecial { get; set; } = false; // Özel ürünler için

        [Required]
        public bool IsSpicy { get; set; } = false; // Acı mı?

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }

        // Navigation Properties
        [ForeignKey("CategoryId")]
        public virtual MenuCategory? Category { get; set; }
    }
}
