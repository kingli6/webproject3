using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using College_API.Models;
using College_API.ViewModels.Category;

namespace College_API.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<List<Category>> GetAllCategoriesAsync();
        public Task AddCategoryAsync(Category category);
        public Task<List<CategoryViewModel>> ListAllCategoryAsync();
        public Task<Category> GetCategoryAsync(int id);
        public Task DeleteCategoryAsync(Category category);
        public Task<CategoryViewModel?> GetCategoryAsyncX(int id);
        // public Task<CategoryViewModel?> GetCategoryAsync(string CategoryName);
        public Task AddCategoryAsyncX(PostCategoryViewModel model/*, int courseID*/);
        public Task UpdateCategoryAsync(int id, PostCategoryViewModel model);
        public Task UpdateCategoryAsync(int id, PutCategoryViewModel model);
        public Task DeleteCategoryAsync(int id);
        Task<bool> SaveAllAsync();
    }
}