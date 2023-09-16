using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using College_API.Models;

namespace College_API.ViewModels
{
    public class PutCourseViewModel
    {
        public string? CourseNumber { get; set; }
        public string? Name { get; set; }
        public string? Duration { get; set; }
        public string? Description { get; set; }
        public string? Details { get; set; }
        public string? Category { get; set; }
        // public int EnrolledStudents { get; set; }

    }
}