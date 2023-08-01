using Microsoft.AspNetCore.Mvc;
using MvcApp.Models;
using MvcApp.ViewModels;

namespace MvcApp.Controllers
{
    [Route("[controller]")]
    public class CoursesController : Controller
    {
        private readonly IConfiguration _config;
        private readonly CourseServiceModel _courseService;

        public CoursesController(IConfiguration config)
        {
            _config = config;
            _courseService = new CourseServiceModel(_config);   //220511_12.. 2:04.00
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
                // var courseService = new CourseServiceModel(_config);
                var course = await _courseService.FindCourse(id);

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
        //why is this method needed?
        [HttpGet("Create")]
        public IActionResult Create()
        {
            var course = new CreateCourseViewModel();
            return View("Create", course);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateCourseViewModel course)   //don't use model as a variable name since in MVC there is key word "model" is being used
        {
            if (!ModelState.IsValid)
            {
                return View("Create", course);
            }
            // var courseService = new CourseServiceModel(_config);
            if (await _courseService.CreateCourse(course))
                return View("CourseSavedConfirmation");

            return View("Create", course);// Should send a unsuccessful page if it fails.

        }

    }
}