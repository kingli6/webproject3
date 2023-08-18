using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using College_API.CustomExceptions;
using College_API.Data;
using College_API.Interfaces;
using College_API.Models;
using College_API.ViewModels;

namespace College_API.Controllers
{
    [ApiController]
    [Route("api/v3/registration")]
    public class RegistrationsController : ControllerBase
    {
        private readonly CollegeDatabaseContext _context;

        public RegistrationsController(CollegeDatabaseContext context)
        {
            _context = context;
        }
        [HttpPost("RegisterCourse")]
        public async Task<IActionResult> RegisterForCourse(int courseId)
        {
            try
            {
                var course = await _context.Courses.FindAsync(courseId);
                if (course == null)
                    return NotFound("No course with id:" + courseId + " was found.");
                //TODO Testing
                return Ok(courseId);
            }

            // {
            //     // Check if user is already registered for the course
            //     var userId = GetCurrentUserId(); // Implement this method to get the current user's ID
            //     var existingRegistration = await _context.Registrations
            //         .FirstOrDefaultAsync(r => r.UserId == userId && r.CourseId == courseId);

            //     if (existingRegistration != null)
            //         return BadRequest("You are already registered for this course.");

            //     var newRegistration = new Registration
            //     {
            //         UserId = userId,
            //         CourseId = courseId,
            //         RegistrationDate = DateTime.UtcNow
            //     };

            //     course.Enrolled++; // Increment enrolled count
            //     _context.Registrations.Add(newRegistration);
            //     await _context.SaveChangesAsync();

            //     return Ok("Successfully registered for the course.");
            // }
            catch (System.Exception)
            {

                throw;
            }
        }

    }
}



// public IActionResult Index()
// {
//     return View();
// }

// [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
// public IActionResult Error()
// {
//     return View("Error!");
// }