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

        public CourseController(CollegeDatabaseContext context)
        {
            _context = context;
        }
        /*
            
        */
        [HttpGet("GetAllCourse")]
        public async Task<ActionResult<List<Course>>> GetAllCourse()
        {
            try
            {
                var educationList = await _context.Courses.ToListAsync();
                return Ok(educationList);
            }
            catch (System.Exception ex)
            {
                return StatusCode(404, "Tabel doesn't exist. " + "Error msg: " + ex.Message);
            }
        }
        [HttpPost("AddCourse")]
        public async Task<ActionResult<Course>> AddCourse(Course education)
        {
            try
            {
                await _context.Courses.AddAsync(education);
                if (await _context.SaveChangesAsync() > 0)
                    return StatusCode(201);
                return StatusCode(500, "failed to add to DB");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}