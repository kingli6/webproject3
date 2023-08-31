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
            // var result = await _context.Courses.Select(v => new
            // {
            //     Id = v.Id,
            //     CourseNumber = v.CourseNumber,
            //     CourseName = v.CourseName,
            //     Duration = v.Duration,
            //     Description = v.Description,
            //     Detail = v.Details,
            //     ImagePath = v.ImagePath,
            //     // ImageUrl = course.ImageUrl
            //     // Users = v.Users

            // }).SingleOrDefaultAsync(c => c.Id == id);
            // return Ok(result);
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



            // var courseToAdd = new Course
            // {
            //     CourseNumber = course.CourseNumber,
            //     CourseName = course.CourseName,
            //     Duration = course.Duration,
            //     Description = course.Description,
            //     Details = course.Details,
            //     ImagePath = course.ImagePath,
            //     // ImageUrl = course.ImageUrl
            //     // Users = course.Users
            // };
            // try
            // {
            //     await _context.Courses.AddAsync(courseToAdd);
            //     if (await _context.SaveChangesAsync() > 0)
            //         // return StatusCode(201);
            //         return CreatedAtAction(nameof(GetCourseById), new { id = courseToAdd.Id },
            //         new
            //         {
            //             Id = courseToAdd.Id,
            //             CourseNumber = courseToAdd.CourseNumber,
            //             CourseName = courseToAdd.CourseName,
            //             Description = courseToAdd.Description
            //         });
            //     return StatusCode(500, "failed to add to DataBase");
            // }
            // catch (Exception ex)
            // {
            //     Console.WriteLine(ex.Message);
            //     return StatusCode(500, "Internal server error");
            // }
            // return Ok();
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