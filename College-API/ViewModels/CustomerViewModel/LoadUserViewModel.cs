using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace College_API.ViewModels.CustomerViewModel
{
    public class LoadUserViewModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public List<string>? UserRole { get; set; }
    }
}