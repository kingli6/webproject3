using College_API.Data;
using College_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace College_API.Controllers
{
    [ApiController]
    [Route("api/v3/course")]
    public class CourseController : ControllerBase
    {
        private readonly CollegeDatabaseContext _context;

        public CourseController(CollegeDatabaseContext context)
        {
            _context = context;
        }
        /*
            Task 1 List of http functions
                -crud users, +++ 

            Task ? 
            Task ? front end? razor pages? react, svelt?
             TASK 4 students website. Or Admin page
                    Admin page:
                        create users
                        delete users

                        create +++ teacher, courses, send emails?+++
            Task 5 


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