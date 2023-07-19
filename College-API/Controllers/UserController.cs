using AutoMapper;
using College_API.Interfaces;
using College_API.Models;
using College_API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace College_API.Controllers
{
    [ApiController]
    [Route("api/v3/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        public UserController(IUserRepository userRepo, IMapper mapper)
        {
            _mapper = mapper;
            _userRepo = userRepo;
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<List<User>>> ListAllUsers()
        {
            return Ok(await _userRepo.ListAllUsersAsync());

            // var userList = await _userRepo.ListAllUsersAsync();
            // return Ok(userList);

            // var userList = _mapper.Map<List<UserViewModel>>(response);
            // var response = await _userRepo.ListAllUsersAsync();

            // var response = await _context.Users.ToListAsync();
            // var userList = new List<UserViewModel>();
            // foreach (var user in response)
            // {
            //     userList.Add(
            //         new UserViewModel
            //         {
            //             UserId = user.Id,
            //             UserName = string.Concat(user.FirstName, " ", user.LastName),
            //             Email = user.Email,
            //             PhoneNumber = user.PhoneNumber,
            //             Address = user.Address
            //         }
            //     );
            // }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var response = await _userRepo.GetUserAsync(id);
            if (response is null)
                return NotFound($"couldn't find id: {id} to delete");

            return Ok(response);
        }

        [HttpGet("byuser/{userName}")]
        public async Task<ActionResult<User>> GetUser(string userName)
        {
            var response = await _userRepo.GetUserAsync(userName);
            if (response is null)
                return NotFound($"couldn't find name: {userName} in database");
            return Ok(response);
        }
        //[HttpGet("byemail/{userEmail}")] or find a list of users specific to something? Course applied?

        [HttpPost()]
        public async Task<ActionResult> AddUser(PostUserViewModel model)
        {

            if (await _userRepo.GetUserEmailAsync(model.Email!.ToLower()) is not null)  //220503 09.... 27:00
                return BadRequest($"Email {model.Email} already exists in the database");

            await _userRepo.AddUserAsync(model);
            if (await _userRepo.SaveAllAsync())
            {
                return StatusCode(201); // Status code 204
            }
            return StatusCode(500, "An error occured when attempted to save new user");



            // var userToAdd = _mapper.Map<User>(user);    //To <User>(class) from user(PostUserViewModel class-object)
            // await _context.Users.AddAsync(userToAdd);
            // await _context.SaveChangesAsync();
            // return StatusCode(201, user);

            // var userToAdd = new User
            // {
            //     FirstName = user.FirstName,
            //     LastName = user.LastName,
            //     Email = user.Email,
            //     PhoneNumber = user.PhoneNumber,
            //     Address = user.Address
            // };
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, PostUserViewModel model)
        {
            try //220503_09 50:33
            {
                await _userRepo.UpdateUserAsync(id, model);
                if (await _userRepo.SaveAllAsync())
                {
                    return NoContent(); // Status code 204
                }
                return StatusCode(500, "An error occured when attempted to update user");
            }
            catch (Exception ex)    //51:40
            {
                return StatusCode(500, ex.Message);
            }

            // var response = await _context.Users.FindAsync(id);
            // if (response is null)
            //     return NotFound($"Couldn't find user with id: {id}");

            // response.FirstName = model.FirstName;
            // response.LastName = model.LastName;
            // response.Email = model.Email;
            // response.PhoneNumber = model.PhoneNumber;
            // response.Address = model.Address;
            // _context.Users.Update(response);
            // await _context.SaveChangesAsync();

            // return NoContent();
        }

        [HttpPatch("{id}")] //to update phone and adress of user...
        public async Task<ActionResult> UpdateUser(int id, PatchUserViewModel model)
        {
            try
            {
                await _userRepo.UpdateUserAsync(id, model);

                if (await _userRepo.SaveAllAsync())
                    return NoContent();

                return StatusCode(500, "An error occured when updating user data");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            await _userRepo.DeleteUserAsync(id);

            if (await _userRepo.SaveAllAsync())
                return NoContent();
            return StatusCode(500, "Ops... couldn't save");
        }
    }
}
//            
