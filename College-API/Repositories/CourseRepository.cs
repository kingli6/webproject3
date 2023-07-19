using AutoMapper;
using AutoMapper.QueryableExtensions;
using College_API.Data;
using College_API.Interfaces;
using College_API.Models;
using College_API.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace College_API.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly CollegeDatabaseContext _context;
        private readonly IMapper _mapper;
        public CourseRepository(CollegeDatabaseContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task AddCourseAsync(PostCourseViewModel model)
        {
            var course = _mapper.Map<Course>(model);
            await _context.Courses.AddAsync(course);
        }

        public async Task<List<CourseViewModel>> ListAllCourseAsync()
        {
            return await _context.Courses.ProjectTo<CourseViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public Task<CourseViewModel> GetCourseAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CourseViewModel?> GetCourseAsync(string CourseName)
        {
            throw new NotImplementedException();
        }

        public Task<CourseViewModel?> GetCourseNumberAsync(string CourseNumber)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCourseAsync(int id, PostCourseViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCourseAsync(int id, PatchCourseViewModel model)
        {
            throw new NotImplementedException();
        }
        public Task DeleteCourseAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}