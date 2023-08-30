using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace College_API.ViewModels.UserCustomerViewModel
{
    public class SignInCustomerViewModel
    {
        // public string? Id { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Wrong e-post adress")]
        public string? Email { get; set; }
        //public string? UserName { get; set; } // username will be email?
        [Required]
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public List<string>? UserRole { get; set; }
    }
}