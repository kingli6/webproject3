using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace College_API.ViewModels
{
    public class PatchCourseViewModel
    {
        public string? Description { get; set; }
        public string? Details { get; set; }
        public string? ImagePath { get; set; } // seperate one for this?
    }
}