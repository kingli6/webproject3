using System.ComponentModel.DataAnnotations;
using College_API.Models;

namespace College_API.ViewModels.RegisterUserViewModel
{
    public class UserViewModel : User
    {
        public string? Id { get; set; }
        public string? Username { get; set; }

        [EmailAddress(ErrorMessage = "Felaktig e-postadress")]
        public string? Email { get; set; }

        public DateTime Expires { get; set; }
        public string? Token { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
}