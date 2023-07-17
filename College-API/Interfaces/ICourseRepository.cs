using College_API.ViewModels;

namespace College_API.Interfaces
{
    public interface ICourseRepository
    {
        public Task<List<CourseViewModel>> ListAllCourseAsync();
        public Task<CourseViewModel> GetCourseAsync(int id);
        public Task<CourseViewModel?> GetCourseAsync(string CourseName);
        public Task<CourseViewModel?> GetCourseNumberAsync(string CourseNumber);
        public Task AddCourseAsync(PostCourseViewModel model);
        public Task UpdateCourseAsync(int id, PostCourseViewModel model);
        public Task UpdateCourseAsync(int id, PatchCourseViewModel model);
        public Task DeleteCourseAsync(int id);
        public Task<bool> SaveAllAsync();
    }
}