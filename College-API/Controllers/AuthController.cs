using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using College_API.ViewModels.AuthViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace College_API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        public AuthController(IConfiguration config)
        {
            _config = config;

        }

        [HttpPost("login")]
        public ActionResult Login(LoginViewModel model)
        {
            if (model.UserName == "Joe" && model.Password == "password!")
            {
                return Ok(new
                {
                    access_token = CreateJwtToken(model.UserName)
                });
            }
            return Unauthorized();
        }
        //[Authorize("Administrator")]
        private string CreateJwtToken(string userName)
        {// jwt.io  to control the token
            var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("apiKey")!);
            var claims = new List<Claim>{
                new Claim(ClaimTypes.Name, userName),
                new Claim("Admin", "true"),
                new Claim(ClaimTypes.Email, "joe@gmail.com")
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
    }
}