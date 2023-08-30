using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using College_API.CustomExceptions;
using College_API.Data;
using College_API.Models;
using College_API.ViewModels.AuthViewModels;
using College_API.ViewModels.CustomerViewModel;
using College_API.ViewModels.RegisterUserViewModel;
using College_API.ViewModels.UserCustomerViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


namespace College_API.Controllers
{
    [ApiController]
    [Route("api/v3/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly CollegeDatabaseContext _context;

        public AuthController(CollegeDatabaseContext context, IMapper mapper, IConfiguration config, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
            _config = config;
            _mapper = mapper;
            _context = context;
        }

        // admin1@email.com PassWord!1
        // "user1@example.com",PassWord!1

        [HttpGet("getAllAdmins")]
        public async Task<ActionResult<List<SignInUserViewModel>>> GetAllAdminsAsync()
        {
            try
            {
                var admins = await _userManager.GetUsersInRoleAsync("Administrator");
                Console.WriteLine(admins);
                var adminList = _mapper.ProjectTo<SignInUserViewModel>(admins.AsQueryable()).ToList();
                return Ok(adminList);
            }
            catch (NotFoundException)
            {
                return StatusCode(404, "Table doesn't exsist for method GetAllAdminsAsync");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetAllUsers")]    //Get Identity information
        public async Task<ActionResult<IEnumerable<SignInUserViewModel>>> GetAllUsersAsync()
        {
            try
            {
                //return Ok(await _context.Users.ProjectTo<SignInUserViewModel>(_mapper.ConfigurationProvider).ToListAsync());

                //getting everyone in _userManager
                return Ok(await _userManager.Users.ToListAsync());
            }
            catch (NotFoundException)
            {
                return StatusCode(404, "Table doesn't exsist in method GetAllusersAsync");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("getUserById/{id}")]
        public async Task<ActionResult<CustomerUserViewModel>> GetUserById(string id)
        {
            try
            {
                var user = await _context.ApplicationUsers.FindAsync(id);

                if (user == null)
                {
                    return NotFound("User not found");
                }

                var roles = await _userManager.GetRolesAsync(user);

                var userViewModel = new CustomerUserViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    Roles = roles.ToList()
                };

                return Ok(userViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("register")]
        public async Task<ActionResult<SignInUserViewModel>> RegisterUser(RegisterUserViewModel model)
        {
            // if Administrator role doesn't exsist, create one.
            if (!await _roleManager.RoleExistsAsync("Administrator"))
            {
                var adminRole = new IdentityRole("Administrator");
                await _roleManager.CreateAsync(adminRole);
            }
            var user = new IdentityUser
            {
                Email = model.Email!.ToLower(),
                UserName = model.Email.ToLower(),

            };
            var result = await _userManager.CreateAsync(user, model.Password!);
            if (result.Succeeded)
            {

                // creating a claim called Admin and setting IsAdmin to true    220504_13.. 1:25:00
                if (model.IsAdmin)
                {
                    await _userManager.AddClaimAsync(user, new Claim("Admin", "true"));
                    await _userManager.AddToRoleAsync(user, "Administrator");  //220504_13.. 2:33:00
                    await _userManager.AddClaimAsync(user, new Claim("Administrator", "true"));
                }
                if (!await _roleManager.RoleExistsAsync("User"))
                {
                    var userRole = new IdentityRole("User");
                    await _roleManager.CreateAsync(userRole);
                }
                // ??? Roles or claims?
                await _userManager.AddToRoleAsync(user, "User");
                await _userManager.AddClaimAsync(user, new Claim("User", "true"));
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, user.UserName));
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.NameIdentifier, user.Id));

                var userData = new SignInUserViewModel
                {
                    UserName = user.UserName,
                    Token = await CreateJwtToken(user)
                };
                return StatusCode(201, userData);
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("User registration error. ", error.Description);
                }
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet("getallusersByAdmin")]
        public async Task<ActionResult<IEnumerable<CustomerUserViewModel>>> GetAllUsers()
        {
            try
            {
                var users = await _context.ApplicationUsers.ToListAsync();

                var userViewModels = new List<CustomerUserViewModel>();

                foreach (var user in users)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    var userViewModel = new CustomerUserViewModel
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Address = user.Address,
                        Roles = roles.ToList()
                    };
                    userViewModels.Add(userViewModel);
                }
                return Ok(userViewModels);
            }
            catch (NotFoundException)
            {
                return StatusCode(404, "Tabel doesn't exsist");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("createUser")]
        public async Task<ActionResult<SignInUserViewModel>> AddUserAsync([FromBody] SignInCustomerViewModel newUser)
        {
            try
            { // Creating roles if it doesn't exists
                if (!await _roleManager.RoleExistsAsync("User"))
                {
                    var userRole = new IdentityRole("User");
                    await _roleManager.CreateAsync(userRole);
                }
                if (!await _roleManager.RoleExistsAsync("Administrator"))
                {
                    var adminRole = new IdentityRole("Administrator");
                    await _roleManager.CreateAsync(adminRole);
                }

                if (!await _roleManager.RoleExistsAsync("Teacher")) // Add this block for the "Teacher" role
                {
                    var teacherRole = new IdentityRole("Teacher");
                    await _roleManager.CreateAsync(teacherRole);
                }

                var user = new ApplicationUser
                {
                    FirstName = newUser.FirstName!.ToLower(),
                    LastName = newUser.LastName!.ToLower(),
                    UserName = newUser.Email!.ToLower(),
                    Email = newUser.Email,
                    PhoneNumber = newUser.PhoneNumber,
                    Address = newUser.Address!.ToLower(),
                };

                var result = await _userManager.CreateAsync(user, newUser.Password!);
                if (result.Succeeded)
                {//Adding User roles
                    if (newUser.UserRole.Contains("Administrator"))
                    {
                        await _userManager.AddToRoleAsync(user, "Administrator");  //220504_13.. 2:33:00
                        await _userManager.AddClaimAsync(user, new Claim("Administrator", "true"));
                    }
                    if (newUser.UserRole.Contains("Teacher"))
                    {
                        await _userManager.AddToRoleAsync(user, "Teacher");
                        await _userManager.AddClaimAsync(user, new Claim("Teacher", "true"));
                    }
                    if (newUser.UserRole.Contains("User")) // Add this block to handle the "User" role
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                        await _userManager.AddClaimAsync(user, new Claim("User", "true"));
                    }
                    await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, user.UserName));
                    await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));
                    await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.NameIdentifier, user.Id));

                    var userData = new SignInUserViewModel
                    {
                        UserName = user.UserName,
                        Token = await CreateJwtToken(user)
                    };
                    return StatusCode(201, userData);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("User registration error. ", error.Description);
                    }
                    return StatusCode(500, ModelState);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("updateUserById/{id}")]
        public async Task<ActionResult> UpdateUserByIdAsync(string id, SignInCustomerViewModel model)
        {
            try
            {
                if (model is null)
                    throw new BadRequestException();

                var user = await _context.ApplicationUsers.SingleOrDefaultAsync(u => u.Id == id) ?? throw new NotFoundException();

                user.FirstName = model.FirstName!;
                user.LastName = model.LastName!;
                user.PhoneNumber = model.PhoneNumber!;
                user.Address = model.Address!;

                // Update user properties
                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("User update error. ", error.Description);
                    }
                    return StatusCode(500, ModelState);
                }

                // Update user roles
                var existingRoles = await _userManager.GetRolesAsync(user);
                var rolesToAdd = model.UserRole.Except(existingRoles);
                var rolesToRemove = existingRoles.Except(model.UserRole);

                await _userManager.AddToRolesAsync(user, rolesToAdd);
                await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

                return Ok("User updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("updateUserByEmail/{email}")]
        public async Task<ActionResult> UpdateUserByEmail(string email, SignInCustomerViewModel model)
        {
            try
            {
                if (model is null)
                    throw new BadRequestException();

                var user = await _context.ApplicationUsers.SingleOrDefaultAsync(u => u.Email == email) ?? throw new NotFoundException();

                user.FirstName = model.FirstName!;
                user.LastName = model.LastName!;
                user.PhoneNumber = model.PhoneNumber!;
                user.Address = model.Address!;

                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("User update error. ", error.Description);
                    }
                    return StatusCode(500, ModelState);
                }

                // Update user roles
                var existingRoles = await _userManager.GetRolesAsync(user);
                var rolesToAdd = model.UserRole.Except(existingRoles);
                var rolesToRemove = existingRoles.Except(model.UserRole);

                foreach (var roleToAdd in rolesToAdd)
                {
                    var role = await _roleManager.FindByNameAsync(roleToAdd);
                    if (role != null)
                    {
                        user.UserRoles.Add(new IdentityUserRole<string>
                        {
                            RoleId = role.Id,
                            UserId = user.Id
                        });
                    }
                }

                foreach (var roleToRemove in rolesToRemove)
                {
                    var role = await _roleManager.FindByNameAsync(roleToRemove);
                    if (role != null)
                    {
                        var userRole = user.UserRoles.FirstOrDefault(ur => ur.RoleId == role.Id);
                        if (userRole != null)
                        {
                            user.UserRoles.Remove(userRole);
                        }
                    }
                }

                // Save changes
                await _context.SaveChangesAsync();

                return Ok("User updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("users-with-roles")]
        public async Task<ActionResult<List<UserWithRolesViewModel>>> GetUsersWithRoles()
        {
            var users = await _userManager.Users.ToListAsync();

            var usersWithRoles = new List<UserWithRolesViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userWithRoles = new UserWithRolesViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Roles = roles.ToList()
                };
                usersWithRoles.Add(userWithRoles);
            }

            return Ok(usersWithRoles);
        }

        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteUserByEmail(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null) return StatusCode(404, email + " User not found");

                await _userManager.DeleteAsync(user);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return StatusCode(404, email + " User not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<SignInUserViewModel>> Login(LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName!);
            if (user is null)
                return Unauthorized("Wrong username or password???");

            // Check if the user has the "Administrator" role
            if (!await _userManager.IsInRoleAsync(user, "Administrator") && !await _userManager.IsInRoleAsync(user, "User"))
                return Unauthorized("Only users with the 'Administrator' or 'User'[Hack] role can log in.");
            // if (!await _userManager.IsInRoleAsync(user, "Administrator"))
            //         return Unauthorized("Only users with the 'Administrator' role can log in.");
            //if (!await _userManager.GetRolesAsync(user).Any(role => role == "Administrator" || role == "User"))
            //      return Unauthorized("Only users with the 'Administrator' or 'User' role can log in.");
            //
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password!, false);   //false is for lockoutonfailure .false meaning; we won't lock them out

            if (!result.Succeeded)
                return Unauthorized("Wrong password");

            // Attempt to sign in with password. (Can still fail if user is on lockout or account isn't verified etc.)
            result = await _signInManager.PasswordSignInAsync(user, model.Password!, false, true);

            if (!result.Succeeded)
                return Unauthorized();
            var userRoles = await _userManager.GetRolesAsync(user);
            var userVM = _mapper.Map<SignInUserViewModel>(user);
            // Create JW token.
            userVM.Token = await CreateJwtToken(user);
            userVM.Expires = DateTime.Now.AddDays(7);
            userVM.Roles = userRoles.ToList();

            return Ok(userVM);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return NoContent(); // Return a successful response with no content
        }

        private async Task<string> CreateJwtToken(IdentityUser user)
        {// jwt.io  to control the token
            var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("apiKey")!);
            // var claims = new List<Claim>{
            //     new Claim(ClaimTypes.Name, user.UserName!),
            //     new Claim(ClaimTypes.Email, user.Email!),
            //     // new Claim("Admin", "true"),
            // };

            var userClaims = await _userManager.GetClaimsAsync(user);//220504_13..1:35:00
            var roles = await _userManager.GetRolesAsync(user);
            userClaims.ToList().AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var jwt = new JwtSecurityToken(
                claims: userClaims,//claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        // private string CreateJwtToken(string userName)
        // {// jwt.io  to control the token
        //     var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("apiKey")!);
        //     var claims = new List<Claim>{
        //         new Claim(ClaimTypes.Name, userName),
        //         new Claim("Admin", "true"),
        //         new Claim(ClaimTypes.Email, "joe@gmail.com")
        //     };
        //     var jwt = new JwtSecurityToken(
        //         claims: claims,
        //         notBefore: DateTime.Now,
        //         expires: DateTime.Now.AddDays(1),
        //         signingCredentials: new SigningCredentials(
        //             new SymmetricSecurityKey(key),
        //             SecurityAlgorithms.HmacSha512Signature));
        //     return new JwtSecurityTokenHandler().WriteToken(jwt);
        // }

        //Get all users doesn't show admins. Is it due to them being IdentityUser object?

        [HttpPost("create-role")]// 220504_13 2:34:00
        public async Task<IActionResult> CreateRole([FromQuery] string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (!result.Succeeded) return BadRequest(result.Errors);
            }
            else
            {
                return StatusCode(200, $"Role name with name '{roleName}' already exists.");
            }
            return StatusCode(201, $"Role '{roleName}' created.");

        }
    }
}