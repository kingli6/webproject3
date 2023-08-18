using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace College_API.ViewModels.AuthViewModels
{
    public class UserWithRolesViewModel
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public List<string> Roles { get; set; }
    }
}