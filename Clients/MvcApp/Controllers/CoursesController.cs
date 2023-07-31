using Microsoft.AspNetCore.Mvc;
using MvcApp.Models;
using MvcApp.ViewModels;

namespace MvcApp.Controllers
{
    [Route("[controller]")]
    public class CoursesController : Controller
    {
        private readonly IConfiguration _config;
        public CoursesController(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                // ViewData["CourseID"] = "A million ID's";
                var courseService = new CourseServiceModel(_config);
                var courses = await courseService.ListAllCourse();
                return View("Index", courses);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            ViewBag.CourseId = id;
            var baseUrl = _config.GetValue<string>("baseUrl");
            var url = $"{baseUrl}/courses/{id}";

            using var http = new HttpClient();
            var response = await http.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                Console.WriteLine("Details method didn't work, couldn't find the course with id: " + id);

            var course = await response.Content.ReadFromJsonAsync<CourseViewModel>();

            return View("Details", course ?? new CourseViewModel());
        }


    }
}