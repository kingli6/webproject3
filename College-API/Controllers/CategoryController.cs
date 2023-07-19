using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using College_API.CustomExceptions;
using College_API.Interfaces;
using College_API.Models;
using College_API.ViewModels.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace College_API.Controllers
{
    [ApiController]
    [Route("api/v3/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoryController(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        [HttpGet("GetAllCategories")]
        public async Task<ActionResult<List<Category>>> ListAllCategory()
        {
            try
            {
                return Ok(await _categoryRepo.ListAllCategoryAsync());
            }
            catch (NotFoundException)
            {
                return StatusCode(404, "Tabel doesn't exsist");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                return Ok(await _categoryRepo.GetCategoryAsync(id));
            }
            catch (NotFoundException)
            {
                return StatusCode(404, $"Category with id = {id} doesn't exist");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("AddCategory")]
        public async Task<ActionResult<Category>> AddCategory(PostCategoryViewModel model)
        {
            try
            {
                await _categoryRepo.AddCategoryAsync(model);

                if (await _categoryRepo.SaveAllAsync())
                {
                    return StatusCode(201);
                }

                return StatusCode(500, "Failed to save category");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("ReplaceCategory/{id}")]
        public async Task<IActionResult> ReplaceCategory(int id) //testing IActionResult
        {
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)// //testing IActionResult
        {
            try
            {
                await _categoryRepo.DeleteCategoryAsync(id);

                if (await _categoryRepo.SaveAllAsync())
                {
                    return NoContent();
                }

                return StatusCode(500, $"Failed to delete category (id = {id})");
            }
            catch (NotFoundException)
            {
                return StatusCode(404, $"Category with id = {id} doesn't exist");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}