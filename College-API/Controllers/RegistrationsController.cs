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

        [HttpGet("check-registration/{courseId}")]
        public async Task<IActionResult> CheckRegistration(int courseId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized("User not authenticated.");

            var isRegistered = _context.Registrations
                .Any(r => r.UserId == user.Id && r.CourseId == courseId);

            return Ok(new { IsRegistered = isRegistered });
        }

        [HttpDelete("delete-registration/{registrationId}")]
        public async Task<IActionResult> DeleteRegistration(int registrationId)
        {
            try
            {
                var registration = await _context.Registrations.FindAsync(registrationId);

                if (registration == null)
                    return NotFound("No registration with id:" + registrationId + " was found.");

                // Check if the currently authenticated user is the one who registered
                var currentUser = await _userManager.GetUserAsync(User); // Assuming you're using UserManager

                if (registration.UserId != currentUser.Id)
                    return Forbid("You are not authorized to delete this registration.");

                // Remove the user from the registration and save changes
                _context.Registrations.Remove(registration); // Remove the user association
                await _context.SaveChangesAsync();
                await UpdateEnrolledStudentsCount(registration.CourseId);

                return Ok("Registration deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }

        [HttpGet("getAllRegistrations")]
        public async Task<IActionResult> GetAllRegistrations()
        {
            try
            {
                var registrations = await _context.Registrations
                    .Include(r => r.User) // Include the User navigation property
                    .Include(r => r.Course) // Include the Course navigation property
                    .Select(r => new
                    {
                        r.RegistrationId,
                        UserFullName = r.User.Email, // Assuming User has an Email property
                        CourseTitle = r.Course.Name, // Assuming Course has a Name property
                        r.RegistrationDate
                    })
                    .ToListAsync();

                return Ok(registrations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }

        [HttpPost("register-course/{id}")]
        public async Task<IActionResult> RegisterCourse(int id)
        {
            try
            {
                var course = await _context.Courses.FindAsync(id);

                if (course == null)
                    return NotFound("No course with id:" + id + " was found.");

                var user = await _userManager.GetUserAsync(User);

                if (user == null)
                    return Unauthorized("User not authenticated.");

                if (_context.Registrations.Any(r => r.UserId == user.Id && r.CourseId == id))
                    return BadRequest("User is already registered for this course.");

                var registration = new Registration
                {
                    User = user,
                    Course = course,
                    RegistrationDate = DateTime.UtcNow
                };

                _context.Registrations.Add(registration);
                // course.EnrolledStudents++;
                await _context.SaveChangesAsync();
                await UpdateEnrolledStudentsCount(id);

                return Ok("User registered for the course successfully.");

            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }
        private async Task UpdateEnrolledStudentsCount(int courseId)
        {
            var course = await _context.Courses.FindAsync(courseId);

            if (course != null)
            {
                course.EnrolledStudents = await _context.Registrations.CountAsync(r => r.CourseId == courseId);
                await _context.SaveChangesAsync();
            }
        }
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