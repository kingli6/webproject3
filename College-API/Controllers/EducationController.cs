using Microsoft.AspNetCore.Mvc;

namespace College_API.Controllers
{
    [ApiController]
    [Route("api/v3/education")]
    public class EducationController : ControllerBase
    {
        [HttpGet("GetAllEducation")]
        public ActionResult GetAllEducation()
        {
            return Ok("It works");
        }
    }
}