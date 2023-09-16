// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using College_API.CustomExceptions;
// using College_API.Interfaces;
// using College_API.Models;
// using College_API.ViewModels.Category;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;

// namespace College_API.Controllers
// {
//     [ApiController]
//     [Route("api/v3/categories")]
//     public class CategoryController : ControllerBase
//     {
//         private readonly ICategoryRepository _categoryRepo;
//         public CategoryController(ICategoryRepository categoryRepo)
//         {
//             _categoryRepo = categoryRepo;
//         }

//         [HttpGet("GetAllCategories")]
//         public async Task<ActionResult<List<Category>>> GetAllCategories()
//         {
//             try
//             {
//                 var categories = await _categoryRepo.GetAllCategoriesAsync();
//                 return Ok(categories);
//             }
//             catch (Exception ex)
//             {
//                 return StatusCode(500, ex.Message);
//             }
//         }
//         // public async Task<ActionResult<List<Category>>> ListAllCategory()
//         // {
//         //     try
//         //     {
//         //         return Ok(await _categoryRepo.ListAllCategoryAsync());
//         //     }
//         //     catch (NotFoundException)
//         //     {
//         //         return StatusCode(404, "Tabel doesn't exsist");
//         //     }
//         //     catch (Exception ex)
//         //     {
//         //         return StatusCode(500, ex.Message);
//         //     }
//         // }
//         [AllowAnonymous]
//         [HttpGet("{id}")]
//         public async Task<ActionResult<Category>> GetCategoryById(int id)
//         {
//             try
//             {
//                 var category = await _categoryRepo.GetCategoryAsync(id);

//                 if (category == null)
//                 {
//                     return NotFound($"Category with id = {id} doesn't exist");
//                 }

//                 return Ok(category);
//             }
//             catch (Exception ex)
//             {
//                 return StatusCode(500, ex.Message);
//             }
//         }

//         [HttpPost("AddCategory")]
//         public async Task<ActionResult<Category>> AddCategory(PostCategoryViewModel model)
//         {

//             try
//             {
//                 var category = new Category()
//                 {
//                     CategoryName = model.CategoryName,
//                 };
//                 await _categoryRepo.AddCategoryAsync(category);
//                 if (await _categoryRepo.SaveAllAsync())
//                     return StatusCode(201);

//                 return StatusCode(500, "Error occured during saving of Category");
//             }
//             catch (Exception ex)
//             {
//                 return StatusCode(500, ex.Message);
//             }

//             // try
//             // {
//             //     await _categoryRepo.AddCategoryAsync(category);
//             //     if (await _categoryRepo.SaveAllAsync())
//             //     {
//             //         return StatusCode(201);
//             //     }
//             //     return StatusCode(500, "Error occurred during saving of category");
//             // }
//             // catch (Exception ex)
//             // {
//             //     return StatusCode(500, ex.Message);
//             // }
//         }
//         [HttpPut("ChangeCategoryName/{id}")]
//         public async Task<ActionResult> ChangeCategoryName(int id, PutCategoryViewModel model)
//         {
//             try
//             {
//                 var category = await _categoryRepo.GetCategoryAsync(id);

//                 if (category == null)
//                     return StatusCode(404, $"Category with id = {id} doesn't exist");

//                 category.CategoryName = model.CategoryName;

//                 await _categoryRepo.UpdateCategoryAsync(id, category);

//                 if (await _categoryRepo.SaveAllAsync())
//                     return NoContent();

//                 return StatusCode(500, $"Failed to update category with id: {id}");
//             }
//             catch (Exception ex)
//             {
//                 return StatusCode(500, ex.Message);
//             }
//         }

//         // [HttpPut("ChangeCategoryName/{id}")]
//         // public async Task<ActionResult> ChangeCategoryName(int id, PutCategoryViewModel model)
//         // {
//         //     try
//         //     {
//         //         var category = await _categoryRepo.GetCategoryAsync(id);
//         //         category.CategoryName = model.CategoryName;

//         //         await _categoryRepo.UpdateCategoryAsync(id, category);
//         //         if (await _categoryRepo.SaveAllAsync())
//         //             return NoContent();

