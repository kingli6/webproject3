using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using College_API.ViewModels;
using College_API.ViewModels.RegisterUserViewModel;
using Microsoft.AspNetCore.Mvc;

namespace College_API.Interfaces
{
    public interface IAdminRepository
    {
        public Task<IActionResult> CreateRoleAsync(string roleName);

        public Task<ActionResult<ViewModels.UserViewModel>> RegisterAdminAsync(RegisterUserViewModel model);

        // public Task<ActionResult<UserViewModel>> LoginAsync(LoginViewModel model);

        // public Task<IActionResult> LogoutAsync();

        // Task<string> CreateJwtToken(User user);

        // public Task<List<UserViewModel>> GetAllAdminsAsync();

        // public Task<List<UserViewModel>> GetAllUsersAsync();

        // public Task<UserViewModel> GetAdminAsync(string id);

        // public Task<UserViewModel> GetUserAsync(string id);

        // public Task ReplaceUserAsync(string id, PutUserViewModel model);
        // public Task DeleteUserAsync(string id);

        // public Task<IActionResult> ConfirmEmailAsync(string userId, string token);
    }
}