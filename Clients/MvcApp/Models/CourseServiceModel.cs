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
    }
}