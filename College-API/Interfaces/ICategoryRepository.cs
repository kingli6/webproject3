using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using College_API.ViewModels.Category;

namespace College_API.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<List<CategoryViewModel>> ListAllCategoryAsync();
        public Task<CategoryViewModel?> GetCategoryAsync(int id);
        // public Task<CategoryViewModel?> GetCategoryAsync(string CategoryName);
        public Task AddCategoryAsync(PostCategoryViewModel model/*, int courseID*/);
        public Task UpdateCategoryAsync(int id, PostCategoryViewModel model);
        public Task UpdateCategoryAsync(int id, PutCategoryViewModel model);
        public Task DeleteCategoryAsync(int id);
        Task<bool> SaveAllAsync();
    }
}