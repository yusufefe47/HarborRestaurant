using System.ComponentModel.DataAnnotations;

namespace HarborRestaurant.Entities.Concrete
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(100)]
        public string? NameEn { get; set; }
        
        [Required]
        public string Description { get; set; } = string.Empty;
        
        public string? DescriptionEn { get; set; }
        
        [Range(1, 100)]
        public int Capacity { get; set; }
        
        [Range(0.01, 10000)]
        public decimal? MinimumOrderAmount { get; set; }
        
        public string? ImageUrl { get; set; }
        
        public string? Features { get; set; }
        
        [Range(1, 5)]
        public int StarRating { get; set; } = 5;
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedDate { get; set; }
        
        public DateTime? UpdatedDate { get; set; }
        
        public int SortOrder { get; set; }
    }
}
