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
            var course = new Course
            {
                CourseNumber = model.CourseNumber,
                Name = model.Name,
                Duration = model.Duration,
                Description = model.Description,
                Details = model.Details,
                CategoryId = model.CategoryId, // Set the CategoryId
                                               // ... other properties ...
            };
            // var course = _mapper.Map<Course>(model);
            await _context.Courses.AddAsync(course);
        }

        public async Task<List<CourseViewModel>> ListAllCourseAsync()
        {
            return await _context.Courses.ProjectTo<CourseViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<CourseViewModel?> GetCourseAsync(int id)
        {
            // ?? not sure if this includes the registrations... 
            //??? How do I add a registration?
            return await _context.Courses.Where(c => c.Id == id).Include(c => c.Registrations)
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
        public async Task UpdateCourseAsync(int courseId, PutCourseViewModel courseViewModel)
        {
            var existingCourse = await _context.Courses.FindAsync(courseId) ?? throw new NotFoundException($"Course with id {courseId} not found.");

            // Map properties from the view model to the existing course entity
            existingCourse.CourseNumber = courseViewModel.CourseNumber;
            existingCourse.Name = courseViewModel.Name;
            existingCourse.Duration = courseViewModel.Duration;
            existingCourse.Description = courseViewModel.Description;
            existingCourse.Details = courseViewModel.Details;
            existingCourse.CategoryId = courseViewModel.CategoryId;
            existingCourse.EnrolledStudents = courseViewModel.EnrolledStudents;

            _context.Courses.Update(existingCourse); // Attach the updated course to the DbContext for updating
            await _context.SaveChangesAsync(); // Save the changes to the database
        }
        public async Task UpdateCourseAsync(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();

        }

        public Task UpdateCourseAsync(int id, PatchCourseViewModel model)
        {
            throw new NotImplementedException();
        }
        public async Task DeleteCourseAsync(int id)
        {

            var course = await _context.Courses.Include(c => c.Registrations).FirstOrDefaultAsync(c => c.Id == id);
            if (course == null)
                throw new NotFoundException();
            // ?? Not sure if this works...
            _context.Registrations.RemoveRange(course.Registrations);
            _context.Courses.Remove(course);

        }
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}