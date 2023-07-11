using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using College_API.Data;
using College_API.Models;
using College_API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace College_API.Controllers
{
    [ApiController]
    [Route("api/v3/users")]
    public class UserController : ControllerBase
    {
        private readonly CollegeDatabaseContext _context;
        public UserController(CollegeDatabaseContext context) { _context = context; }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var response = await _context.Users.ToListAsync();
            return Ok(response);
        }
        [HttpGet("byuser/{userName}")]
        public async Task<ActionResult<User>> GetUserByName(string userName)
        {
            var response = await _context.Users.FirstOrDefaultAsync(c => c.FirstName == userName || c.LastName == userName);
            if (response is null)
                return NotFound($"couldn't find name: {userName} in database");
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var response = await _context.Users.FindAsync(id);
            if (response is null)
                return NotFound($"couldn't find id: {id} to delete");
            return Ok(response);
        }
        [HttpPost()]
        public async Task<ActionResult<User>> AddUser(PostUserViewModel user)
        {
            var userToAdd = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address
            };
            await _context.Users.AddAsync(userToAdd);
            await _context.SaveChangesAsync();
            return StatusCode(201, user);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, User model)
        {
            var response = await _context.Users.FindAsync(id);
            if (response is null)
                return NotFound($"Couldn't find user with id: {id}");

            response.FirstName = model.FirstName;
            response.LastName = model.LastName;
            response.Email = model.Email;
            response.PhoneNumber = model.PhoneNumber;
            response.Address = model.Address;
            _context.Users.Update(response);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var response = await _context.Users.FindAsync(id);
            if (response is null) return NotFound($"couldn't find id: {id} to delete");
            _context.Users.Remove(response);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}