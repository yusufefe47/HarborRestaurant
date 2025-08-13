using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HarborRestaurant.Entities.Concrete
{
    public class MenuCategory
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Kategori adı gereklidir")]
        [MaxLength(100, ErrorMessage = "Kategori adı en fazla 100 karakter olabilir")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(300, ErrorMessage = "Açıklama en fazla 300 karakter olabilir")]
        public string? Description { get; set; }

        [MaxLength(200, ErrorMessage = "Resim URL en fazla 200 karakter olabilir")]
        public string? ImageUrl { get; set; }

        public int SortOrder { get; set; } = 0;
        public int DisplayOrder { get; set; } = 0;

        [Required]
        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }

        // Navigation Properties
        public virtual ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
    }
}
