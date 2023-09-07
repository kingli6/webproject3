using College_API.CustomExceptions;
using College_API.Data;
using College_API.Interfaces;
using College_API.Models;
using College_API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace College_API.Controllers
{
    [ApiController]
    [Route("api/v3/courses")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepo;
        public CourseController(ICourseRepository courseRepo)
        {
            _courseRepo = courseRepo;
        }

        // [Authorize]
        [HttpGet("GetAllCourses")]
        public async Task<ActionResult<List<Course>>> ListAllCourses()
        {
            try
            {
                var list = await _courseRepo.ListAllCourseAsync();
                return Ok(list);
            }

            catch (System.Exception ex)
            {
                return StatusCode(404, "Tabel doesn't exist. " + "Error msg: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCourseById(int id)
        {
            try
            {
                return Ok(await _courseRepo.GetCourseAsync(id));
            }
            catch (NotFoundException)
            {
                return StatusCode(404, $"Course with id = {id} doesn't exist");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        //PROBLEM TODO a new category is added when a course is created... 
        [HttpPost("AddCourse")]
        public async Task<ActionResult<Course>> AddCourse(PostCourseViewModel model)
        {
            try
            {

                await _courseRepo.AddCourseAsync(model);
                if (await _courseRepo.SaveAllAsync())
                    return StatusCode(201);

                return StatusCode(500, "Error occured during saving of Course");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("ReplaceCourse/{id}")]
        public async Task<ActionResult> UpdateCourse(int id, PutCourseViewModel model)
        {
            try
            {
                await _courseRepo.UpdateCourseAsync(id, model);

                if (await _courseRepo.SaveAllAsync())
                    return NoContent();

                return StatusCode(500, $"Failed to save course with id: {id}");
            }
            catch (NotFoundException)
            {
                return StatusCode(404, $"Course with id = {id} doesn't exist");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCourse(int id)
        {
            try
            {
                await _courseRepo.DeleteCourseAsync(id);
                if (await _courseRepo.SaveAllAsync())
                    return NoContent();

                return StatusCode(500, $"Failed to delete course (id = {id})");
            }
            catch (NotFoundException)
            {
                return StatusCode(404, $"Coures with id = {id} doesn't exist");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}