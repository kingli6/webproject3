using AutoMapper;
using AutoMapper.QueryableExtensions;
using College_API.CustomExceptions;
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

        public async Task<CourseViewModel> GetCourseAsync(int id)
        {
            return await _context.Courses.Where(c => c.Id == id)
            .ProjectTo<CourseViewModel>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
        }

        public Task<CourseViewModel?> GetCourseAsync(string CourseName)
        {
            throw new NotImplementedException();
        }

        public Task<CourseViewModel?> GetCourseNumberAsync(string CourseNumber)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateCourseAsync(int id, PostCourseViewModel model)
        {
            if (model is null)
                throw new BadRequestException();

            var response = await _context.Courses.FindAsync(id) ?? throw new NotFoundException();

            response.CourseNumber = model.CourseNumber!;
            response.Name = model.Name;
            response.Duration = model.Duration;
            response.Description = model.Description;
            response.Details = model.Details;
            _context.Courses.Update(response);
        }

        public Task UpdateCourseAsync(int id, PatchCourseViewModel model)
        {
            throw new NotImplementedException();
        }
        public async Task DeleteCourseAsync(int id)
        {
            var response = await _context.Courses.FindAsync(id);
            if (response == null)
                throw new NotFoundException();

            _context.Courses.Remove(response);
        }
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}