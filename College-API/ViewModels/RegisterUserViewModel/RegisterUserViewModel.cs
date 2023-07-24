using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace College_API.ViewModels.RegisterUserViewModel
{
    public class RegisterUserViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Wrong e-post adress")]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}