using College_API.Data;
using College_API.Interfaces;
using College_API.Models;
using College_API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace College_API.Controllers
{
    [ApiController]
    [Route("api/v3/courses")]
    public class CourseController : ControllerBase
    {
        private readonly CollegeDatabaseContext _context;
        private readonly ICourseRepository _courseRepo;
        public CourseController(CollegeDatabaseContext context, ICourseRepository courseRepo)
        {
            _context = context;
            _courseRepo = courseRepo;
        }

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
        public async Task<IActionResult> GetCourseById(int id)
        {
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
            return Ok();
        }

        [HttpPost("AddCourse")]
        public async Task<ActionResult<Course>> AddCourse(PostCourseViewModel model)
        {
            await _courseRepo.AddCourseAsync(model);
            if (await _courseRepo.SaveAllAsync())
                return StatusCode(201);

            return StatusCode(500, "Error occured during saving of Course");
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

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCourse(int id)
        {
            return NoContent();
        }

    }
}