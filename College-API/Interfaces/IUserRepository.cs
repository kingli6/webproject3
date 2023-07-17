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
        public Task UpdateUserAsync(int id, PostUserViewModel model);
        public Task UpdateUserAsync(int id, PatchUserViewModel model);
        public Task DeleteUserAsync(int id);
        public Task<bool> SaveAllAsync();
    }
}