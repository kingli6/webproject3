using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using College_API.Data;
using College_API.Interfaces;
using College_API.Models;
using College_API.ViewModels.Category;
using Microsoft.EntityFrameworkCore;
using College_API.CustomExceptions;

namespace College_API.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CollegeDatabaseContext _context;
        private readonly IMapper _mapper;
        public CategoryRepository(CollegeDatabaseContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }
        public async Task AddCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }
        public async Task<Category> GetCategoryAsync(int id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (category == null)
                {
                    throw new NotFoundException($"Category with ID {id} not found.");
                }
                return category;
            }
            catch (Exception ex)
            {
                //Log stuff?
                throw new ApplicationException("An error occurred while fetching the category.", ex);
            }
        }
        public async Task DeleteCategoryAsync(Category category)
        {
            try
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle database errors or other exceptions
                throw new ApplicationException("Failed to delete category.", ex);
            }
        }
        public async Task AddCategoryAsyncX(PostCategoryViewModel model/*, int courseID*/)   //or Course name?
        {// this is one to many? Course can have many categories. But Course is not included in categories props, so I should be simply able to add it...
            await _context.Categories.AddAsync(_mapper.Map<Category>(model));

            // var course = await _context.Courses.Include(c => c.Category)
            // .Where(c => c.Id == courseID)
            // .SingleOrDefaultAsync();
            // if (course is null)
            //     throw new Exception($"We couldn't find the courseID: {courseID}");
            // var categoryToAdd = _mapper.Map<Category>(model);
            // categoryToAdd.Courses.Add(course);

            // await _context.Categories.AddAsync(categoryToAdd);
            // //I need to already have a course to be able to create a category... which feels wrong...
            // throw new NotImplementedException();

        }


        public async Task<List<CategoryViewModel>> ListAllCategoryAsync()
        {
            return await _context.Categories.ProjectTo<CategoryViewModel>(_mapper.ConfigurationProvider)
            .OrderBy(c => c.CategoryId).ToListAsync();

        }
        public async Task<CategoryViewModel?> GetCategoryAsyncX(int id)
        {
            var response = await _context.Categories.FindAsync(id);
            if (response == null)
                throw new NotFoundException();

            return _mapper.Map<CategoryViewModel>(response);
            // return await _context.Categories.Where(c => c.Id == id)
            //     .ProjectTo<CategoryViewModel>(_mapper.ConfigurationProvider)
            //     .SingleOrDefaultAsync();
        }
        // public async Task<CategoryViewModel?> GetCategoryAsync(string CategoryName)
        // {
        //     throw new NotImplementedException();
        // }

        public async Task UpdateCategoryAsync(int id, PostCategoryViewModel model)
        {
            if (model is null)
                throw new BadRequestException();
            var response = await _context.Categories.FindAsync(id);
            if (response is null)
                throw new NotFoundException();

            response.CategoryName = model.CategoryName;
            _context.Categories.Update(response);
        }

        public async Task UpdateCategoryAsync(int id, PutCategoryViewModel model)
        {
            var response = await _context.Categories.FindAsync(id);

            if (response == null)
                throw new NotFoundException();

            _context.Categories.Update(_mapper.Map(model, response));
        }
        public async Task DeleteCategoryAsync(int id)
        {
            var response = await _context.Categories.FindAsync(id);

            if (response == null)
                throw new NotFoundException();

            _context.Categories.Remove(response);
        }
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}