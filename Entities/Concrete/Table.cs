using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HarborRestaurant.Entities.Concrete
{
    public class Table
    {
        [Key]
        public int TableId { get; set; }

        [Required(ErrorMessage = "Masa numarası gereklidir")]
        [MaxLength(10, ErrorMessage = "Masa numarası en fazla 10 karakter olabilir")]
        public string TableNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kapasite gereklidir")]
        [Range(1, 20, ErrorMessage = "Kapasite 1 ile 20 arasında olmalıdır")]
        public int Capacity { get; set; }

        [MaxLength(200, ErrorMessage = "Konum açıklaması en fazla 200 karakter olabilir")]
        public string? Location { get; set; } // Pencere kenarı, terasta vb.

        [Required]
        public bool IsActive { get; set; } = true;

        [Required]
        public bool IsAvailable { get; set; } = true; // Masa müsait mi?

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }

        // Navigation Properties
        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
