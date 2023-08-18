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
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace College_API.Controllers
{
    [ApiController]
    [Route("api/v3/registration")]
    public class RegistrationsController : ControllerBase
    {
        private readonly CollegeDatabaseContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public RegistrationsController(CollegeDatabaseContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("registrations")]
        public async Task<IActionResult> GetAllRegistrations()
        {
            try
            {
                var registrations = await _context.Registrations
                    .Select(r => new
                    {
                        r.RegistrationId,
                        r.UserId,
                        r.CourseId,
                        r.RegistrationDate,
                        UserFullName = r.User.Email,
                        CourseTitle = r.Course.Name
                    })
                    .ToListAsync();

                return Ok(registrations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }

        [HttpPost("register-course")]
        public async Task<IActionResult> RegisterCourse(int courseId)
        {
            try
            {
                var course = await _context.Courses.FindAsync(courseId);

                if (course == null)
                    return NotFound("No course with id:" + courseId + " was found.");

                var user = await _userManager.GetUserAsync(User);

                if (user == null)
                    return Unauthorized("User not authenticated.");

                if (_context.Registrations.Any(r => r.UserId == user.Id && r.CourseId == courseId))
                    return BadRequest("User is already registered for this course.");

                var registration = new Registration
                {
                    UserId = user.Id,
                    CourseId = courseId,
                    RegistrationDate = DateTime.UtcNow
                };
                // This used to initiate a database transaction. A database transaction is a way to group multiple database operations together into a single unit of work that is executed as a whole.
                //Either all the operations within the transaction are completed successfully, or none of them are.
                using var transaction = await _context.Database.BeginTransactionAsync();

                try
                {
                    _context.Registrations.Add(registration);
                    await _context.SaveChangesAsync();
                    course.EnrolledStudents++;
                    course.Registrations.Add(registration);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return Ok("User registered for the course successfully.");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(500, "An error occurred: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }
        // [HttpPost("register-course")]
        // public async Task<IActionResult> RegisterCourse(int courseId)
        // {
        //     try
        //     {
        //         var course = await _context.Courses.Include(c => c.Registrations).FirstOrDefaultAsync(c => c.Id == courseId);
        //         if (course == null)
        //             return NotFound("No course with id:" + courseId + " was found.");

        //         var user = await _userManager.GetUserAsync(User);
        //         // return Ok(user);

        //         if (user == null)
        //             return Unauthorized("User not authenticated.");

        //         // Check if the user is already registered for the course
        //         if (_context.Registrations.Any(r => r.UserId == user.Id && r.CourseId == courseId))
        //             return BadRequest("User is already registered for this course.");

        //         // Create a new registration
        //         var registration = new Registration
        //         {
        //             UserId = user.Id,
        //             CourseId = courseId,
        //             RegistrationDate = DateTime.UtcNow
        //         };

        //         _context.Registrations.Add(registration);
        //         await _context.SaveChangesAsync();

        //         //update the course's Registrations property
        //         course.Registrations.Add(registration);
        //         await _context.SaveChangesAsync();

        //         return Ok("User registered for the course successfully.");
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, "An error occurred: " + ex.Message);
        //     }
        // }

    }
}
// var course = await _context.Courses.FindAsync(courseId);
// if (course == null)
//     return NotFound("No course with id:" + courseId + " was found.");
// //TODO Testing
// return Ok(courseId);

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

//     course.EnrolledStudents++; // Increment enrolled count
//     _context.Registrations.Add(newRegistration);
//     await _context.SaveChangesAsync();

//     return Ok("Successfully registered for the course.");
// }