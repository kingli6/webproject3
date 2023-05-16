using College_API.Data;
using College_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace College_API.Controllers
{
    [ApiController]
    [Route("api/v3/education")]
    public class EducationController : ControllerBase
    {
        private readonly CollegeDatabaseContext _context;

        public EducationController(CollegeDatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllEducation")]
        public async Task<ActionResult<List<Education>>> GetAllEducation()
        {
            try
            {
                var educationList = await _context.Educations.ToListAsync();
                return Ok(educationList);
            }
            catch (System.Exception ex)
            {
                return StatusCode(404, "Tabel doesn't exist. " + "Error msg: " + ex.Message);
            }
        }
        [HttpPost("AddCourse")]
        public async Task<ActionResult<Education>> AddEducation(Education education)
        {
            try
            {
                await _context.Educations.AddAsync(education);
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