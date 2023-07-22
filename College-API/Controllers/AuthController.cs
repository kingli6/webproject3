using System.IdentityModel.Tokens.Jwt;
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
        [HttpPost()]
        public ActionResult Authenticate(LoginViewModel model)
        {
            if (model.UserName == "Joe" && model.Password == "password!")
                return Ok();
            return Unauthorized();
        }

        private string CreateJwtToken(string userName)
        {
            var key = Encoding.ASCII.GetBytes("slkj#Xzsalslk.-,jfsö^#¤sdf");
            var jwt = new JwtSecurityToken(
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}