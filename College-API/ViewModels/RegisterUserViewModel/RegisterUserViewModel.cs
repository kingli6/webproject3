using System.ComponentModel.DataAnnotations;

namespace College_API.ViewModels.RegisterUserViewModel
{
    public class RegisterUserViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Wrong e-post adress")]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        public bool IsAdmin { get; set; } = false;
    }
}