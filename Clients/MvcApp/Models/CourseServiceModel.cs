using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using MvcApp.ViewModels;

namespace MvcApp.Models
{
    public class CourseServiceModel
    {
        private readonly string _baseUrl;
        private readonly JsonSerializerOptions _options;
        private readonly IConfiguration _config;
        public CourseServiceModel(IConfiguration config)
        {
            _config = config;
            _baseUrl = $"{_config.GetValue<string>("baseUrl")}/courses"!;

            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<List<CourseViewModel>> ListAllCourse()
        {
            var url = $"{_baseUrl}/GetAllCourses";

            using var http = new HttpClient();
            var response = await http.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new Exception("It didn't work!! A) ListAllCourse method...");

            //One liner
            //var result = await response.Content.ReadFromJsonAsync<List<CourseViewModel>>();
            //or two liner
            var result = await response.Content.ReadAsStringAsync();
            var courses = JsonSerializer.Deserialize<List<CourseViewModel>>(result, _options);

            return courses ?? new List<CourseViewModel>();  //return courses, if null, return an empty CourseViewModel
        }
        public async Task<CourseViewModel> FindCourse(int id)
        {
            var baseUrl = _config.GetValue<string>("baseUrl");
            var url = $"{baseUrl}/courses/{id}";

            using var http = new HttpClient();
            var response = await http.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                Console.WriteLine("Details method didn't work, couldn't find the course with id: " + id);

            var course = await response.Content.ReadFromJsonAsync<CourseViewModel>();

            return course ?? new CourseViewModel();
        }

        public async Task<bool> CreateCourse(CreateCourseViewModel course)
        {
            using var http = new HttpClient();
            var baseUrl = _config.GetValue<string>("baseUrl");
            var url = $"{baseUrl}/courses/AddCourse";

            var response = await http.PostAsJsonAsync(url, course);

            if (!response.IsSuccessStatusCode)
            {
                string reason = await response.Content.ReadAsStringAsync();
                Console.WriteLine(reason);
                return false;
            }
            return true;


        }
    }


}