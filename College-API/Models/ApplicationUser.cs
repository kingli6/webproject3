using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace College_API.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string? FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string? LastName { get; set; }
        [MaxLength(100)]
        public string? Address { get; set; }
        [Required]
        [RegularExpression("^(Administrator|User|Teacher)$", ErrorMessage = "Invalid UserRole")]
        public ICollection<IdentityUserRole<string>> UserRoles { get; } = new List<IdentityUserRole<string>>();
    }
}