using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using College_API.Models;
using College_API.ViewModels;

namespace College_API.Interfaces
{
    public interface IUserRepository
    {
        public Task<List<UserViewModel>> ListAllUsersAsync();
        public Task<UserViewModel?> GetUserAsync(int id);
        public Task<UserViewModel?> GetUserAsync(string userName);
        public Task<UserViewModel?> GetUserEmailAsync(string userEmail);
        public Task AddUserAsync(PostUserViewModel model);
        public Task UpdateUser(int id, PostUserViewModel model);
        public Task DeleteUser(int id);
        public Task<bool> SaveAllAsync();
    }
}