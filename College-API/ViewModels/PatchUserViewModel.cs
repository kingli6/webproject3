using System.ComponentModel.DataAnnotations;

namespace College_API.ViewModels
{
    public class PatchUserViewModel
    {
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? Address { get; set; }
    }
}