using College_API.Models;
using College_API.ViewModels;

namespace College_API.Interfaces
{
    public interface ICourseRepository
    {
        public Task AddCourseAsync(PostCourseViewModel model);
        public Task<List<CourseViewModel>> ListAllCourseAsync();
        public Task<CourseViewModel> GetCourseAsync(int id);
        // public Task<CourseViewModel?> GetCourseAsync(string CourseName);
        // public Task<CourseViewModel?> GetCourseNumberAsync(string CourseNumber);
        Task UpdateCourseAsync(int courseId, PutCourseViewModel courseViewModel);
        public Task UpdateCourseAsync(Course course);
        // public Task UpdateCourseAsync(int id, PatchCourseViewModel model);
        public Task DeleteCourseAsync(int id);
        public Task<bool> SaveAllAsync();
    }
}