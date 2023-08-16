using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace College_API.ViewModels.RegisterUserViewModel
{
    public class PutSignInUserViewModel
    {
        public string? Username { get; set; }

        [EmailAddress(ErrorMessage = "Felaktig e-postadress")]
        public string? Email { get; set; }
    }
}