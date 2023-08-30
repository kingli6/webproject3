using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace College_API.ViewModels.AuthViewModels
{
    public class UserProfileUpdateModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
    }
}