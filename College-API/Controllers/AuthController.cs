using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using College_API.ViewModels.AuthViewModels;
using College_API.ViewModels.RegisterUserViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace College_API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AuthController(IConfiguration config, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserViewModel>> RegisterUser(RegisterUserViewModel model)
        {
            var user = new IdentityUser
            {
                Email = model.Email!.ToLower(),
                UserName = model.Email.ToLower(),

            };
            var result = await _userManager.CreateAsync(user, model.Password!);
            if (result.Succeeded)
            {
                var userData = new UserViewModel
                {
                    UserName = user.UserName,
                    Token = CreateJwtToken(user)
                };
                return StatusCode(201, userData);
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("User registration", error.Description);
                }
                return StatusCode(500, ModelState);
            }
        }

        [HttpPost("login")]
        public ActionResult Login(LoginViewModel model)
        {
            if (model.UserName == "Joe" && model.Password == "password!")
            {
                return Ok(new
                {
                    access_token = "" //CreateJwtToken(model.UserName)
                });
            }
            return Unauthorized();
        }
        //[Authorize("Administrator")]
        private string CreateJwtToken(IdentityUser user)
        {// jwt.io  to control the token
            var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("apiKey")!);
            var claims = new List<Claim>{
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                // new Claim("Admin", "true"),
            };
            var jwt = new JwtSecurityToken(
                claims: claims,
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
    }
}