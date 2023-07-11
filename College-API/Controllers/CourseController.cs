using College_API.Data;
using College_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace College_API.Controllers
{
    [ApiController]
    [Route("api/v3/courses")]
    public class CourseController : ControllerBase
    {
        private readonly CollegeDatabaseContext _context;
        public CourseController(CollegeDatabaseContext context) { _context = context; }

        [HttpGet("GetAllCourse")]
        public async Task<ActionResult<List<Course>>> GetAllCourse()
        {
            try
            {
                var courseList = await _context.Courses.ToListAsync();
                return Ok(courseList);
            }

            catch (System.Exception ex)
            {
                return StatusCode(404, "Tabel doesn't exist. " + "Error msg: " + ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _context.Courses.Select(v => new
            {
                Id = v.Id,
                CourseNumber = v.CourseNumber,
                CourseName = v.CourseName,
                Duration = v.Duration,
                Description = v.Description,
                Detail = v.Details,
                ImagePath = v.ImagePath,
                // ImageUrl = course.ImageUrl
                // Users = v.Users

            }).SingleOrDefaultAsync(c => c.Id == id);
            return Ok(result);
        }

        [HttpPost("AddCourse")]
        public async Task<ActionResult<Course>> AddCourse(Course course)
        {
            var courseToAdd = new Course
            {
                CourseNumber = course.CourseNumber,
                CourseName = course.CourseName,
                Duration = course.Duration,
                Description = course.Description,
                Details = course.Details,
                ImagePath = course.ImagePath,
                // ImageUrl = course.ImageUrl
                // Users = course.Users
            };
            try
            {
                await _context.Courses.AddAsync(courseToAdd);
                if (await _context.SaveChangesAsync() > 0)
                    // return StatusCode(201);
                    return CreatedAtAction(nameof(GetById), new { id = courseToAdd.Id },
                    new
                    {
                        Id = courseToAdd.Id,
                        CourseNumber = courseToAdd.CourseNumber,
                        CourseName = courseToAdd.CourseName,
                        Description = courseToAdd.Description
                    });
                return StatusCode(500, "failed to add to DataBase");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}