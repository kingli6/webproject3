using System.ComponentModel.DataAnnotations;

namespace College_API.ViewModels
{
    public class PostUserViewModel
    {
        // [Required]  //follow this by if(!ModelState.IsValid){}
        [Required(ErrorMessage = "FirstName is required")]  //220503_09 35:30
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
    }
}