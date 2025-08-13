using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HarborRestaurant.Entities.Concrete
{
    public class AppUser : IdentityUser
    {
        [MaxLength(100, ErrorMessage = "Ad en fazla 100 karakter olabilir")]
        public string? FirstName { get; set; }

        [MaxLength(100, ErrorMessage = "Soyad en fazla 100 karakter olabilir")]
        public string? LastName { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? LastLoginDate { get; set; }
        public bool IsActive { get; set; } = true;

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}".Trim();
    }
}
