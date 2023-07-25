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
                // creating a claim called Admin and setting IsAdmin to true    220504_13.. 1:25:00
                if (model.IsAdmin)
                {
                    await _userManager.AddClaimAsync(user, new Claim("Admin", "true"));
                }
                await _userManager.AddClaimAsync(user, new Claim("User", "true"));
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, user.UserName));
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.NameIdentifier, user.Id));
                var userData = new UserViewModel
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
        //user@example.com, Password!1
        //"userName": "joe@gmail.com", "password": "Password!1"
        [HttpPost("login")]
        public async Task<ActionResult<UserViewModel>> Login(LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName!);
            if (user is null)
                return Unauthorized("Wrong username or password???");

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password!, false);   //false is for lockoutonfailure .false meaning; we won't lock them out

            if (!result.Succeeded)
                return Unauthorized();

            var userData = new UserViewModel
            {
                UserName = user.UserName,
                Token = await CreateJwtToken(user)
            };

            return Ok(userData);
            // if (model.UserName == "Joe" && model.Password == "password!")
            // {
            //     return Ok(new
            //     {
            //         access_token = CreateJwtToken(model.UserName)
            //     });
            // }
            // return Unauthorized();
        }
        //[Authorize("Administrator")]
        private async Task<string> CreateJwtToken(IdentityUser user)
        {// jwt.io  to control the token
            var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("apiKey")!);
            // var claims = new List<Claim>{
            //     new Claim(ClaimTypes.Name, user.UserName!),
            //     new Claim(ClaimTypes.Email, user.Email!),
            //     // new Claim("Admin", "true"),
            // };

            var userClaims = await _userManager.GetClaimsAsync(user);//220504_13..1:35:00
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
    }
}