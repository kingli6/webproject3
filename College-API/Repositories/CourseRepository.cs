using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using College_API.Data;
using College_API.Interfaces;
using College_API.ViewModels;

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

        public Task AddCourseAsync(PostCourseViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<List<CourseViewModel>> ListAllCourseAsync()
        {
            throw new NotImplementedException();
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
        public Task<bool> SaveAllAsync()
        {
            throw new NotImplementedException();
        }

    }
}