//         //         return StatusCode(500, $"Failed to update category with id: {id}");
//         //     }
//         //     catch (NotFoundException)
//         //     {
//         //         return StatusCode(404, $"Category with id = {id} doesn't exist");
//         //     }
//         //     catch (Exception ex)
//         //     {
//         //         return StatusCode(500, ex.Message);
//         //     }
//         // }
//         [HttpDelete("DeleteCategory/{id}")]
//         public async Task<ActionResult> DeleteCategory(int id)
//         {
//             try
//             {
//                 var categoryToDelete = await _categoryRepo.GetCategoryAsync(id);

//                 if (categoryToDelete == null)
//                 {
//                     return NotFound($"Category with ID {id} not found.");
//                 }

//                 await _categoryRepo.DeleteCategoryAsync(categoryToDelete);

//                 if (await _categoryRepo.SaveAllAsync())
//                 {
//                     return NoContent(); // Successfully deleted
//                 }

//                 return StatusCode(500, "Failed to delete category.");
//             }
//             catch (Exception ex)
//             {
//                 return StatusCode(500, $"An error occurred: {ex.Message}");
//             }
//         }

//         // public async Task<ActionResult<Category>> AddCategory(PostCategoryViewModel model)
//         // {
//         //     try
//         //     {
//         //         await _categoryRepo.AddCategoryAsync(model);

//         //         if (await _categoryRepo.SaveAllAsync())
//         //         {
//         //             return StatusCode(201);
//         //         }

//         //         return StatusCode(500, "Failed to save category");
//         //     }
//         //     catch (Exception ex)
//         //     {
//         //         return StatusCode(500, ex.Message);
//         //     }
//         // }
//         // [HttpPut("ReplaceCategory/{id}")]
//         // public async Task<IActionResult> ReplaceCategory(int id, PutCategoryViewModel model) //testing IActionResult
//         // {
//         //     try
//         //     {
//         //         await _categoryRepo.UpdateCategoryAsync(id, model);

//         //         if (await _categoryRepo.SaveAllAsync())
//         //         {
//         //             return NoContent();
//         //         }

//         //         return StatusCode(500, $"Failed to save category with (id = {id})");
//         //     }
//         //     catch (NotFoundException)
//         //     {
//         //         return StatusCode(404, $"Category with id = {id} doesn't exist");
//         //     }
//         //     catch (Exception ex)
//         //     {
//         //         return StatusCode(500, ex.Message);
//         //     }
//         // }
//         // [HttpPatch("UpdateCategory/{id}")]
//         // public async Task<IActionResult> UpdateCategory(int id, PostCategoryViewModel model)
//         // {
//         //     try
//         //     {
//         //         await _categoryRepo.UpdateCategoryAsync(id, model);

//         //         if (await _categoryRepo.SaveAllAsync())
//         //         {
//         //             return StatusCode(200, $"Category {id} updated!");
//         //         }

//         //         return StatusCode(500, $"Failed to save changes (id = {id})");
//         //     }
//         //     catch (NotFoundException)
//         //     {
//         //         return StatusCode(404, $"Category with id = {id} doesn't exist");
//         //     }
//         //     catch (Exception ex)
//         //     {
//         //         return StatusCode(500, ex.Message);
//         //     }
//         // }
//         // [HttpDelete("{id}")]
//         // public async Task<ActionResult> DeleteCategory(int id)// //testing IActionResult
//         // {
//         //     try
//         //     {
//         //         await _categoryRepo.DeleteCategoryAsync(id);

//         //         if (await _categoryRepo.SaveAllAsync())
//         //         {
//         //             return NoContent();
//         //         }

//         //         return StatusCode(500, $"Failed to delete category (id = {id})");
//         //     }
//         //     catch (NotFoundException)
//         //     {
//         //         return StatusCode(404, $"Category with id = {id} doesn't exist");
//         //     }
//         //     catch (Exception ex)
//         //     {
//         //         return StatusCode(500, ex.Message);
//         //     }
//         // }
//         //}
//     }

// }