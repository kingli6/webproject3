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
            try
            {
                var courseService = new CourseServiceModel(_config);
                var course = await courseService.FindCourse(id);

                return View("Details", course);
            }
            catch (Exception ex)
            {
                //We should return a errorpage with information
                //return View("Error", errorObject);
                //Or create a ViewBad t ex ViewBag.ErrorMessage = ???
                // return view Details and look if error message has any information
                //don't forget to control the ErrorMessage property is in the ViewBag boject
                System.Console.WriteLine(ex.Message);
                return View("Error");
            }

        }

        [HttpGet("Create")]
        public IActionResult Create(string x)
        {
            return View("Create");
        }


    }
}