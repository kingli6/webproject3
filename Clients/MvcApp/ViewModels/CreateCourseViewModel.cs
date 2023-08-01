using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcApp.ViewModels
{
    public class CreateCourseViewModel
    {
        [Required(ErrorMessage = "Course Number is required")]
        [Display(Name = "Course Nr")]
        public string? CourseNumber { get; set; }


        [Required(ErrorMessage = "Course Name is required")]
        [Display(Name = "Name")]
        public string? Name { get; set; }


        [Required(ErrorMessage = "Course Duration is required")]
        [Display(Name = "Length")]
        public string? Duration { get; set; }


        [Required(ErrorMessage = "Course Description is required")]
        [Display(Name = "Description")]
        public string? Description { get; set; }


        [Required(ErrorMessage = "Course Details is required")]
        [Display(Name = "Detail")]
        public string? Details { get; set; }
        // public string? ImagePath { get; set; }
    }
}