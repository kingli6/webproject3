using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            return View("Details");
        }


    }
}